using Microsoft.Extensions.Logging;
using Moq;

namespace NearEarthObjectsWebServiceTests.TestUtility;

public static class MockLoggerExtensions
{
    public static void VerifyLogs<T>(this Mock<ILogger<T>> mockLogger, LogLevel logLevel, Func<Times> times)
    {
        var timesAfterBeingInvoked = times.Invoke();
        VerifyLogsNoMessage(mockLogger, logLevel, timesAfterBeingInvoked);
    }

    public static void VerifyLogs<T>(this Mock<ILogger<T>> mockLogger, LogLevel logLevel, Times times)
    {
        VerifyLogsNoMessage(mockLogger, logLevel, times);
    }

    public static void VerifyLogs<T>(this Mock<ILogger<T>> mockLogger, LogLevel logLevel, string message, Func<Times> times)
    {
        var timesAfterBeingInvoked = times.Invoke();
        VerifyLogsWithMessage(mockLogger, logLevel, message, timesAfterBeingInvoked);
    }

    private static void VerifyLogsNoMessage<T>(Mock<ILogger<T>> mockLogger, LogLevel logLevel, Times times)
    {
        mockLogger.Verify(logger => logger.Log(
                It.Is<LogLevel>(ll => ll == logLevel),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
            times);
    }

    private static void VerifyLogsWithMessage<T>(Mock<ILogger<T>> mockLogger, LogLevel logLevel, string message, Times times)
    {
        mockLogger.Verify(
            logger => logger.Log(
                It.Is<LogLevel>(l => l == logLevel),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(message)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
            times);
    }
}
