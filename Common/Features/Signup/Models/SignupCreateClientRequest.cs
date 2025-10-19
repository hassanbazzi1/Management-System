namespace Common.Features.Signup.Models;

public class SignupCreateClientRequest
{
    public string ContactPhone { get; set; }
    public int SubscriptionType { get; set; }
    public string CompanyName { get; set; }
    public string WebsiteUrl { get; set; }
    public string BusinessEmail { get; set; }
    public string BusinessPhone { get; set; }
    public int Industry { get; set; }

    public string IdealCustomer { get; set; }
    public string Services { get; set; }
    public string? FacebookUrl { get; set; }
    public string? TiktokUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? Xurl { get; set; }
    public string? InstagramUrl { get; set; }

    public bool FromChat { get; set; }
}