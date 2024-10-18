using Blog.Bases.Settings;
using Blog.Repositories.Extensions;
using Blog.Services.Abstractions;
using Blog.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Blog.Validations.Extensions;
using FluentValidation;
using Blog.Validations.Validations.AuthorValidation;

namespace Blog.Services.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, DatabaseSettings databaseSettings)
        {
            services.AddValidationFactory();
            services.AddValidatorsFromAssemblyContaining<AuthorValidation>();
            services.AddRepositories(databaseSettings);
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            
            return services;
        }
    }
}
