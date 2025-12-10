using System.Net;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NearEarthObjectsWebService.Services.Interfaces;

namespace NearEarthObjectsWebService.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("v{v:apiVersion}/LargestNeoDuringBirthWeek")]
public class NearEarthObjectsController(
    ILogger<NearEarthObjectsController> logger,
    INearEarthObjectsService nearEarthObjectsService) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [MapToApiVersion(1)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LargestNeoDuringBirthWeekV1([FromQuery] DateTime birthDate)
    {
        var result = await nearEarthObjectsService.GetLargestNeoDuringBirthWeek(birthDate);

        if (result.HttpStatusCode != null)
        {
            return StatusCode((int)result.HttpStatusCode, result.Content);
        }

        logger.LogError("{Method} returned a null HttpStatusCode.", nameof(LargestNeoDuringBirthWeekV1));
        result.HttpStatusCode = HttpStatusCode.InternalServerError;
        return Problem();
    }
}
