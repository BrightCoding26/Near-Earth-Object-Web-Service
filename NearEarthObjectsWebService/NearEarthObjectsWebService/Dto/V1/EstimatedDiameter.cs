namespace NearEarthObjectsWebService.Dto.V1;

public record EstimatedDiameter
{
    public Diameter? Kilometers { get; set; }

    public Diameter? Meters { get; set; }

    public Diameter? Miles { get; set; }

    public Diameter? Feet { get; set; }
}