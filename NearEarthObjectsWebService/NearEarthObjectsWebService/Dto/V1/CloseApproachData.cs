namespace NearEarthObjectsWebService.Dto.V1;

public record CloseApproachData
{
    public string? CloseApproachDate { get; set; }

    public string? CloseApproachDateFull { get; set; }

    public long? EpochDateCloseApproach { get; set; }

    public required RelativeVelocity RelativeVelocity { get; set; }

    public required MissDistance MissDistance { get; set; }

    public required string OrbitingBody { get; set; }
}
