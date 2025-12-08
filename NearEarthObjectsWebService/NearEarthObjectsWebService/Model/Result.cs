using System.Net;

namespace NearEarthObjectsWebService.Model;

public record Result<T>
{
    public T? Content { get; set; }

    public string? Message { get; set; }

    public bool? Success { get; set; }

    public HttpStatusCode? HttpStatusCode { get; set; }
}
