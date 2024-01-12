using Microsoft.AspNetCore.Mvc;
using ShareFileCloud.SimpleModels.Auth;

namespace ShareFileCloud.Services.Auth;

public interface IAuthService
{
    Task<LoginSimpleAnswer> LoginAsync(Login model);
    Task<IActionResult> RegisterAsync(Register model);
    Task<IActionResult> LogoutAsync();
}