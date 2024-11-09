using Blog.Bases.Settings;
using Blog.Repositories.Extensions;
using Blog.Services.Abstractions;
using Blog.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Blog.Validations.Extensions;
using FluentValidation;
using Blog.Validations.Validations.AuthorValidation;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Blog.Services.AutoMapper;

namespace Blog.Services.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, DatabaseSettings databaseSettings)
        {
            services.AddValidationFactory();
            services.AddValidatorsFromAssemblyContaining<AuthorValidation>();
            services.AddRepositories(databaseSettings);
            services.AddMappings();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }

        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, JwtSettings jwtSettings)
        {
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidIssuer = jwtSettings.Issuer
                };
            });

            return services;
        }

        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AuthorProfile));
            services.AddAutoMapper(typeof(PostProfile));
            services.AddAutoMapper(typeof(CommentProfile));

            return services;
        }
    }
}
