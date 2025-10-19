using Common.API.Models;
using Common.Auth.JWT;
using Common.DB.MasterDB;
using Common.DB.MasterDB.Models;
using Common.Features.Signup.Models;
using Common.Web;
using MainServer.Common.Controllers;
using MainServer.Features.Signup.Models;
using MainServer.Features.Signup.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MainServer.Features.Signup.Controllers;

[Route(ApiBase)]
[ApiController]
public class SignupController : MainServerController
{
    private const string ApiBase = "api/signup";
    private const string RouteCreateClient = "CreateClientAsync";
    private const string RouteVerifyEmail = "VerifyEmailAsync";
    private const string RouteSetNewPassword = "SetNewPasswordAsync";
    private const string RouteNewPasswordEmail = "NewPasswordAsync";

    private const int EmailVerifyLinkDurationMinutes = 15;

    // TODO: How to set this in .NET identity
    private const int NewPasswordLinkDurationMinutes = 60;
    private readonly IOptionsMonitor<JwtValidationConfig> _jwtValidationConfig;
    private readonly IOptionsMonitor<JwtSigningConfig> _jwtSigningConfig;

    // JWT Service and Config
    private readonly JwtService _jwtService;

    private readonly SignupService _signupService;


    public SignupController(MasterDbContext masterDbContext, IOptionsMonitor<JwtValidationConfig> jwtValidationConfig, IOptionsMonitor<JwtSigningConfig> jwtSigningConfig, JwtService jwtService, SignupService signupService) : base(masterDbContext)
    {
        _jwtService = jwtService;
        _jwtValidationConfig = jwtValidationConfig;
        _jwtSigningConfig = jwtSigningConfig;
        _signupService = signupService;
    }

