using Microsoft.AspNetCore.Mvc;
using ShareFileCloud.Services.Auth;
using ShareFileCloud.SimpleModels.Auth;

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
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var answer = await _authService.LoginAsync(model);
            return answer.State ? Ok(answer) : BadRequest(answer);
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            return await _authService.RegisterAsync(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            return await _authService.LogoutAsync();
        }
    }
}
