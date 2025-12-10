using System.Text.Json.Serialization;

namespace NearEarthObjectsWebService.Dto.Nasa.V1;

public record NearEarthObject
{
    public Links? Links { get; set; }

    public required string Id { get; set; }

    [JsonPropertyName("neo_reference_id")]
    public string? NeoReferenceId { get; set; }

    public required string Name { get; set; }

    [JsonPropertyName("nasa_jpl_url")]
    public required string NasaJplUrl { get; set; }

    [JsonPropertyName("absolute_magnitude_h")]
    public required double AbsoluteMagnitudeH { get; set; }

    [JsonPropertyName("estimated_diameter")]
    public required EstimatedDiameter EstimatedDiameter { get; set; }

    [JsonPropertyName("is_potentially_hazardous_asteroid")]
    public required bool IsPotentiallyHazardousAsteroid { get; set; }

    [JsonPropertyName("close_approach_data")]
    public required List<CloseApproachData> CloseApproachData { get; set; }

    [JsonPropertyName("is_sentry_object")]
    public required bool IsSentryObject { get; set; }

    [JsonPropertyName("sentry_data")]
    public string? SentryData { get; set; }
}
