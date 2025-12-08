namespace NearEarthObjectsWebService.Dto.V1;

public record NearEarthObject
{
    public required string Name { get; set; }

    public required string NasaJplUrl { get; set; }

    public required double AbsoluteMagnitudeH { get; set; }

    public required EstimatedDiameter EstimatedDiameter { get; set; }

    public required bool IsPotentiallyHazardousAsteroid { get; set; }

    public required List<CloseApproachData> CloseApproachData { get; set; }

    public required bool IsSentryObject { get; set; }

    public string? SentryData { get; set; }
}
