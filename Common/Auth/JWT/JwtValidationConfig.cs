using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Common.Auth.JWT;

public class JwtValidationConfig
{
    public string Name { get; set; }
    public string SecretKey { get;set; }
    public string? ValidIssuer { get;set; }
    public string? ValidAudience { get;set; }

    public TokenValidationParameters GetTokenValidationParameters()
    {
        var keyBytes = Encoding.UTF8.GetBytes(SecretKey);
        var key = new SymmetricSecurityKey(keyBytes);

        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = ValidIssuer != null,
            ValidateAudience = ValidAudience != null,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidAlgorithms = [SecurityAlgorithms.HmacSha256]
        };

        if (ValidIssuer != null)
            parameters.ValidIssuer = ValidIssuer;
        if (ValidAudience != null)
            parameters.ValidAudience = ValidAudience;

        return parameters;
    }
}