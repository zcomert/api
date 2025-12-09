using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace WebApp.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        string defaultMessage = "Beklenmeyen bir hata oluştu.";
        // log al
        _logger.LogError(exception, defaultMessage);

        httpContext.Response.ContentType = "application/json";

        var errorDetails = new ErrorDetails();

        (errorDetails.StatusCode, errorDetails.Message) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, defaultMessage)
        };

        httpContext.Response.StatusCode = errorDetails.StatusCode;
        await httpContext.Response.WriteAsync(errorDetails.ToString(), cancellationToken);
        return true;
    }
}
