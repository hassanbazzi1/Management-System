using Common.Auth.JWT;
using Common.DB.ClientDB;
using Common.DB.ClientDB.Factories;
using Common.DB.ClientDB.Services;
using Common.Web;
using Common.Whatsapp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientAPIServer;

public class Program
{
    // Fixed Values

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Client DB Contexts
        var clientConn = builder.Configuration.GetValue("DB:Clients:PostGreSQL.ConnectionString", string.Empty);
        builder.Services.AddDbContextFactory<ClientDbContext>(b => b.UseNpgsql(clientConn));
        builder.Services.AddSingleton<ClientDbContextFactory, PostGreSqlClientContextFactory>();

        // WhatsApp Credentials
 /*       builder.Services.AddSingleton(new WhatsAppBusinessCredential
        {
            AccessToken = builder.Configuration.GetValue("Auth:WhatsappBusiness:Token", string.Empty),
            BusinessAccountId = builder.Configuration.GetValue("Auth:WhatsappBusiness:BusinessAccountId", string.Empty)
        });*/

        // Controllers and Routing
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Controllers
        builder.Services.AddControllers();

        // JWT Services and Credentials
        builder.Services.AddSingleton<JwtService>();
        var validationConfig = builder.Configuration.GetSection("Auth:JWT:Validation").Get<List<JwtValidationConfig>>();
        foreach (var config in validationConfig)
            builder.Services.Configure<JwtValidationConfig>(config.Name, builder.Configuration.GetSection("Auth:JWT:Validation:" + config.Name));

     /*   var signingCredentials = builder.Configuration.GetSection("Auth:JWT:Signing").Get<List<JwtSigningConfig>>();
        foreach (var config in signingCredentials)
            builder.Services.Configure<JwtValidationConfig>(config.Name, builder.Configuration.GetSection("Auth:JWT:Signing:" + config.Name));*/

        // HttpClients for different servers that will be accessed
        builder.Services.AddHttpClient();
        builder.Services.AddHttpClient(HttpConfig.DefaultHttpClient, httpClient => { });

        // TODO: Add dictionary of servers like with JWT credentials
        builder.Services.AddHttpClient(HttpConfig.MainServerHttpClient, httpClient => { httpClient.BaseAddress = new Uri(builder.Configuration.GetValue("Servers:Master:URL", string.Empty)); });
        builder.Services.AddScoped<ChatDbService>();
        // DB Services
        builder.Services.AddScoped<ConversationDbService>();
        builder.Services.AddScoped<ChatMessageDbService>();
 
        builder.Services.AddScoped<SessionDbService>();
        builder.Services.AddScoped<AIAgentDbService>();
        builder.Services.AddScoped<ChatEscalationDbService>();
       
        // Authorization
        builder.Services.AddAuthorization();

        // Authentication
        // Add Master To Client Authentication (to authenticate requests from master server)
     /*   builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtConfig.KeyJwtMainClient;
            options.DefaultChallengeScheme = JwtConfig.KeyJwtMainClient;
        }).AddJwtBearer(JwtConfig.KeyJwtMainClient, options => { options.TokenValidationParameters = validationConfig.First(x => x.Name.Equals(JwtConfig.KeyJwtMainClient)).GetTokenValidationParameters(); })
            .AddJwtBearer(JwtConfig.KeyN8NClient, options => { options.TokenValidationParameters = validationConfig.First(x => x.Name.Equals(JwtConfig.KeyN8NClient)).GetTokenValidationParameters(); });

*/
        var app = builder.Build();
        app.UseAuthentication();
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            if (app.Environment.IsDevelopment())
            {
                endpoints.MapControllers().AllowAnonymous();
            }
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        await app.RunAsync();

        // TODO: Should add IP restrictions, mTLS certificates, etc. for intra-server security
    }
}