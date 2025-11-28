namespace NearEarthObjectsWebService.Utility;

public static class HttpClientExtensions
{
    public static async ValueTask<HttpResponseMessage> ExecuteGetAsync(
        this HttpClient httpClient,
        string requestUri,
        CancellationToken ct = default)
    {
        httpClient.BaseAddress = new Uri(requestUri);
        var httpResponseMessage = await httpClient.GetAsync(requestUri, ct);
        httpResponseMessage.EnsureSuccessStatusCode();
        return httpResponseMessage;
    }
}
