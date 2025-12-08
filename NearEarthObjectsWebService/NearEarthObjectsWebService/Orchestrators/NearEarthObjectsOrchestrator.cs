using System.Net;
using NearEarthObjectsWebService.Model;
using NearEarthObjectsWebService.Orchestrators.Interfaces;
using NearEarthObjectsWebService.Services.Interfaces;
using NearEarthObjectsWebService.Utility.Helpers;
using NearEarthObjectsWebService.Utility.Mappers;

namespace NearEarthObjectsWebService.Orchestrators;

public class NearEarthObjectsOrchestrator(INasaService nasaService) : INearEarthObjectsOrchestrator
{
    public async Task<Result<Dto.V1.NearEarthObject>> GetLargestNeoDuringBirthWeek(DateTime dateOfBirth)
    {
        var result = new Result<Dto.V1.NearEarthObject>();
        var weekDates = DateHelper.FindWeekDates(dateOfBirth);
        var response = await nasaService.GetNearEarthObjectsByApproachDateAsync(weekDates.StartOfWeek, weekDates.EndOfWeek);

        if (response.HttpStatusCode == HttpStatusCode.NoContent || !response.Success!.Value)
        {
            result.Message = response.Message;
            result.Success = response.Success;
            result.HttpStatusCode = response.HttpStatusCode;
            return result;
        }

        var nearEarthObjects = response.Content?.NearEarthObjects?.SelectMany(kvp => kvp.Value);
        var largestNeo = nearEarthObjects?.MaxBy(neo =>
            (neo.EstimatedDiameter.Kilometers?.EstimatedDiameterMin +
                neo.EstimatedDiameter.Kilometers?.EstimatedDiameterMax) / 2);

        if (largestNeo == null)
        {
            throw new InvalidOperationException("Failed to find the largest NEO in the list.");
        }

        result.Content = largestNeo.MapToV1Dto();
        result.HttpStatusCode = HttpStatusCode.OK;
        result.Success = true;
        return result;
    }
}
