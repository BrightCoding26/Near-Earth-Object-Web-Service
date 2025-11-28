namespace NearEarthObjectsWebService.Dto.Nasa;

public record NearEarthObjectsByApproachResponse
{
    public required Links Links { get; init; }

    public int ElementCount { get; init; }

    public required Dictionary<string, List<NearEarthObject>> NearEarthObjects { get; init; }
}
