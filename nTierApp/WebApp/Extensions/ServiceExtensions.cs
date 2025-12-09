using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;

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
}
