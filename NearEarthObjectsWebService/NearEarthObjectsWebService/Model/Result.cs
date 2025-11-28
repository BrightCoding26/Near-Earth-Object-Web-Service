namespace NearEarthObjectsWebService.Model;

public record Result<T>
{
    public T? Object { get; init; }

    public string? Message { get; set; }

    public bool? Success { get; set; }

    public int? HttpStatusCode { get; set; }
}
