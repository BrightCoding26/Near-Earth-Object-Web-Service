using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NearEarthObjectsWebService.Middlewares;
using NearEarthObjectsWebService.Utility;
using NearEarthObjectsWebServiceTests.TestUtility;

namespace NearEarthObjectsWebServiceTests.Middlewares;

public class ExceptionMiddlewareTests
{
    private readonly Mock<ILogger<ExceptionMiddleware>> _loggerMock = new();

    [Fact]
    public async Task Invoke_NoException_ShouldLogAndProceed()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var invoked = false;

        var middleware = new ExceptionMiddleware(Next, _loggerMock.Object);

        // Act
        await middleware.Invoke(context);

        // Assert
        Assert.True(invoked);
        _loggerMock.VerifyLogs(
            LogLevel.Debug,
            "Attempting to access Microsoft.AspNetCore.Http.DefaultHttpRequest within Middleware ExceptionMiddleware",
            Times.Once);

        return;

        async Task Next(HttpContext _)
        {
            invoked = true;
            await Task.CompletedTask;
        }
    }

    [Fact]
    public async Task Invoke_NasaExceptionOccurs_ReturnNasaExceptionToUser()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;
        var exception = new NasaApiException("NASA API Excepton.");

        var middleware = new ExceptionMiddleware(Next, _loggerMock.Object);

        // Act
        await middleware.Invoke(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();

        Assert.Equal((int)HttpStatusCode.BadGateway, context.Response.StatusCode);
        Assert.Equal(
            "\"An error occurred when retrieving the near earth objects from NASA's API. Message: NASA API Excepton.\"",
            responseBody);

        return;

        Task Next(HttpContext _) => throw exception;
    }

    [Fact]
    public async Task Invoke_UnexpectedExceptionOccurs_LogsErrorAndReturnsGenericMessage()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;
        var exception = new Exception("General Error");

        var middleware = new ExceptionMiddleware(Next, _loggerMock.Object);

        // Act
        await middleware.Invoke(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();

        Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        Assert.Contains("An error occurred in the service. TraceId:", responseBody);
        _loggerMock.VerifyLogs(LogLevel.Error, Times.Once);

        return;

        Task Next(HttpContext _) => throw exception;
    }
}
