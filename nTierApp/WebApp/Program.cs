
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Repositories;
using WebApp.Extensions;

var logger = NLog.LogManager
    .Setup()
    .LoadConfigurationFromAppSettings()
    .GetCurrentClassLogger();

logger.Debug("Init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddControllersWithViews();
    builder.Services.ConfigureDbContext(builder.Configuration);
    builder.Services.ConfigureServices();
    builder.Services.ConfigureRepositories();

    builder.Logging.ClearProviders();
    builder.UseNLog();

    var app = builder.Build();

    app.UseStaticFiles();
    app.MapControllers();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Book}/{action=Index}/{id?}"
    );

    app.MapGet("/books/context", (RepositoryContext context) =>
        Results.Ok(context.Books.ToList())
    );

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Bir hata oluþtu ve uygulama durdu!");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}


