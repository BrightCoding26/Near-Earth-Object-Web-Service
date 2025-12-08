using System.Text.Json.Serialization;

namespace NearEarthObjectsWebService.Dto.Nasa.V1;

public record Diameter
{
    [JsonPropertyName("estimated_diameter_min")]
    public required double EstimatedDiameterMin { get; set; }

    [JsonPropertyName("estimated_diameter_max")]
    public required double EstimatedDiameterMax { get; set; }
}
