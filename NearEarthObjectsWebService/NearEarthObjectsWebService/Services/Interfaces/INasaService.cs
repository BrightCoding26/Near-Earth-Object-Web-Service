using NearEarthObjectsWebService.Dto.Nasa;
using NearEarthObjectsWebService.Dto.Nasa.V1;
using NearEarthObjectsWebService.Model;

namespace NearEarthObjectsWebService.Services.Interfaces;

public interface INasaService
{
    Task<Result<NeoByApproachDateResponse>> GetNeosByApproachDateAsync(
        DateTime? startDate,
        DateTime? endDate);
}
