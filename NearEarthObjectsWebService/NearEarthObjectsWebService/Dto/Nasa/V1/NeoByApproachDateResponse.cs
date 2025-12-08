using System.Text.Json.Serialization;

namespace NearEarthObjectsWebService.Dto.Nasa.V1;

public record NeoByApproachDateResponse
{
    public required Links Links { get; init; }

    [JsonPropertyName("element_count")]
    public int ElementCount { get; init; }

    [JsonPropertyName("near_earth_objects")]
    public Dictionary<string, List<NearEarthObject>>? NearEarthObjects { get; init; }
}
