using Blog.Repositories.Abstractions;
using Blog.Repositories.Contexts;
using Blog.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Blog.Bases.Settings;
using Microsoft.AspNetCore.Identity;

namespace Blog.Repositories.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, DatabaseSettings databaseSettings)
        {
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(databaseSettings.ConnectionStringIdentity));

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDbContext<BlogDbContext>(o => o.UseSqlServer(databaseSettings.ConnectionStringBlog));            
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            return services;
        }
    }
}
