using Blog.AutoMapper.Extensions;
using Blog.Bases.Settings;
using Blog.Services.Extensions;
using Blog.Services.Helpers;
using Blog.Translations.Extensions;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Settings configuration
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(SettingsConstants.AppSettings, optional: false, reloadOnChange: true)
            .AddJsonFile(SettingsConstants.DatabaseSettings, optional: false, reloadOnChange: true)
            .AddJsonFile(SettingsConstants.JwtSettings, optional: false, reloadOnChange: true);
        var configuration = configBuilder.Build();

        builder.Services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        builder.Services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
        builder.Services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

        DatabaseSettings databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
        JwtSettings jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        #endregion

        #region Extended Services configuration
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddMappings();
        builder.Services.AddTranslation();
        builder.Services.AddServices(databaseSettings);
        builder.Services.AddJwtConfiguration(jwtSettings);
        #endregion

        builder.Services.AddControllers();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Dev", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            options.AddPolicy("Prod", builder =>
                builder.WithOrigins("https://localhost:5001")
                    //.WithMethods("POST")
                    .AllowAnyHeader());
        });

        //builder.Services.AddControllers(options =>
        //{
        //    options.Filters.Add<LoggingExceptionFilter>();
        //    options.Filters.Add<LoggingActionFilter>();
        //});
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Token JWT: Bearer {seu token}",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("Dev");
        }
        else
        {
            app.UseCors("Prod");
        }

        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        if (app.Environment.IsDevelopment())
        {
            DbMigrationHelper.SeedDataAsync(app).Wait();
        }

        app.Run();
    }
}