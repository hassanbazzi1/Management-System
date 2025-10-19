using ClientAPIServer.Common.Controllers;
using Common.API.Models;
using Common.Auth.JWT;
using Common.DB.ClientDB;
using Common.Features.DemoSplitter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPIServer.Features.DemoSplitter.Controllers;

[Route(ApiBase)]
[ApiController]
public class WhatsAppSplitterController : ClientAPIController
{
    private const string ApiBase = "api/demosplitter";
    private const string RouteDemoMessageTrigger = "MessageTrigger";
    private readonly JwtConfig _jwtConfig;

    private readonly JwtService _jwtService;

    public WhatsAppSplitterController(ClientDbContext clientDbContext, JwtService jwtService, JwtConfig jwtConfig) : base(clientDbContext)
    {
        _jwtService = jwtService;
        _jwtConfig = jwtConfig;
    }

    // POST: api/Signup
    [HttpPost(RouteDemoMessageTrigger)]
    [Authorize(AuthenticationSchemes = JwtConfig.KeyJwtMainClient)]
    public async Task<ActionResult<CommonApiResponse>> PostMessageTrigger(N8NWebhookRequestResponse request)
    {
        var response = new CommonApiResponse();
        try
        {
            // TODO: Verify N8N workflow exists and send request to it using N8N JWT

            response.SetStatusSuccess();
            return Ok(response);
            // TODO: Silently ignore errors?
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            response.SetStatusFailure("Error creating client");
            return BadRequest(response);
        }
    }
}