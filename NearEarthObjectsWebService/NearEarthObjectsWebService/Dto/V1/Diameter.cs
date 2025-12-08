namespace NearEarthObjectsWebService.Dto.V1;

public record Diameter
{
    public required double EstimatedDiameterMin { get; set; }

    public required double EstimatedDiameterMax { get; set; }
}
