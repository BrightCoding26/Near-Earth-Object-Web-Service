using System.Net;

using NearEarthObjectsWebService.Utility;

namespace NearEarthObjectsWebService.Middlewares;

public sealed class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            logger.LogDebug(
                "Attempting to access {Request} within Middleware {Middleware}",
                context.Request,
                nameof(ExceptionMiddleware));

            await next(context);
        }
        catch (NasaApiException nasaEx)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadGateway;
            await context.Response.WriteAsJsonAsync($"{nasaEx.Message}");
        }
        catch (Exception ex)
        {
            var traceIdGuid = Guid.NewGuid();
            logger.LogError(
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
