using DTO.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ShareFileCloud.Services.Auth;

public interface IAuthService
{
    Task<IActionResult> RegisterAsync(Register model);
    Task<IActionResult> LoginAsync(Login model);
    Task<IActionResult> LogoutAsync();
}