using Services.Contracts;

namespace WebApp.Extensions;

public static class ApplicationExtensions
{
    public static async Task SeedUsers(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var serviceManager = scope.ServiceProvider
            .GetRequiredService<IServiceManager>();
        await serviceManager.AuthService.SeedUsersAndRolesAsync();
    }
}
