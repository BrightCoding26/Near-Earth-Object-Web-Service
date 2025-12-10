using NearEarthObjectsWebService.Utility;

namespace NearEarthObjectsWebServiceTests.Utility;

public class DateHelperTests
{
    [Fact]
    public void IsDateRangeExceeded_DateRangeIsExceeded_ReturnsTrue()
    {
        // Arrange
        var startDateTime = new DateTime(2016, 04, 03, 0, 0, 0, DateTimeKind.Utc);
        var endDateTime = new DateTime(2016, 04, 11, 0, 0, 0, DateTimeKind.Utc);

        // Act
        var result = DateHelper.IsDateRangeExceeded(startDateTime, endDateTime);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsDateRangeExceeded_DatesAre7daysApart_ReturnsFalse()
    {
        // Arrange
        var startDateTime = new DateTime(2016, 04, 03, 0, 0, 0, DateTimeKind.Utc);
        var endDateTime = new DateTime(2016, 04, 10, 0, 0, 0, DateTimeKind.Utc);

        // Act
        var result = DateHelper.IsDateRangeExceeded(startDateTime, endDateTime);

        // Assert
        Assert.False(result);
    }
}