    // POST: api/Signup
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost(RouteCreateClient)]
    [Authorize(AuthenticationSchemes = JwtConfig.KeyMainOnboarding + "," + JwtConfig.KeyJwtMainMain)]
    public async Task<ActionResult<ApiResponse<ClientUserApiResponse>>> PostCreateClient(SignupCreateClientRequest data)
    {
        await using var transaction = await DbContext.Database.BeginTransactionAsync();
        {
            var response = new ApiResponse<ClientUserApiResponse>();
            try
            {
                var identityUser = new IdentityUser
                {
                    UserName = data.BusinessEmail,
                    Email = data.BusinessEmail
                };

                var result = await _signupService.UserManager.CreateAsync(identityUser);
                if (result.Succeeded)
                {
                    // Create client with default server ID
                    var clientCreateResult = await _signupService.CreateClientEntity(data.Industry, data.BusinessEmail, data.BusinessPhone, data.Services, data.WebsiteUrl, data.CompanyName, data.FacebookUrl, data.TiktokUrl, data.Xurl, data.LinkedInUrl, data.InstagramUrl, data.IdealCustomer, identityUser.Id);

                    await transaction.CommitAsync();

                    if (clientCreateResult.IsSuccess())
                    {
                        var client = clientCreateResult.Data;
                        var user = client.ClientUsers.First();

                        var verifyLink = GenerateVerifyLink(user, data.SubscriptionType, data.FromChat);
                        await _signupService.EmailSender.SendConfirmationLinkAsync(identityUser, user.Email, verifyLink);

                        response.SetStatusSuccess(new ClientUserApiResponse
                        {
                            ClientId = client.Id,
                            UserId = user.Id
                        });

                        return CreatedAtAction(RouteCreateClient, response);
                    }
                }

                throw new Exception("Error creating client");
            }
            catch (Exception e)
            {
                response.SetStatusFailure(e.Message);
            }

            return BadRequest(response);
        }
    }

    [HttpGet(RouteVerifyEmail)]
    public async Task<ActionResult<CommonApiResponse>> GetVerifyEmail([FromQuery] SignupVerifyEmailRequest request)
    {
        var response = new ApiResponse<ClientUserApiResponse>();
        try
        {
            var claimsResult = await _jwtService.Decode(request.Token, _jwtValidationConfig.Get(JwtConfig.KeyJwtMainMain));
            if (claimsResult.IsSuccess())
            {
                if (claimsResult.Data.ContainsKey(ClaimsClientId) && claimsResult.Data.ContainsKey(ClaimsPhone) && claimsResult.Data.ContainsKey(ClaimsUserId) && claimsResult.Data.ContainsKey(ClaimsSubscriptionType))
                {
                    var client = await _signupService.MasterDbContext.Clients.FirstOrDefaultAsync(c => c.Id == int.Parse(claimsResult.Data[ClaimsClientId]));
                    if (client == null)
                        throw new Exception("Invalid client ID");

                    var user = await _signupService.MasterDbContext.ClientUsers.Include(d => d.IdentityUser).FirstOrDefaultAsync(u => u.Id == int.Parse(claimsResult.Data[ClaimsUserId]) && u.Cid == client.Id);

                    if (user == null || user.IdentityUser == null)
                        throw new Exception("Invalid user ID");

                    var phone = claimsResult.Data[ClaimsPhone];
                    // TODO: Check for already active client to prevent creating database again
                    if (true) //client.IsActive || user.IsActive)
                    {
                        client.IsActive = true;
                        user.IsActive = true;

                        await _signupService.MasterDbContext.SaveChangesAsync();

                        var createResult = await _signupService.InitializeClientDatabase(client, int.Parse(claimsResult.Data[ClaimsSubscriptionType]));
                        if (createResult.IsSuccess())
                        {
                            var newPasswordLink = GenerateNewPasswordLink(user);

                            if (request.FromChat)
                            {
                                // TODO: Respond Immediately to Onboarding Server
                                // Will either need HTTP Endpoint or Websocket on Onboarding server
                                // OR Handle immediately via endpoint on N8N without passing through onboarding server
                                /*
                                var responseToken = _masterToN8NJwt.Encode(new Dictionary<string, string>
                                {
                                    { ClaimsLink, newPasswordLink },
                                    { ClaimsStatus, CommonApiResponse.GetStatusStatus() },
                                    { ClaimsPhone, phone }
                                }, TimeSpan.FromSeconds(120)).Data;

                                var http = new HttpClient();

                                var webHookResponse = http.PostAsJsonAsync(new Uri(N8NVerifyResultWebhook),
                                    new SignupVerifyEmailResponse
                                    {
                                        Token = responseToken
                                    });
                                */
                                response.SetStatusSuccess();
                            }
                            else
                            {
                                // TODO: From Web ==> Redirect to page to enter new password
                                await _signupService.EmailSender.SendPasswordResetLinkAsync(user.IdentityUser, user.Email, newPasswordLink);
                                response.SetStatusSuccess();
                            }


                            return Ok(response);
                        }

                        throw new Exception("Error creating client database");
                    }
                    // TODO: Already activated?
                }
            }

            // TODO: More detailed error handling
            throw new Exception("Missing parameters");
        }
        catch (Exception e)
        {
            response.SetStatusFailure(e.Message);
        }

        return BadRequest(response);
    }

    [HttpPost(RouteSetNewPassword)]
    public async Task<ActionResult<CommonApiResponse>> PostSetNewPassword(SignupSetNewPasswordRequest request)
    {
        var response = new CommonApiResponse();
        try
        {
            var claimsResult = await _jwtService.Decode(request.Token, _jwtValidationConfig.Get(JwtConfig.KeyJwtMainMain));
            if (claimsResult.IsSuccess())
            {
                var clientUser = _signupService.MasterDbContext.ClientUsers.Include(x => x.IdentityUser).First(u => u.Id == int.Parse(claimsResult.Data[ClaimsUserId]) && u.Cid == int.Parse(claimsResult.Data[ClaimsClientId]));
                var result = await _signupService.UserManager.ResetPasswordAsync(clientUser.IdentityUser, request.Token, request.Password);
                if (!result.Succeeded)
                    throw new Exception("Error setting password");

                response.SetStatusSuccess();
                return Ok(response);
            }

            throw new Exception("Invalid token");
        }
        catch (Exception e)
        {
            response.SetStatusFailure(e.Message);
        }

        return BadRequest(response);
    }

    private string GenerateNewPasswordLink(ClientUser user)
    {
        // TODO: Wrap token in JWT before sending by email
        var token = _signupService.UserManager.GeneratePasswordResetTokenAsync(user.IdentityUser);
        // TODO: Use absolute request path for api, not based on current request
        var uri = UriUtil.GetUri(Request.Host.ToUriComponent(), Path.Combine(ApiBase, RouteNewPasswordEmail), new Dictionary<string, object?>
        {
            { ParamToken, token }
        });

        return uri.ToString();
    }

    private string GenerateVerifyLink(ClientUser user, int subscriptionType, bool fromChat)
    {
        var jwtResult = _jwtService.Encode(_jwtSigningConfig.Get(JwtConfig.KeyJwtMainMain), new Dictionary<string, string>
        {
            { ClaimsPhone, user.Client.Phone },
            { ClaimsClientId, user.Cid.ToString() },
            { ClaimsSubscriptionType, subscriptionType.ToString() },
            { ClaimsUserId, user.Id.ToString() }
        }, TimeSpan.FromMinutes(EmailVerifyLinkDurationMinutes));

        // TODO: Use absolute request path for api, not based on current request
        var uri = UriUtil.GetUri(Request.Host.ToUriComponent(), Path.Combine(ApiBase, RouteVerifyEmail), new Dictionary<string, object?>
        {
            { ParamToken, jwtResult.Data },
            { ParamFromChat, fromChat.ToString() }
        });

        return uri.ToString();
    }
}