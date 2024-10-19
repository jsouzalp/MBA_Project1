using Blog.Bases.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Area("auth")]
    [Route("api/v1/[area]")]
    public class AuthenticationController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<JwtSettings> jwtSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        //[HttpPost("register")]
        //public async Task<ActionResult> RegisterUserAsync(RegisterUserViewModel registerUser)
        //{
        //    if (!ModelState.IsValid) return ValidationProblem(ModelState);

        //    var user = new IdentityUser
        //    {
        //        UserName = registerUser.Email,
        //        Email = registerUser.Email,
        //        EmailConfirmed = true
        //    };

        //    var result = await _userManager.CreateAsync(user, registerUser.Password);

        //    if (result.Succeeded)
        //    {
        //        await _signInManager.SignInAsync(user, false);
        //        return Ok(await GenerateJwtAsync(user.Email));
        //    }

        //    return Problem("Falha ao registrar o usuário");
        //}

        //[HttpPost("login")]
        //public async Task<ActionResult> LoginUserAsync(LoginUserViewModel loginUser)
        //{
        //    if (!ModelState.IsValid) return ValidationProblem(ModelState);

        //    var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

        //    if (result.Succeeded)
        //    {
        //        return Ok(await GenerateJwtAsync(loginUser.Email));
        //    }

        //    return Problem("Usuário ou senha incorretos");
        //}

        //private async Task<string> GenerateJwtAsync(string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    var roles = await _userManager.GetRolesAsync(user);

        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.UserName)
        //    };

        //    // Adicionar roles como claims
        //    foreach (var role in roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    }

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        //    var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Issuer = _jwtSettings.Issuer,
        //        Audience = _jwtSettings.Audience,
        //        Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationInHours),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    });

        //    var encodedToken = tokenHandler.WriteToken(token);

        //    return encodedToken;
        //}
    }
}
