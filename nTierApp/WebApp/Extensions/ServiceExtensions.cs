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
}
