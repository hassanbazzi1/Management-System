using Common.Auth.JWT;
using Common.Web;
using Common.Web.HttpService;
using SignupService.WebSocket;
using SignupService.WebSocket.Hubs;

namespace SignupService;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add SignalR
        builder.Services.AddSignalR();
        builder.Services.AddLogging(logging =>
        {
            logging.AddFilter("Microsoft.AspNetCore.SignalR", LogLevel.Debug);
            logging.AddFilter("Microsoft.AspNetCore.Http.Connections", LogLevel.Debug);
        });

        builder.Services.AddSingleton<TerminalPolicies>();

        // Controllers and Routing
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // JWT Services and Credentials
        builder.Services.AddSingleton<JwtService>();
        var validationConfig = builder.Configuration.GetSection("Auth:JWT:Validation").Get<List<JwtValidationConfig>>();
        foreach (var config in validationConfig)
            builder.Services.Configure<JwtValidationConfig>(config.Name, builder.Configuration.GetSection("Auth:JWT:Validation:" + config.Name));

        var signingCredentials = builder.Configuration.GetSection("Auth:JWT:Signing").Get<List<JwtSigningConfig>>();
        foreach (var config in signingCredentials)
            builder.Services.Configure<JwtValidationConfig>(config.Name, builder.Configuration.GetSection("Auth:JWT:Signing:" + config.Name));


        // HttpClients for different servers that will be accessed
        builder.Services.AddHttpClient(HttpConfig.DefaultHttpClient, httpClient => { });

        builder.Services.AddHttpClient(HttpConfig.N8NOnboardingWebhook, httpClient => { httpClient.BaseAddress = new Uri(builder.Configuration.GetValue("N8N:WebhookURL", string.Empty)); });

        builder.Services.AddHttpClient(HttpConfig.MainServerHttpClient, httpClient => { httpClient.BaseAddress = new Uri(builder.Configuration.GetValue("Servers:Master:URL", string.Empty)); });

        // HTTP Services
        builder.Services.AddSingleton<HttpWebService>();
        builder.Services.AddSingleton<JwtHttpWebService>();

        var app = builder.Build();

        // Map our Hub endpoint
        app.MapHub<ServerHub>("/ServerHub");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        await app.RunAsync();
    }
}