namespace NearEarthObjectsWebService.Dto.V1;

public record RelativeVelocity
{
    public string? KilometersPerSecond { get; set; }

    public string? KilometersPerHour { get; set; }

    public string? MilesPerHour { get; set; }
}
