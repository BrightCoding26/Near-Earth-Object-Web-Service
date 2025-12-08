using NearEarthObjectsWebService.Model;

namespace NearEarthObjectsWebService.Orchestrators.Interfaces;

public interface INearEarthObjectsOrchestrator
{
    Task<Result<Dto.V1.NearEarthObject>> GetLargestNeoDuringBirthWeek(DateTime dateOfBirth);
}
