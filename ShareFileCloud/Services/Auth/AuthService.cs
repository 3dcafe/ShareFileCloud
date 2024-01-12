using CafeExtensions.Exceptions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using ShareFileCloud.SimpleModels.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShareFileCloud.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IStringLocalizer<SharedResource> _localization;
    private readonly IConfiguration _config;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<SharedResource> localization,
        IConfiguration config,
        SignInManager<ApplicationUser> signInManager
        )
    {
        _userManager = userManager;
        _localization = localization;
        _config = config;
        _signInManager = signInManager;
    }

#warning Document this method
#warning write test for this method
    public async Task<LoginSimpleAnswer> LoginAsync(SimpleModels.Auth.Login model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if(user == null)
            return new LoginSimpleAnswer { State = false, Error = _localization["ErrorTextUserNotFound"] };
        if (user.IsDeleted)
            return new LoginSimpleAnswer { State = false, Error = _localization["TextErrorLockAdmin"] };
        if (user.LockoutEnabled)
            return new LoginSimpleAnswer { State = false, Error = _localization["TextErrorLockAdmin"] };

        if (model.Password != null && HashPassword(model.Password) == user.PasswordHash)
        {
            var token = GetJwtSecurityToken(user);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new LoginSimpleAnswer { State = true, JWT = jwtToken, Role = user.RoleName, Error = string.Empty };
        }
        return new LoginSimpleAnswer { State = false, Error = _localization["ErrorTextUserNotFound"] };
    }


    public async Task<IActionResult> LogoutAsync()
    {
        //todo
#warning todo this method
        return null;
    }

    public Task<IActionResult> RegisterAsync(Register model)
    {
#warning todo this method
        throw new NotImplementedException();
    }


    /// <summary>
    /// Calculate hash password
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    string HashPassword(string password)
    {
#warning take from ENV or file ?
        var salt = Encoding.ASCII.GetBytes("testtesttest");
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        return hashed;
    }

    /// <summary>
    /// Generate JWT Token for user login
    /// </summary>
    /// <param name="user"></param>
    /// <param name="organizationId"></param>
    /// <returns></returns>
    JwtSecurityToken GetJwtSecurityToken(ApplicationUser? user)
    {
        if (user == null)
            throw new AccessErrorException($"error - user can not be null");
        var claims = new[]
        {
            new Claim("Id",user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, (user.UserName ?? string.Empty) ),
            new Claim("role",value: user.RoleName ?? "user"),
            new Claim(JwtRegisteredClaimNames.UniqueName, (user.UserName ?? string.Empty)),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? String.Empty)
        };
        #warning take from ENV or file ?
        string key = _config.GetValue<string>("SigningKey");
        var token = new JwtSecurityToken
        (
            issuer: _config.GetValue<string>("Issuer"),
            audience: _config.GetValue<string>("Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddMonths(12),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha512)
        );
        return token;
    }

}