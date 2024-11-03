using Blog.Bases.Services;
using Blog.Entities.Authentication;
using Blog.Services.Abstractions;
using Blog.Services.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Area("auth")]
    [Route("api/v1/[area]")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<AuthenticationOutput>>> RegisterUserAsync([FromBody] ServiceInput<AuthenticationInput> registerUser)
        {
            var result = await _authenticationService.RegisterUserAsync(true, registerUser);
            return GenerateResponse(result, StatusCodes.Status200OK);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ServiceOutput<AuthenticationOutput>>> LoginUserAsync([FromBody] ServiceInput<AuthenticationInput> loginUser)
        {
            var result = await _authenticationService.LoginUserAsync(true, loginUser);
            return GenerateResponse(result, StatusCodes.Status200OK);
        }
    }
}
