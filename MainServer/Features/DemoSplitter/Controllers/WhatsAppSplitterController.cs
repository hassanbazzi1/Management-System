using System.Text;
using Common.API.Models;
using Common.Auth.JWT;
using Common.DB.MasterDB;
using Common.Features.DemoSplitter.Models;
using Common.Web;
using Common.Web.HttpService;
using MainServer.Common.Controllers;
using MainServer.Features.DemoSplitter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MainServer.Features.DemoSplitter.Controllers;

[Route(ApiBase)]
[ApiController]
public class WhatsAppSplitterController : MainServerController
{
    // Should match the names in client API service
    private const string ApiBase = "api/demosplitter";
    private const string RouteDemoMessageTrigger = "MessageTrigger";

    private readonly IOptionsMonitor<JwtSigningConfig> _jwtSigningConfig;
    private readonly JwtHttpWebService _jwtHttpWebService;

    public WhatsAppSplitterController(MasterDbContext masterDbContext, IOptionsMonitor<JwtSigningConfig> jwtSigningConfig, JwtHttpWebService jwtHttpWebService) : base(masterDbContext)
    {
        _jwtHttpWebService = jwtHttpWebService;
        _jwtSigningConfig = jwtSigningConfig;
    }

    [HttpPost(RouteDemoMessageTrigger)]
    public async Task<ActionResult<CommonApiResponse>> PostMessageTrigger(WhatsAppWebhookPayload payload)
    {
        var response = new CommonApiResponse();

        try
        {
            // TODO: Should i use WA phone id instead of phone number to uniquely identify
            var senderPhone = payload.Entries.First().Changes.First().Value.Messages.First().From;

            var client = await DbContext.Clients.Include(s => s.Server).FirstOrDefaultAsync(c => c.HasDemo == true && c.Phone == senderPhone);

            if (client == null || !client.Server.IsClient)
                throw new Exception("Invalid phone number");

            // TODO: Should client database be accessed directly here to detemine N8N workflow to forward to?
            var httpClient = _jwtHttpWebService.GetAuthorizedHttpClient(HttpConfig.ClientHttpClient, _jwtSigningConfig.Get(JwtConfig.KeyJwtMainClient), null, TimeSpan.FromSeconds(300));
            var uri = UriUtil.GetUri(client.Server.Url, Path.Combine(ApiBase, RouteDemoMessageTrigger));
            var body = JObject.FromObject(new N8NWebhookRequestResponse
            {
                SenderPhone = senderPhone,
                JsonBody = JsonConvert.SerializeObject(payload)
            });

            var bodyStr = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            httpClient.PostAsync(uri.ToString(), bodyStr);
            response.SetStatusSuccess();
            // TODO: Silently ignore response?
            return Ok(response);
        }
        catch (Exception e)
        {
            response.SetStatusFailure(e.Message);
        }

        return BadRequest(response);
    }
}