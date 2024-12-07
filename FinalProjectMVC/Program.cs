using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FinalProjectMVC.Helpers;
using FinalProjectMVC.Services.Implementations;
using FinalProjectMVC.Data;
using FinalProjectMVC.Identity;
using FinalProjectMVC.Repositories.Implementations;
using Services.Interfaces;
using Repositories.Repositories;
namespace FinalProjectMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);


            var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Smtp"));

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));



            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

            })
                
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<IFaqRepository, FAQRepository>();
            builder.Services.AddScoped<IHomePreviewRepository, HomePreviewRepository>();
            builder.Services.AddScoped<IAboutUsRepository, AboutUsRepository>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IHomePreviewService, HomePreviewService>();
            builder.Services.AddScoped<IAboutUsService, AboutUsService>();
            builder.Services.AddScoped<IfaqService, FaqService>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });

            app.Run();
        }
    }
}
