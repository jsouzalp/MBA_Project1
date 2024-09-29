using Blog.Api.Helpers;
using Blog.Bases.Settings;
using Blog.Services.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Settings configuration
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(SettingsConstants.AppSettings, optional: false, reloadOnChange: true)
            .AddJsonFile(SettingsConstants.DatabaseSettings, optional: false, reloadOnChange: true);
        var configuration = configBuilder.Build();

        builder.Services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        builder.Services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));

        DatabaseSettings databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
        #endregion

        #region Extended Services configuration
        builder.Services.AddServices(databaseSettings);
        #endregion

        builder.Services.AddControllers();
        //builder.Services.AddControllers(options =>
        //{
        //    options.Filters.Add<LoggingExceptionFilter>();
        //    options.Filters.Add<LoggingActionFilter>();
        //});
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();
        //app.UseMiddleware<RequestInterceptingMiddleware>();
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