using NearEarthObjectsWebService.Dto.Nasa;
using NearEarthObjectsWebService.Model;

namespace NearEarthObjectsWebService.Services.Interfaces;

public interface INearEarthObjectsService
{
    Task<Result<NearEarthObjectsByApproachResponse>> GetNearEarthObjectsByApproachDateAsync(
        DateTime? startDate,
        DateTime? endDate,
        CancellationToken ct = default);
}
