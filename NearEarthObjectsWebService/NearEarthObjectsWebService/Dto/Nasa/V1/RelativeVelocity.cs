using System.Text.Json.Serialization;

namespace NearEarthObjectsWebService.Dto.Nasa.V1;

public record RelativeVelocity
{
    [JsonPropertyName("kilometers_per_second")]
    public string? KilometersPerSecond { get; set; }

    [JsonPropertyName("kilometers_per_hour")]
    public string? KilometersPerHour { get; set; }

    [JsonPropertyName("miles_per_hour")]
    public string? MilesPerHour { get; set; }
}
