namespace NearEarthObjectsWebService.Dto.Nasa.V1;

public record Links
{
    public string? Next { get; set; }

    public string? Previous { get; set; }

    public string? Self { get; init; }
}
