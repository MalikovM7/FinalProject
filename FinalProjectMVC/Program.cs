using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using FluentValidation.AspNetCore;
using FluentValidation;
using Domain.Helpers;
using FinalProjectMVC.Services.Implementations;
using Persistence.Data;
using Domain.Identity;
using FinalProjectMVC.FluentValidation.TestimonialsValidation;
using Persistence.Repositories.Implementations;
using Repositories.Repositories;
using Services.Implementations.Implementations;
using Services.Interfaces;

namespace FinalProjectMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure services
            ConfigureServices(builder);

            var app = builder.Build();

            // Configure middleware
            ConfigureMiddleware(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Connection string
            var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection")
                ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

            // Database context
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            // SMTP Configuration
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Smtp"));

            // Authentication and Identity
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // FluentValidation
            builder.Services.AddFluentValidationAutoValidation(options => options.DisableDataAnnotationsValidation = true);
            builder.Services.AddValidatorsFromAssemblyContaining<TestimonialAddValidation>();

            // Add services and repositories
            RegisterServices(builder.Services);
            RegisterRepositories(builder.Services);

            // Add controllers with views
            builder.Services.AddControllersWithViews();
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ITestimonialService, TestimonialService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IHomePreviewService, HomePreviewService>();
            services.AddScoped<IAboutUsService, AboutUsService>();
            services.AddScoped<IfaqService, FaqService>();
            services.AddScoped<IVehicleService, VehicleService>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IFaqRepository, FAQRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IHomePreviewRepository, HomePreviewRepository>();
            services.AddScoped<IAboutUsRepository, AboutUsRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ITestimonialRepository, TestimonialRepository>();
        }
    }
}
