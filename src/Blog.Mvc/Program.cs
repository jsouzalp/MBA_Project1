using Blog.Bases.Settings;
using Blog.Mvc.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Blog.AutoMapper.Extensions;
using Blog.Services.Extensions;
using Blog.Translations.Extensions;

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

        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(databaseSettings.ConnectionStringIdentity));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 1;
            options.Password.RequiredUniqueChars = 0;
        }).AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

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

        app.Run();
    }
}