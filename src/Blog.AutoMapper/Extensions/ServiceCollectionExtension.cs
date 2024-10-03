using Blog.AutoMapper.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.AutoMapper.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AuthorProfile));
            services.AddAutoMapper(typeof(PostProfile));
            services.AddAutoMapper(typeof(CommentProfile));

            return services;
        }
    }
}
