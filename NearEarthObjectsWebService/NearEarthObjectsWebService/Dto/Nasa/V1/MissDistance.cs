namespace NearEarthObjectsWebService.Dto.Nasa;

public record MissDistance
{
    public string? Astronomical { get; set; }

    public string? Lunar { get; set; }

    public string? Kilometers { get; set; }

    public string? Miles { get; set; }
}
