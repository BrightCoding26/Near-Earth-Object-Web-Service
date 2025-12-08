using System.Net;
using HttpExceptions.Exceptions;
using NearEarthObjectsWebService.Utility;

namespace NearEarthObjectsWebService.Middlewares;

public sealed class ExceptionMiddleware(RequestDelegate next, ILoggerFactory factory)
{
    private readonly ILogger<ExceptionMiddleware> _logger = factory.CreateLogger<ExceptionMiddleware>();

    public async Task Invoke(HttpContext context)
    {
        try
        {
            _logger.LogDebug(
                "Attempting to access Request {Request} within Middleware {Middleware}",
                context.Request,
                nameof(ExceptionMiddleware));

            await next(context);
        }
        catch (HttpException httpEx)
        {
            context.Response.StatusCode = (int)httpEx.StatusCode;
            await context.Response.WriteAsJsonAsync($"{httpEx.Message}");
        }
        catch (NasaApiException nasaEx)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadGateway;
            await context.Response.WriteAsJsonAsync($"{nasaEx.Message}");
        }
        catch (Exception ex)
        {
            var traceIdGuid = Guid.NewGuid();
            _logger.LogError(
                ex,
                "An error was caught by the {Middleware} for TraceId " +
                "{Guid} accessing Request {Request}: Exception - {Message}",
                nameof(ExceptionMiddleware),
                traceIdGuid,
                context.Request,
                ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync($"An error occurred in the service. TraceId: {traceIdGuid}");
        }
    }
}
