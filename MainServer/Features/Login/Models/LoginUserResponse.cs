using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MainServer.Features.Login.Models;

public class LoginUserResponse
{
    public string APIToken { get; set; }

    public SignInResult Result { get; set; }
}