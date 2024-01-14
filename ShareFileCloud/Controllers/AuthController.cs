using DTO.Models;
using DTO.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShareFileCloud.Services.Auth;
using ShareFileCloud.SimpleModels.Auth;
using System.Security.Claims;

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
        public async Task<ActionResult<ApplicationUserDTO>> CreateUserAsync([FromBody] Login login)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await _usersRepository.CreateAsync(new DTO.Models.ApplicationUserDTO()
            {
                UserName = login.UserName,
                PasswordHash = _authService.HashPassword(login.Password ?? string.Empty)
            }, string.Empty);
            return Ok(user);
        }
#endif


        /*
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var answer = await _authService.LoginAsync(model);
            return answer.State ? Ok(answer) : BadRequest(answer);
        }


        [HttpPost("logout")]
        [Authorize]
        public ActionResult Logout()
        {
            var userName = User.Identity?.Name!;
            jwtAuthManager.RemoveRefreshTokenByUserName(userName);
            logger.LogInformation("User [{userName}] logged out the system.", userName);
            return Ok();
        }

        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var userName = User.Identity?.Name!;
                logger.LogInformation("User [{userName}] is trying to refresh JWT token.", userName);

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return Unauthorized();
                }

                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var jwtResult = jwtAuthManager.Refresh(request.RefreshToken, accessToken ?? string.Empty, DateTime.Now);
                logger.LogInformation("User [{userName}] has refreshed JWT token.", userName);
                return Ok(new LoginResult
                {
                    UserName = userName,
                    Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message); // return 401 so that the client side can redirect the user to login page
            }
        }*/



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
