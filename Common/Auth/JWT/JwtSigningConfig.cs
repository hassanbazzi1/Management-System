using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Common.Auth.JWT;

public class JwtSigningConfig
{
    public string Name { get;set; }
    public string SecretKey { get;set; }
    public string? Issuer { get;set; }
    public string? Audience { get;set; }

    public SecurityTokenDescriptor GetSecurityTokenDescriptor(Dictionary<string, string>? claims = null, TimeSpan? expiry = null)
    {
        var keyBytes = Encoding.UTF8.GetBytes(SecretKey);
        var key = new SymmetricSecurityKey(keyBytes);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenClaims = new List<Claim>();
        if (claims != null)
            foreach (var kvp in claims)
                tokenClaims.Add(new Claim(kvp.Key, kvp.Value));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(tokenClaims),
            Expires = DateTime.UtcNow.Add(expiry ?? TimeSpan.FromHours(1)),
            SigningCredentials = credentials
        };

        if (Issuer != null)
            tokenDescriptor.Issuer = Issuer;
        if (Audience != null)
            tokenDescriptor.Audience = Audience;

        return tokenDescriptor;
    }
}