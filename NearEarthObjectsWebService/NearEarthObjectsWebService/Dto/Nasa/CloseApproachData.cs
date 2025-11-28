namespace NearEarthObjectsWebService.Dto.Nasa;

public record CloseApproachData
{
    public string? CloseApproachDate { get; set; }

    public string? CloseApproachDateFull { get; set; }

    public long? EpochDateCloseApproach { get; set; }

    public required RelativeVelocity RelativeVelocity { get; set; }

    public required MissDistance MissDistance { get; set; }

    public required string OrbitingBody { get; set; }
}

public record RelativeVelocity
{
    public string? KilometersPerSecond { get; set; }

    public string? KilometersPerHour { get; set; }

    public string? MilesPerHour { get; set; }
}

public record MissDistance
{
    public string? Astronomical { get; set; }

    public string? Lunar { get; set; }

    public string? Kilometers { get; set; }

    public string? Miles { get; set; }
}
