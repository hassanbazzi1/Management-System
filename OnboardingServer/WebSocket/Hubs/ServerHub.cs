using System.Text;
using Common.API.Models;
using Common.Auth.JWT;
using Common.Features.Signup.Models;
using Common.SignalR;
using Common.Web;
using Common.Web.HttpService;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignupService.WebSocket.Models;

namespace SignupService.WebSocket.Hubs;

public class ServerHub : Hub<ClientInterface>
{
    private const string RouteCreateClient = "CreateClientAsync";

    private const string KIND_MESSAGE = "message";
    private const string KIND_VERIFY = "verify";
    private const string TYPE_TEXT = "text";
    private readonly IOptionsMonitor<JwtSigningConfig> _jwtSigningConfig;
    private readonly JwtHttpWebService _jwtHttpWebService;
    private readonly ILogger<ServerHub> _logger;
    private readonly TerminalPolicies _terminalPolicies;

    public ServerHub(ILogger<ServerHub> logger, TerminalPolicies terminalPolicies, JwtHttpWebService jwtHttpWebService, IOptionsMonitor<JwtSigningConfig> jwtSigningConfig)
    {
        _logger = logger;
        _terminalPolicies = terminalPolicies;
        _jwtSigningConfig = jwtSigningConfig;
        _jwtHttpWebService = jwtHttpWebService;
    }

    public async Task SendMessage(string message)
    {
        try
        {
            var n8nMessage = new N8NTextMessage
            {
                Kind = KIND_MESSAGE,
                Data = message,
                Type = TYPE_TEXT,
                ConnectionId = Context.ConnectionId
            };

            var json = JObject.FromObject(n8nMessage);
            var httpClient = _jwtHttpWebService.GetAuthorizedHttpClient(HttpConfig.N8NOnboardingWebhook, _jwtSigningConfig.Get(JwtConfig.KeyOnboardingN8N), null, TimeSpan.FromSeconds(300));
            var httpResponse = await httpClient.PostAsync("", new StringContent(json.ToString(), Encoding.UTF8, "application/json"));
            var response = await httpResponse.Content.ReadFromJsonAsync<N8NTextMessage>();
            if (response.Kind == KIND_MESSAGE)
            {
                await Clients.Client(Context.ConnectionId).ReceiveMessage(response.Data);
            }
            else if (response.Kind == KIND_VERIFY)
            {
                httpClient = _jwtHttpWebService.GetAuthorizedHttpClient(HttpConfig.MainServerHttpClient, _jwtSigningConfig.Get(JwtConfig.KeyMainOnboarding), null, TimeSpan.FromSeconds(300));
                var createRequestData = JsonConvert.DeserializeObject<SignupCreateClientRequest>(response.Data);
                // TODO: Should input validation be done here or one main server?
                var createResponse = await httpClient.PostAsync(RouteCreateClient, new StringContent(createRequestData.ToString(), Encoding.UTF8, "application/json"));
                var responseData = await createResponse.Content.ReadFromJsonAsync<ApiResponse<ClientUserApiResponse>>();
                if (!responseData.IsStatusSuccess())
                    throw new Exception(responseData.Message);

                await Clients.Client(Context.ConnectionId).ReceiveMessage("Your account is being created. Please wait..");
            }
            else
            {
                throw new Exception("Unexpected input type");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // TODO: Handle 
        }
    }

    /// <summary>
    ///     Logs when a client connects to the hub.
    /// </summary>
    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("Client connected: {ConnId}", Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    /// <summary>
    ///     Handles client disconnection and removes their terminal session.
    /// </summary>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var terminalId = _terminalPolicies.GetClientTerminalIdByConnectionId(Context.ConnectionId);
        if (!string.IsNullOrEmpty(terminalId))
            _terminalPolicies.RemoveTerminal(terminalId);

        _logger.LogInformation("Client disconnected: {ConnId}", Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    ///     Performs the handshake, registering the client and initiating authentication.
    /// </summary>
    public async Task<string> Handshake(ClientConnectionInfo connectionInfo)
    {
        connectionInfo.ConnectionId = Context.ConnectionId;

        _logger.LogInformation("Client attempting handshake: {ConnId}", Context.ConnectionId);

        var errorMsg = _terminalPolicies.OnHandshake(connectionInfo);
        if (!string.IsNullOrEmpty(errorMsg))
        {
            _logger.LogWarning("Handshake failed: {Error}", errorMsg);
            return errorMsg;
        }

        _logger.LogInformation("Handshake succeeded for {ConnId}", Context.ConnectionId);

        // Temporary auto-authentication for testing; remove in production
        await MarkAuthenticated(connectionInfo.TerminalId);
        return string.Empty;
    }

    /// <summary>
    ///     Marks the specified terminal as authenticated.
    /// </summary>
    public Task MarkAuthenticated(string terminalId)
    {
        _terminalPolicies.MarkTerminalAsAuthenticated(terminalId);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Removes the terminal session upon explicit disconnect request.
    /// </summary>
    public Task DisconnectTerminal(string? terminalId)
    {
        if (!string.IsNullOrEmpty(terminalId))
            _terminalPolicies.RemoveTerminal(terminalId);
        return Task.CompletedTask;
    }
}