
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Repositories;
using WebApp.Extensions;
using WebApp.Middleware;

var logger = NLog.LogManager
    .Setup()
    .LoadConfigurationFromAppSettings()
    .GetCurrentClassLogger();

logger.Debug("Init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers(options =>
    {
        options.RespectBrowserAcceptHeader = true;
        options.ReturnHttpNotAcceptable = true; // 406
    })
    .AddXmlDataContractSerializerFormatters(); // XML desteði ekle


    builder.Services.AddControllersWithViews();
    builder.Services.ConfigureDbContext(builder.Configuration);
    builder.Services.ConfigureServices();
    builder.Services.ConfigureRepositories();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.ConfigureCors();

    builder.Logging.ClearProviders();
    builder.UseNLog();

    var app = builder.Build();
    app.UseExceptionHandler(_ => { });
    app.UseStaticFiles();
    app.MapControllers();

    app.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Book}/{action=Index}/{id?}"
    );

    if(app.Environment.IsDevelopment())
    {
        app.UseCors("AllowAll");
    }

    if(app.Environment.IsProduction())
    {
        app.UseCors("CorsPolicy");
    }
    
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


