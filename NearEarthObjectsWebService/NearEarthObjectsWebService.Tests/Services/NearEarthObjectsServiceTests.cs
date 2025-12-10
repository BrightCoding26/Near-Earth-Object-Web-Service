using System.Net;
using Moq;
using NearEarthObjectsWebService.Dto.Nasa.V1;
using NearEarthObjectsWebService.Model;
using NearEarthObjectsWebService.Services;
using NearEarthObjectsWebService.Services.Interfaces;
using NearEarthObjectsWebServiceTests.TestUtility;
using NearEarthObject = NearEarthObjectsWebService.Dto.V1.NearEarthObject;

namespace NearEarthObjectsWebServiceTests.Services;

public class NearEarthObjectsServiceTests
{
    private readonly Mock<INasaService> _nasaService = new();
    private readonly NearEarthObjectsService _nearEarthObjectsService;

    public NearEarthObjectsServiceTests()
    {
        _nearEarthObjectsService = new NearEarthObjectsService(_nasaService.Object);
    }

    [Fact]
    public async Task LargestNeoDuringBirthWeekV1_ValidBirthDate_LargestNeoDuringBirthWeekShouldBeReturned()
    {
        // Arrange
        var birthDate = new DateTime(2025, 11, 25, 0, 0, 0, DateTimeKind.Utc);
        var expectedNeo = EntityBuilder.NearEarthObjectV1();
        var expectedResponse = new Result<NearEarthObject>
        {
            Content = expectedNeo,
            Success = true,
            HttpStatusCode = HttpStatusCode.OK
        };
        var expectedStartDate = new DateTime(2025, 11, 23, 0, 0, 0, DateTimeKind.Utc);
        var expectedEndDate = new DateTime(2025, 11, 29, 0, 0, 0, DateTimeKind.Utc);
        (DateTime? StartDate, DateTime? EndDate)? getNeosByApproachDateAsyncParams = null;

        _nasaService.Setup(s => s.GetNeosByApproachDateAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Callback((DateTime? startDate, DateTime? endDate) =>
                getNeosByApproachDateAsyncParams = (startDate, endDate))
            .ReturnsAsync(new Result<NeoByApproachDateResponse>
            {
                Content = EntityBuilder.NeoByApproachDateResponse(),
                Success = true,
                HttpStatusCode = HttpStatusCode.OK
            })
            .Verifiable(Times.Once);

        // Act
        var result = await _nearEarthObjectsService.GetLargestNeoDuringBirthWeek(birthDate);

        // Assert
        ComparisonHelper.AssertComparisonResultAndIgnoreObjectTypes(expectedResponse, result);
        Assert.Equal(expectedStartDate, getNeosByApproachDateAsyncParams!.Value.StartDate);
        Assert.Equal(expectedEndDate, getNeosByApproachDateAsyncParams!.Value.EndDate);
    }

    [Fact]
    public async Task LargestNeoDuringBirthWeekV1_NasaApiReturnsNoContent_ExitsMethodEarly()
    {
        // Arrange
        var birthDate = new DateTime(2025, 11, 25, 0, 0, 0, DateTimeKind.Utc);
        var expectedResponse = new Result<NearEarthObject>
        {
            Success = true,
            HttpStatusCode = HttpStatusCode.NoContent
        };
        var expectedStartDate = new DateTime(2025, 11, 23, 0, 0, 0, DateTimeKind.Utc);
        var expectedEndDate = new DateTime(2025, 11, 29, 0, 0, 0, DateTimeKind.Utc);
        (DateTime? StartDate, DateTime? EndDate)? getNeosByApproachDateAsyncParams = null;

        _nasaService.Setup(s => s.GetNeosByApproachDateAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Callback((DateTime? startDate, DateTime? endDate) =>
                getNeosByApproachDateAsyncParams = (startDate, endDate))
            .ReturnsAsync(new Result<NeoByApproachDateResponse>
            {
                Success = true,
                HttpStatusCode = HttpStatusCode.NoContent
            })
            .Verifiable(Times.Once);

        // Act
        var result = await _nearEarthObjectsService.GetLargestNeoDuringBirthWeek(birthDate);

        // Assert
        ComparisonHelper.AssertComparisonResultAndIgnoreObjectTypes(expectedResponse, result);
        Assert.Equal(expectedStartDate, getNeosByApproachDateAsyncParams!.Value.StartDate);
        Assert.Equal(expectedEndDate, getNeosByApproachDateAsyncParams!.Value.EndDate);
    }

    [Fact]
    public async Task LargestNeoDuringBirthWeekV1_ReturnsNasaCallFails_ExitsMethodEarly()
    {
        // Arrange
        var birthDate = new DateTime(2025, 11, 25, 0, 0, 0, DateTimeKind.Utc);
        var expectedResponse = new Result<NearEarthObject>
        {
            Success = false,
            HttpStatusCode = HttpStatusCode.NotFound
        };
        var expectedStartDate = new DateTime(2025, 11, 23, 0, 0, 0, DateTimeKind.Utc);
        var expectedEndDate = new DateTime(2025, 11, 29, 0, 0, 0, DateTimeKind.Utc);
        (DateTime? StartDate, DateTime? EndDate)? getNeosByApproachDateAsyncParams = null;

        _nasaService.Setup(s => s.GetNeosByApproachDateAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Callback((DateTime? startDate, DateTime? endDate) =>
                getNeosByApproachDateAsyncParams = (startDate, endDate))
            .ReturnsAsync(new Result<NeoByApproachDateResponse>
            {
                Success = false,
                HttpStatusCode = HttpStatusCode.NotFound
            })
            .Verifiable(Times.Once);

        // Act
        var result = await _nearEarthObjectsService.GetLargestNeoDuringBirthWeek(birthDate);

        // Assert
        ComparisonHelper.AssertComparisonResultAndIgnoreObjectTypes(expectedResponse, result);
        Assert.Equal(expectedStartDate, getNeosByApproachDateAsyncParams!.Value.StartDate);
        Assert.Equal(expectedEndDate, getNeosByApproachDateAsyncParams!.Value.EndDate);
    }

    [Fact]
    public async Task LargestNeoDuringBirthWeekV1_ReturnsSuccessButContentIsNull_ExceptionIsThrown()
    {
        // Arrange
        var birthDate = new DateTime(2025, 11, 25, 0, 0, 0, DateTimeKind.Utc);

        var expectedStartDate = new DateTime(2025, 11, 23, 0, 0, 0, DateTimeKind.Utc);
        var expectedEndDate = new DateTime(2025, 11, 29, 0, 0, 0, DateTimeKind.Utc);
        (DateTime? StartDate, DateTime? EndDate)? getNeosByApproachDateAsyncParams = null;

        _nasaService.Setup(s => s.GetNeosByApproachDateAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Callback((DateTime? startDate, DateTime? endDate) =>
                getNeosByApproachDateAsyncParams = (startDate, endDate))
            .ReturnsAsync(new Result<NeoByApproachDateResponse>
            {
                Success = true,
                HttpStatusCode = HttpStatusCode.OK
            })
            .Verifiable(Times.Once);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _nearEarthObjectsService.GetLargestNeoDuringBirthWeek(birthDate));

        Assert.Equal(expectedStartDate, getNeosByApproachDateAsyncParams!.Value.StartDate);
        Assert.Equal(expectedEndDate, getNeosByApproachDateAsyncParams!.Value.EndDate);
    }
}
