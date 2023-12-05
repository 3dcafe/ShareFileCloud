using DTO.Auth;
using Entites.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ShareFileCloud.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> RegisterAsync(Register model)
    {
        //todo
        return null;
    }

    public async Task<IActionResult> LoginAsync(Login model)
    {
        //todo
        return null;
    }

    public async Task<IActionResult> LogoutAsync()
    {
        //todo
        return null;
    }
}