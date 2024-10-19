using Blog.Bases.Settings;
using Blog.AutoMapper.Extensions;
using Blog.Services.Extensions;
using Blog.Translations.Extensions;
using Blog.Services.Helpers;

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
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddMappings();
        builder.Services.AddTranslation();
        builder.Services.AddServices(databaseSettings);
        #endregion
        
        builder.Services.AddControllersWithViews();
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Post}/{action=Index}/{id?}");
        app.MapRazorPages();

        if (app.Environment.IsDevelopment())
        {
            DbMigrationHelper.SeedDataAsync(app).Wait();
        }

        app.Run();
    }
}