using Blog.Repositories.Abstractions;
using Blog.Repositories.Contexts;
using Blog.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Blog.Bases.Settings;

namespace Blog.Repositories.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, DatabaseSettings databaseSettings)
        {
            services.AddDbContext<BlogDbContext>(o => o.UseSqlServer(databaseSettings.ConnectionStringPrincipal));            
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            return services;
        }
    }
}
