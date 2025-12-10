using System.Net;
using NearEarthObjectsWebService.Dto.Nasa.V1;
using NearEarthObjectsWebService.Model;
using NearEarthObjectsWebService.Services.Interfaces;
using NearEarthObjectsWebService.Utility;

namespace NearEarthObjectsWebService.Services;

public sealed class NasaService(IConfiguration config) : INasaService
{
    private static readonly HttpClient nasaClient = new()
    {
        BaseAddress = new Uri("https://api.nasa.gov")
    };

    public async Task<Result<NeoByApproachDateResponse>> GetNeosByApproachDateAsync(
        DateTime? startDate,
        DateTime? endDate)
    {
        var result = new Result<NeoByApproachDateResponse>();

        if (startDate == null && endDate == null)
        {
            endDate = DateTime.Now;
            startDate = endDate.Value.AddDays(-7);
        }
        else if (startDate == null || endDate == null)
        {
            throw new BadHttpRequestException("Both the start date and end date must be either specified or left blank.");
        }
        else if (DateHelper.IsDateRangeExceeded(startDate.Value, endDate.Value))
        {
            throw new ArgumentException("The StartDate and EndDate must be within 7 days of each other.");
        }

        var httpResponse = await nasaClient.ExecuteGetAsync(
            $"{nasaClient.BaseAddress}neo/rest/v1/feed?" +
            $"start_date={startDate.Value:yyyy-MM-dd}&" +
            $"end_date={endDate.Value:yyyy-MM-dd}&" +
            $"api_key={config["NasaApi:ApiKey"] ?? "DEMO_KEY"}");

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new NasaApiException(httpResponse.ReasonPhrase ?? "No 'ReasonPhrase' provided by the API.");
        }

        var response = await httpResponse.Content.ReadFromJsonAsync<NeoByApproachDateResponse>();
        result.Success = true;

        if (response?.NearEarthObjects?.Count != 0)
        {
            result.Content = response;
            return result;
        }

        result.Message = "No near earth objects found during provided time period.";
        result.HttpStatusCode = HttpStatusCode.NoContent;
        return result;
    }
}
