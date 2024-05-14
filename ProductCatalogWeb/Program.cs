using Microsoft.AspNetCore.Authentication.Cookies;
using ProductCatalogWeb.Services;
using ProductCatalogWeb.Services.Interfaces;
using Serilog;

namespace ProductCatalogWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            StaticData.ProductCatalogAPIBase = builder.Configuration["ServiceUrls:ProductCatalogAPI"];

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration).CreateLogger();
            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();
            builder.Services.AddHttpClient<IAuthService, AuthService>();
            builder.Services.AddHttpClient<IProductService, ProductService>();
            builder.Services.AddHttpClient<ICategoryService, CategoryService>();
            builder.Services.AddHttpClient<IAdminService, AdminService>();

            builder.Services.AddScoped<IBaseService, BaseService>();
            builder.Services.AddScoped<ITokenProvider, TokenProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours(10);
                    options.LoginPath = "/Auth/Login";
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
