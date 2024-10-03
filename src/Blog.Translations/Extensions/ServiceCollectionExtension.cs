using Blog.Translations.Abstractions;
using Blog.Translations.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Translations.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTranslation(this IServiceCollection services)
        {
            services.AddSingleton<ITranslationResource, TranslationResource>();
            return services;
        }
    }
}
