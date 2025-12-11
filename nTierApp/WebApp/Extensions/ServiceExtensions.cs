using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using System.Text;

namespace WebApp.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                mig => mig.MigrationsAssembly("Repositories"));

            options.EnableSensitiveDataLogging();
        });
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookManager>();
        services.AddScoped<ICategoryService, CategoryManager>();
        services.AddScoped<IAuthorService, AuthorManager>();
        services.AddScoped<IServiceManager, ServiceManager>();
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    // ConfigureCors
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options => {
            
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });

            // Sadece belirli kaynaklara izin vermek için:
            options.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins("https://www.my-frontend.com", "http://localhost:3000") // Sadece bu kaynaklara izin ver.
                       .WithMethods("GET", "POST", "PUT") // Sadece belirli metotlara izin ver.
                       .WithHeaders("Content-Type", "Authorization")); // Sadece belirli başlıklara izin ver.
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        // Identity yapılandırması buraya eklenebilir.
        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        })
        .AddEntityFrameworkStores<RepositoryContext>()
        .AddDefaultTokenProviders();
    }


    // ConfigureApplicationCookie
    public static void ConfigureApplicationCookie(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login"; // Giriş sayfası yolu
            options.AccessDeniedPath = "/Account/AccessDenied"; // Erişim reddedildi sayfası yolu
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Çerez süresi
            options.SlidingExpiration = true; // Kaydırmalı süre yenileme
        });
    }

    // ConfigureJWT
    public static void ConfigureJWT(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["Secret"] ?? String.Empty;

        services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["ValidIssuer"],
                    ValidAudience = jwtSettings["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
    }
}
