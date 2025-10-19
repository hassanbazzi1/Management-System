using Microsoft.AspNetCore.Identity;

namespace Common.Auth;

public class IdentityUserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
{
    public readonly SignInManager<IdentityUser> SignInManager = signInManager;
    public readonly UserManager<IdentityUser> UserManager = userManager;
    private RoleManager<IdentityRole> _roleManager = roleManager;
}