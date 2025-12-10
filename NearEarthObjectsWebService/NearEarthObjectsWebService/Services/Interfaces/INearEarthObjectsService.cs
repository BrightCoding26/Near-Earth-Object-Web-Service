using NearEarthObjectsWebService.Model;

namespace NearEarthObjectsWebService.Services.Interfaces;

public interface INearEarthObjectsService
{
    Task<Result<Dto.V1.NearEarthObject>> GetLargestNeoDuringBirthWeek(DateTime dateOfBirth);
}
