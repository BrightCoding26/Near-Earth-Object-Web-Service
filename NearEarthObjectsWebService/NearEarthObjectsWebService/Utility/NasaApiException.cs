namespace NearEarthObjectsWebService.Utility;

public class NasaApiException(string message) : Exception(
    "An error occurred when retrieving the near earth objects from NASA's API. Message: " + message);
