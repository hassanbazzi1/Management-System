namespace MainServer.Features.Signup.Models;

public class SignupVerifyEmailRequest
{
    public string Token { get; set; }
    public bool FromChat { get; set; }
}