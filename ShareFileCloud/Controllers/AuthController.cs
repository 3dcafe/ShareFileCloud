using DTO.Models;
using DTO.Repository;
using Microsoft.AspNetCore.Mvc;
using ShareFileCloud.Services.Auth;
using ShareFileCloud.SimpleModels.Auth;

namespace ShareFileCloud.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        private readonly UsersRepository _usersRepository;
       


        public AuthController(
            AuthService authService,
            UsersRepository usersRepository
            )
        {
            _authService = authService;
            _usersRepository = usersRepository;
        }

#if DEBUG
        [HttpPost("debug-create-user")]
        public async Task<ActionResult<ApplicationUserDTO>> CreateUserAsync([FromBody] Login model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await _usersRepository.CreateAsync(new DTO.Models.ApplicationUserDTO()
            {
                UserName = model.UserName,
                PasswordHash = _authService.HashPassword(model.Password ?? string.Empty)
            }, string.Empty);
            return Ok(user);
        }
#endif


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var answer = await _authService.LoginAsync(model);
            return answer.State ? Ok(answer) : BadRequest(answer);
        }


        /*
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            return await _authService.RegisterAsync(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            return await _authService.LogoutAsync();
        }*/
    }
}
