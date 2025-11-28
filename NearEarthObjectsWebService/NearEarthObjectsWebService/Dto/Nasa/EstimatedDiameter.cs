namespace NearEarthObjectsWebService.Dto.Nasa;

public record EstimatedDiameter
{
    public Diameter? Kilometers { get; set; }

    public Diameter? Meters { get; set; }

    public Diameter? Miles { get; set; }

    public Diameter? Feet { get; set; }
}

public record Diameter
{
    public required double EstimatedDiameterMin { get; set; }

    public required double EstimatedDiameterMax { get; set; }
}
