using Microsoft.Identity.Client;

namespace Common.Email.Models;

public class MicrosoftEmailOptions : EmailOptions
{
    public readonly string[] Scopes;

    public MicrosoftEmailOptions(string[] scopes, string clientId, string tenantId, string userId,string clientSecret)
    {
        Scopes = scopes;
        ClientId = clientId;
        TenantId = tenantId;
        UserId = userId;
        ClientSecret = clientSecret;
    }

    public string ClientId { get; set; }
    public string TenantId { get; set; }

    public string UserId { get; set; }
    
    public string ClientSecret { get; set; }
}