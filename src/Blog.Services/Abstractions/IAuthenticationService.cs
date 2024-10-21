using Blog.Bases.Services;
using Blog.Entities.Authentication;
using Blog.Services.Entities;

namespace Blog.Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<ServiceOutput<AuthenticationOutput>> RegisterUserAsync(ServiceInput<AuthenticationInput> registerUser);
        Task<ServiceOutput<AuthenticationOutput>> LoginUserAsync(ServiceInput<AuthenticationInput> loginUser);
    }
}
