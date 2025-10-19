using System.Security.Claims;
using Common.API.Models;
using Common.Auth.JWT;
using Common.DB.MasterDB;
using MainServer.Common.Controllers;
using MainServer.Features.Login.Models;
using MainServer.Features.Login.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MainServer.Features.Login.Controllers;

[Route(ApiBase)]
[ApiController]
public class LoginController : MainServerController
{
    private const string ApiBase = "api/login";
    private const string RouteLogin = "Login";

    // TODO: Move to config maybe
    private const int DefaultApiClientTokenDurationMinutes = 60;
    private readonly IOptionsMonitor<JwtSigningConfig> _jwtSigningConfig;

    // JWT Service and Config
    private readonly JwtService _jwtService;

    private readonly LoginService _loginService;

    public LoginController(MasterDbContext masterDbContext, LoginService loginService, IOptionsMonitor<JwtSigningConfig> jwtSigningConfig, JwtService jwtService) : base(masterDbContext)
    {
        _loginService = loginService;
        _jwtSigningConfig = jwtSigningConfig;
        _jwtService = jwtService;
    }

    [HttpPost(RouteLogin)]
    public async Task<ActionResult<ApiResponse<LoginUserResponse>>> PostUserLogin(LoginUserRequest request)
    {
        var response = new ApiResponse<LoginUserResponse>();
        try
        {
            var clientUser = await DbContext.ClientUsers.Include(x => x.IdentityUser).FirstOrDefaultAsync(x => x.Email.Equals(request.Email));
            // Should never happen
            if (clientUser == null || clientUser.IdentityUser.Email != request.Email)
                throw new Exception("Email not found");

            var result = await _loginService.SignInManager.CheckPasswordSignInAsync(clientUser.IdentityUser, request.Password, true);
            if (result.Succeeded)
            {
                // TODO: Any way to get roles with user immediately
                var roles = await _loginService.UserManager.GetRolesAsync(clientUser.IdentityUser);

                if (roles.Count == 0)
                    throw new Exception("No roles found for user");

                var claims = new Dictionary<string, string>
                {
                    { ClaimsClientId, clientUser.Cid.ToString() },
                    { ClaimsUserId, clientUser.Id.ToString() }
                };

                foreach (var role in roles)
                    claims.Add(ClaimTypes.Role, role);

                var token = _jwtService.Encode(_jwtSigningConfig.Get(JwtConfig.KeyJwtMainMain), claims, TimeSpan.FromMinutes(DefaultApiClientTokenDurationMinutes));

                // TODO: Should i invalidate login here?
                if (!token.IsSuccess())
                    throw new Exception("Error generating API token");

                response.SetStatusSuccess(new LoginUserResponse
                {
                    APIToken = token.Data,
                    Result = result
                });

                return Ok(response);
            }

            response.SetStatusFailure("Incorrect password");
        }
        catch (Exception e)
        {
            response.SetStatusFailure(e.Message);
        }

        // TODO: Fine tune error response
        return BadRequest(response);
    }
}