using Common.Auth.JWT;
using Common.DB.ClientDB;
using Common.DB.ClientDB.Factories;
using Common.DB.ClientDB.Services;
using Common.DB.Global;
using Common.DB.MasterDB;
using Common.DB.MasterDB.Services;
using Common.Email.Models;
using Common.Email.Services;
using Common.N8N.API.Models;
using Common.N8N.API.Services;
using Common.Web;
using Common.Web.HttpService;
using MainServer.Common.Services;
using MainServer.Features.Signup.Models;
using MainServer.Features.Signup.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MainServer;

public class Program
{
    // Fixed Values
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Email Providers
        builder.Services.Configure<GoogleEmailOptions>(builder.Configuration.GetSection("Email:Google"));
        builder.Services.AddSingleton<EmailService, GmailApiEmailService>();
        builder.Services.AddSingleton<EmailSender>();
        builder.Services.AddSingleton<IEmailSender<IdentityUser>, EmailSender>();

        // Master and Client Template DB Contexts
        var masterConn = builder.Configuration.GetValue("DB:Master:PostGreSQL.ConnectionString", string.Empty);
        builder.Services.AddDbContextFactory<MasterDbContext>(b => b.UseNpgsql(masterConn));
        // TODO: Add selector for client template database and master database connection strings
        builder.Services.AddDbContextFactory<ClientDbContext>(b => b.UseNpgsql(masterConn));
        builder.Services.AddScoped<ClientTemplateDbService>();
        builder.Services.AddSingleton<ClientDbContextFactory, PostGreSqlClientContextFactory>();

        // Global DB Context
        var globalConn = builder.Configuration.GetValue("DB:Global:PostGreSQL.ConnectionString", string.Empty);
        builder.Services.AddDbContextFactory<GlobalDbContext>(b => b.UseNpgsql(globalConn));

        // Signup Config and Service
        builder.Services.Configure<SignupConfig>(builder.Configuration.GetSection("Signup:Config"));
        builder.Services.AddScoped<SignupService, PostGreSqlSignupService>();

        // Model DB Services
        builder.Services.AddScoped<MessageTemplateDbService>();

        // Controllers and Routing
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Controllers
        builder.Services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; });

        // ASP.NET Identity
        builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddRoles<IdentityRole>().AddSignInManager().AddEntityFrameworkStores<MasterDbContext>();

        // JWT Services and Credentials
        builder.Services.AddSingleton<JwtService>();

        var validationConfig = builder.Configuration.GetSection("Auth:JWT:Validation").Get<List<JwtValidationConfig>>();
        foreach (var config in validationConfig)
            builder.Services.Configure<JwtValidationConfig>(config.Name, builder.Configuration.GetSection("Auth:JWT:Validation:" + config.Name));

        var signingCredentials = builder.Configuration.GetSection("Auth:JWT:Signing").Get<List<JwtSigningConfig>>();
        foreach (var config in signingCredentials)
            builder.Services.Configure<JwtValidationConfig>(config.Name, builder.Configuration.GetSection("Auth:JWT:Signing:" + config.Name));

        // HttpClients for different servers that will be accessed
        // TODO: Set lifetime for Http handlers
        builder.Services.AddHttpClient();
        builder.Services.AddHttpClient(HttpConfig.DefaultHttpClient, httpClient => { });

        builder.Services.AddHttpClient(HttpConfig.OnboardingHttpClient, httpClient => { httpClient.BaseAddress = new Uri(builder.Configuration.GetValue("Servers:Onboarding:URL", string.Empty)); });

        builder.Services.AddHttpClient(HttpConfig.ClientHttpClient, httpClient => { });

        builder.Services.AddHttpClient(HttpConfig.N8NAPIHttpClient, httpClient =>
        {
            httpClient.BaseAddress = new Uri(builder.Configuration.GetValue("N8N:API:BasePath", string.Empty));
            httpClient.DefaultRequestHeaders.Add(builder.Configuration.GetValue("N8N:API:TokenHeaderName", string.Empty), builder.Configuration.GetValue("N8N:API:Token", string.Empty));
        });

        // N8N API Services
        builder.Services.Configure<N8NApiOptions>(builder.Configuration.GetSection("N8N:Base:Paths"));
        builder.Services.AddSingleton<N8NApiService>();

        // HTTP Web Services
        builder.Services.AddSingleton<HttpWebService>();
        builder.Services.AddSingleton<JwtHttpWebService>();

        // Authorization
        builder.Services.AddAuthorization();

        // Authentication
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtConfig.KeyJwtMainMain;
            options.DefaultChallengeScheme = JwtConfig.KeyJwtMainMain;
        }).AddJwtBearer(JwtConfig.KeyJwtMainMain, options => { options.TokenValidationParameters = validationConfig.First(x => x.Name.Equals(JwtConfig.KeyJwtMainMain)).GetTokenValidationParameters(); }).AddJwtBearer(JwtConfig.KeyMainOnboarding, options => { options.TokenValidationParameters = validationConfig.First(x => x.Name.Equals(JwtConfig.KeyMainOnboarding)).GetTokenValidationParameters(); });

        var app = builder.Build();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.UseEndpoints(endpoints =>
        {
            if (app.Environment.IsDevelopment())
            {
                endpoints.MapControllers().AllowAnonymous();
            }
        });

        app.MapIdentityApi<IdentityUser>();
        //app.UseCors();app.UseCors();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        using (var scope = app.Services.CreateScope())
        {
            var templateService = scope.ServiceProvider.GetRequiredService<MessageTemplateDbService>();
            await templateService.InitializeDefaultMessageTemplates();
        }

        await app.RunAsync();
    }
}