namespace MainServer.Features.Signup.Models;

public class SignupSetNewPasswordRequest
{
    public string Token { get; set; }

    public string Password { get; set; }
}