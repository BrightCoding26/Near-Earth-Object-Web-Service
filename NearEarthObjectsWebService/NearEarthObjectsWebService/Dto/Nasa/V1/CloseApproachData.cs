using System.Text.Json.Serialization;

namespace NearEarthObjectsWebService.Dto.Nasa.V1;

public record CloseApproachData
{
    [JsonPropertyName("close_approach_data")]
    public string? CloseApproachDate { get; set; }

    [JsonPropertyName("close_approach_date_full")]
    public string? CloseApproachDateFull { get; set; }

    [JsonPropertyName("epoch_date_close_approach")]
    public long? EpochDateCloseApproach { get; set; }

    [JsonPropertyName("relative_velocity")]
    public required RelativeVelocity RelativeVelocity { get; set; }

    [JsonPropertyName("miss_distance")]
    public required MissDistance MissDistance { get; set; }

    [JsonPropertyName("orbiting_body")]
    public required string OrbitingBody { get; set; }
}
