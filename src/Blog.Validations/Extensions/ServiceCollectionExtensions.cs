using Blog.Validations.Abstractions;
using Blog.Validations.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Validations.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddValidationFactory(this IServiceCollection services)
        {
            services.AddScoped(typeof(IValidationFactory<>), typeof(ValidationFactory<>));
            return services;
        }
    }
}
