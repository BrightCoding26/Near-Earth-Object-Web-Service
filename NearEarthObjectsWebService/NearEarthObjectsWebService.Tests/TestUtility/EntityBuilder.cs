using NearEarthObjectsWebService.Dto.V1;
using Nasa = NearEarthObjectsWebService.Dto.Nasa;

namespace NearEarthObjectsWebServiceTests.TestUtility;

public static class EntityBuilder
{
    public static NearEarthObject NearEarthObjectV1()
    {
        return new NearEarthObject
        {
            Name = "66063 (1998 RO1)",
            NasaJplUrl = "https://ssd.jpl.nasa.gov/tools/sbdb_lookup.html#/?sstr=2066063",
            AbsoluteMagnitudeH = 18.15,
            EstimatedDiameter = new EstimatedDiameter
            {
                Kilometers = new Diameter
                {
                    EstimatedDiameterMin = 0.6230960191,
                    EstimatedDiameterMax = 1.3932850552
                },
                Meters = new Diameter
                {
                    EstimatedDiameterMin = 623.096019112,
                    EstimatedDiameterMax = 1393.285055244
                },
                Miles = new Diameter
                {
                    EstimatedDiameterMin = 0.3871737965,
                    EstimatedDiameterMax = 0.8657469281
                },
                Feet = new Diameter
                {
                    EstimatedDiameterMin = 2044.2783433435,
                    EstimatedDiameterMax = 4571.1453406468
                }
            },
            IsPotentiallyHazardousAsteroid = false,
            CloseApproachData = new List<CloseApproachData>
            {
                new()
                {
                    CloseApproachDate = "1998-09-22",
                    CloseApproachDateFull = "1998-Sep-22 09:51",
                    EpochDateCloseApproach = 906457860000,
                    RelativeVelocity = new RelativeVelocity
                    {
                        KilometersPerSecond = "17.7963382091",
                        KilometersPerHour = "64066.8175528025",
                        MilesPerHour = "39808.6289399622"
                    },
                    MissDistance = new()
                    {
                        Astronomical = "0.4318146627",
                        Lunar = "167.9759037903",
                        Kilometers = "64598553.774688449",
                        Miles = "40139680.0138966362"
                    },
                    OrbitingBody = "Earth"
                }
            },
            IsSentryObject = true,
            SentryData = "api.nasa.gov/neo/rest/v1/neo/sentry/1234?api_key=fake_key"
        };
    }

    public static Nasa.V1.NeoByApproachDateResponse NeoByApproachDateResponse()
    {
        return new Nasa.V1.NeoByApproachDateResponse
        {
            Links = new Nasa.V1.Links
            {
                Next = "api.nasa.gov/next",
                Previous = "api.nasa.gov/previous",
                Self = "api.nasa.gov/self"
            },
            ElementCount = 2,
            NearEarthObjects = new Dictionary<string, List<Nasa.V1.NearEarthObject>>
            {
                {
                    "2025-11-23",
                    new List<Nasa.V1.NearEarthObject>
                    {
                        NasaV1NearEarthObject(isLargestNeo: true),
                        NasaV1NearEarthObject(isLargestNeo: false)
                    }
                }
            }
        };
    }

    private static Nasa.V1.NearEarthObject NasaV1NearEarthObject(bool isLargestNeo)
    {
        var uniqueNumber = NumberGenerator.UniqueNumber();
        return new Nasa.V1.NearEarthObject
        {
            Id = isLargestNeo ? "2066063" : uniqueNumber.ToString(),
            NeoReferenceId = isLargestNeo ? "2066063" : uniqueNumber.ToString(),
            Name = isLargestNeo ? "66063 (1998 RO1)" : "(1998 HM1)",
            NasaJplUrl = "https://ssd.jpl.nasa.gov/tools/sbdb_lookup.html#/?sstr=2066063",
            AbsoluteMagnitudeH = 18.15,
            EstimatedDiameter = new Nasa.V1.EstimatedDiameter
            {
                Kilometers = new Nasa.V1.Diameter
                {
                    EstimatedDiameterMin = isLargestNeo ? 0.6230960191 : 0.0347180222,
                    EstimatedDiameterMax = isLargestNeo ? 1.3932850552 : 0.0776318577
                },
                Meters = new Nasa.V1.Diameter
                {
                    EstimatedDiameterMin = 623.096019112,
                    EstimatedDiameterMax = 1393.285055244
                },
                Miles = new Nasa.V1.Diameter
                {
                    EstimatedDiameterMin = 0.3871737965,
                    EstimatedDiameterMax = 0.8657469281
                },
                Feet = new Nasa.V1.Diameter
                {
                    EstimatedDiameterMin = 2044.2783433435,
                    EstimatedDiameterMax = 4571.1453406468
                }
            },
            IsPotentiallyHazardousAsteroid = false,
            CloseApproachData = new List<Nasa.V1.CloseApproachData>
            {
                new()
                {
                    CloseApproachDate = "1998-09-22",
                    CloseApproachDateFull = "1998-Sep-22 09:51",
                    EpochDateCloseApproach = 906457860000,
                    RelativeVelocity = new Nasa.V1.RelativeVelocity
                    {
                        KilometersPerSecond = "17.7963382091",
                        KilometersPerHour = "64066.8175528025",
                        MilesPerHour = "39808.6289399622"
                    },
                    MissDistance = new()
                    {
                        Astronomical = "0.4318146627",
                        Lunar = "167.9759037903",
                        Kilometers = "64598553.774688449",
                        Miles = "40139680.0138966362"
                    },
                    OrbitingBody = "Earth"
                }
            },
            IsSentryObject = true,
            SentryData = "api.nasa.gov/neo/rest/v1/neo/sentry/1234?api_key=fake_key"
        };
    }
}
