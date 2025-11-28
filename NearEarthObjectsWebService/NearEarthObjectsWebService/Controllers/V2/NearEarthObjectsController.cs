using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NearEarthObjectsWebService.Dto.Nasa;
using NearEarthObjectsWebService.Services.Interfaces;

namespace NearEarthObjectsWebService.Controllers.V2;

[ApiVersion(2)]
[ApiController]
[Route("v{v:apiVersion}/ByApproachDate")]
public class NearEarthObjectsController(
    ILogger<NearEarthObjectsController> logger,
    INearEarthObjectsService nearEarthObjectsService) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [MapToApiVersion(2)]
    [ProducesResponseType<NearEarthObjectsByApproachResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ByApproachDate([FromQuery]
        DateTime? startDate,
        DateTime? endDate,
        CancellationToken ct = default)
    {
        var result = await nearEarthObjectsService.GetNearEarthObjectsByApproachDateAsync(startDate, endDate, ct);

        if (result.HttpStatusCode != null)
        {
            return StatusCode(result.HttpStatusCode.Value, result.Object);
        }

        logger.LogError("{Method} returned an null HttpStatusCode.", nameof(ByApproachDate));
        result.HttpStatusCode = 500;
        return Problem();
    }
}
