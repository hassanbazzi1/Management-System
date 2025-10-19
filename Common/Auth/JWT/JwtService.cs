using System.IdentityModel.Tokens.Jwt;
using Common.Misc;

namespace Common.Auth.JWT;

public class JwtService
{
    public DataActionResult<string> Encode(JwtSigningConfig signingConfig, Dictionary<string, string>? claims, TimeSpan? expiry = null)
    {
        var result = new DataActionResult<string>();
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = signingConfig.GetSecurityTokenDescriptor(claims, expiry);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            result.Data = tokenHandler.WriteToken(token);
            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }

    public async Task<DataActionResult<Dictionary<string, string>>> Decode(string token, JwtValidationConfig validationConfig)
    {
        var result = new DataActionResult<Dictionary<string, string>>();
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = validationConfig.GetTokenValidationParameters();

            var validatedToken = await tokenHandler.ValidateTokenAsync(token, tokenValidationParameters);

            if (validatedToken.IsValid)
            {
                var jwtToken = (JwtSecurityToken)validatedToken.SecurityToken;

                var dic = new Dictionary<string, string>();
                foreach (var claim in jwtToken.Claims)
                    dic[claim.Type] = claim.Value;

                result.Data = dic;
                result.Status = ActionResult.ActionStatus.Success;
            }
            else
            {
                throw new Exception("Invalid token");
            }
        }
        catch (Exception e)
        {
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }
}