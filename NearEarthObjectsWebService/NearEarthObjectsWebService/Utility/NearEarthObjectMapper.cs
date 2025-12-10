using NearEarthObjectsWebService.Dto.V1;
using Riok.Mapperly.Abstractions;
using Nasa = NearEarthObjectsWebService.Dto.Nasa;

namespace NearEarthObjectsWebService.Utility;

[Mapper]
public static partial class NearEarthObjectMapper
{
    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public static partial NearEarthObject MapToDtoV1(this Nasa.V1.NearEarthObject source);
}
