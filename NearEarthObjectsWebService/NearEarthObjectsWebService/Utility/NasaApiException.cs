namespace NearEarthObjectsWebService.Utility;

public class NasaApiException : Exception
{
    private string _message { get; }

    public NasaApiException(string message) : base(message)
    {
        _message = "An error occurred when retrieving the near earth objects from NASA's API. Message: " + message;
    }
}
