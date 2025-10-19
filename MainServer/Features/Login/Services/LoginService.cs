using Common.Auth;
using Common.DB.MasterDB;
using Microsoft.AspNetCore.Identity;

namespace MainServer.Features.Login.Services;

public class LoginService : IdentityUserService
{
    public LoginService(MasterDbContext masterContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager) : base(userManager, signInManager, roleManager)
    {
        MasterDbContext = masterContext;
    }

    public MasterDbContext MasterDbContext { get; }
}