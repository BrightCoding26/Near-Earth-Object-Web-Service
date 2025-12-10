using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NearEarthObjectsWebService.Controllers;
using NearEarthObjectsWebService.Dto.V1;
using NearEarthObjectsWebService.Model;
using NearEarthObjectsWebService.Services.Interfaces;
using NearEarthObjectsWebServiceTests.TestUtility;

namespace NearEarthObjectsWebServiceTests.Controllers;

public class NearEarthObjectsControllerTests
{
    private readonly Mock<ILogger<NearEarthObjectsController>> _loggerMock = new();
    private readonly Mock<INearEarthObjectsService> _nearEarthObjectsServiceMock = new();

    private readonly NearEarthObjectsController _nearEarthObjectsController;

    public NearEarthObjectsControllerTests()
    {
        _nearEarthObjectsController = new NearEarthObjectsController(_loggerMock.Object, _nearEarthObjectsServiceMock.Object);
    }

    [Fact]
    public async Task LargestNeoDuringBirthWeekV1_ValidBirthDate_LargestNeoDuringBirthWeekShouldBeReturned()
    {
        // Arrange
        var birthDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var expectedNeo = EntityBuilder.NearEarthObjectV1();
        var expectedResponse = new OkObjectResult(expectedNeo);
        DateTime? getLargestNeoDuringBirthWeekParam = null;

        _nearEarthObjectsServiceMock.Setup(s => s.GetLargestNeoDuringBirthWeek(It.IsAny<DateTime>()))
            .Callback((DateTime date) => getLargestNeoDuringBirthWeekParam = date)
            .ReturnsAsync(new Result<NearEarthObject>
            {
                Content = expectedNeo,
                Success = true,
                HttpStatusCode = HttpStatusCode.OK
            })
            .Verifiable(Times.Once);

        // Act
        var result = await _nearEarthObjectsController.LargestNeoDuringBirthWeekV1(birthDate);

        // Assert
        ComparisonHelper.AssertComparisonResultAndIgnoreObjectTypes(expectedResponse, result);
        Assert.Equal(birthDate, getLargestNeoDuringBirthWeekParam);
    }
}
