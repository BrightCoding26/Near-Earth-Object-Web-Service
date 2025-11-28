using System.Net;
using HttpExceptions.Exceptions;
using NearEarthObjectsWebService.Dto.Nasa;
using NearEarthObjectsWebService.Model;
using NearEarthObjectsWebService.Services.Interfaces;
using NearEarthObjectsWebService.Utility;

namespace NearEarthObjectsWebService.Services;

public sealed class NearEarthObjectsService(ILogger<NearEarthObjectsService> logger) : INearEarthObjectsService
{
    private static readonly HttpClient nasaClient = new()
    {
        BaseAddress = new Uri("https://api.nasa.gov/neo/rest/v1/")
    };

    public async Task<Result<NearEarthObjectsByApproachResponse>> GetNearEarthObjectsByApproachDateAsync(
        DateTime? startDate,
        DateTime? endDate,
        CancellationToken ct = default)
    {
        if (startDate == null && endDate == null)
        {
            endDate = DateTime.Now;
            startDate = endDate.Value.AddDays(-7);
        }
        else if (startDate == null || endDate == null)
        {
            throw new BadRequestException("Both the start date and end date must be either specified or left blank.");
        }
        else if (IsDateRangeExceeded(startDate.Value, endDate.Value))
        {
            throw new BadRequestException("The StartDate and EndDate must be within 7 days of each other.");
        }

        var httpResponse = await nasaClient.ExecuteGetAsync(
            $"{nasaClient.BaseAddress}feed?" +
            $"start_date={startDate.Value:yyyy-mm-dd}&" +
            $"end_date={endDate.Value:yyyy-mm-dd}&" +
            $"api_key=DEMO_KEY",
            ct);

        var result = new Result<NearEarthObjectsByApproachResponse>
        {
            Object = await httpResponse.Content.ReadFromJsonAsync<NearEarthObjectsByApproachResponse>(ct),
            HttpStatusCode = (int)httpResponse.StatusCode
        };

        if (result.Object?.ElementCount != 0)
        {
            return result;
        }

        const string NoDataMessage = "No data was returned from NASA's API.";
        logger.LogWarning(NoDataMessage);
        result.Message = NoDataMessage;
        result.HttpStatusCode = (int)HttpStatusCode.NoContent;

        return result;
    }

    private static bool IsDateRangeExceeded(DateTime dateTime1, DateTime dateTime2)
    {
        const int DateRangeInDays = 7;

        double daysDifference = (dateTime1.Date - dateTime2.Date).TotalDays;

        return Math.Abs(daysDifference) <= DateRangeInDays;
    }
}
