using DTO.Auth;
using Microsoft.AspNetCore.Mvc;
using ShareFileCloud.Services.Auth;

namespace ShareFileCloud.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            return await _authService.RegisterAsync(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            return await _authService.LoginAsync(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            return await _authService.LogoutAsync();
        }
    }
}
