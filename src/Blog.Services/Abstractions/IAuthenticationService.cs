using Blog.Bases.Services;
using Blog.Entities.Authentication;
using Blog.Services.Entities;

namespace Blog.Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<ServiceOutput<AuthenticationOutput>> RegisterUserAsync(bool generateToken, ServiceInput<AuthenticationInput> registerUser);
        Task<ServiceOutput<AuthenticationOutput>> LoginUserAsync(bool generateToken, ServiceInput<AuthenticationInput> loginUser);
    }
}
