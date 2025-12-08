namespace NearEarthObjectsWebService.Utility;

public static class HttpClientExtensions
{
    public static async ValueTask<HttpResponseMessage> ExecuteGetAsync(
        this HttpClient httpClient,
        string requestUri)
    {
        var httpResponseMessage = await httpClient.GetAsync(requestUri);
        httpResponseMessage.EnsureSuccessStatusCode();
        return httpResponseMessage;
    }
}
