using Blog.Bases;
using Blog.Bases.Services;
using Blog.Bases.Settings;
using Blog.Entities.Authentication;
using Blog.Entities.Authors;
using Blog.Services.Abstractions;
using Blog.Services.Entities;
using Blog.Translations.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Translations.Constants;
using Blog.Validations.Abstractions;
using Blog.Validations;

namespace Blog.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthorService _authorService;
        private readonly ITranslationResource _translationResource;
        private readonly IValidationFactory<AuthenticationInput> _authenticationValidation;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(IAuthorService authorService,
            ITranslationResource translationResource,
            IValidationFactory<AuthenticationInput> authenticationValidation,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<JwtSettings> jwtSettings)
        {
            _authorService = authorService;
            _translationResource = translationResource;
            _authenticationValidation = authenticationValidation;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<ServiceOutput<AuthenticationOutput>> RegisterUserAsync(bool generateToken, ServiceInput<AuthenticationInput> registerUser)
        {
            ValidationOutput validation = await _authenticationValidation.ValidateAsync(registerUser.Input);
            ServiceOutput<AuthenticationOutput> result = new();

            if (validation.Success)
            {
                var user = new IdentityUser()
                {
                    UserName = registerUser.Input.Email,
                    Email = registerUser.Input.Email,
                    NormalizedUserName = registerUser.Input.FullName,
                    EmailConfirmed = true
                };

                var resultIdentity = await _userManager.CreateAsync(user, registerUser.Input.Password);

                if (resultIdentity.Succeeded)
                {
                    var resultAuthor = await _authorService.CreateAuthorAsync(new ServiceInput<AuthorInput>()
                    {
                        Input = new AuthorInput()
                        {
                            Id = Guid.Parse(user.Id),
                            IdentityUser = Guid.Parse(user.Id),
                            Name = registerUser.Input.FullName
                        }
                    });

                    result.Message = resultAuthor.Message;
                    result.Errors = resultAuthor.Errors;

                    if (resultAuthor.Success)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: true);

                        result.Output = new AuthenticationOutput()
                        {
                            Id = Guid.Parse(user.Id),
                            Email = registerUser.Input.Email,
                            AccessToken = generateToken ? await GenerateJwtAsync(user) : string.Empty
                        };
                    }
                }
                else
                {
                    result.Errors = (from x in resultIdentity.Errors
                                     select new ErrorBase()
                                     {
                                         Code = x.Code,
                                         Message = x.Description
                                     }).ToList();
                }
            }
            else
            {
                result.Errors = validation.Errors;
            }

            return result;
        }

        public async Task<ServiceOutput<AuthenticationOutput>> LoginUserAsync(bool generateToken, ServiceInput<AuthenticationInput> loginUser)
        {
            ServiceOutput<AuthenticationOutput> result = new();
            var resultIdentity = await _signInManager.PasswordSignInAsync(loginUser.Input.Email, loginUser.Input.Password, true, false);

            if (resultIdentity.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginUser.Input.Email);
                await _signInManager.SignInAsync(user, isPersistent: true);

                result.Message = _translationResource.GetResource(AuthenticationConstant.ServiceLoginSuccess);
                result.Output = new AuthenticationOutput()
                {
                    Id = Guid.Parse(user.Id),
                    Email = loginUser.Input.Email,
                    AccessToken = generateToken ? await GenerateJwtAsync(user) : string.Empty
                };
            }
            else
            {
                ErrorBase error = new();
                if (resultIdentity.IsNotAllowed)
                {
                    error.Code = _translationResource.GetCodeResource(AuthenticationConstant.ServiceLoginNotAllowed);
                    error.Message = _translationResource.GetResource(AuthenticationConstant.ServiceLoginNotAllowed);
                }
                else if (resultIdentity.IsLockedOut)
                {
                    error.Code = _translationResource.GetCodeResource(AuthenticationConstant.ServiceLoginBlocked);
                    error.Message = _translationResource.GetResource(AuthenticationConstant.ServiceLoginBlocked);
                }
                else
                {
                    error.Code = _translationResource.GetCodeResource(AuthenticationConstant.ServiceLoginNotDefined);
                    error.Message = _translationResource.GetResource(AuthenticationConstant.ServiceLoginNotDefined);
                }

                result.Errors = new List<ErrorBase>() { error };
            }
            return result;
        }

        private async Task<string> GenerateJwtAsync(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            // Adicionar roles como claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }
    }
}
