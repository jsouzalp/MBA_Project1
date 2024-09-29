using Blog.Validations.Abstractions;
using Blog.Validations.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Blog.Validations.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddValidationFactory(this IServiceCollection services)
        {
            services.TryAddSingleton(typeof(IValidationFactory<,>), typeof(ValidationFactory<,>));
            return services;
        }
    }
}
