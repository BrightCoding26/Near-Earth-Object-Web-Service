using NearEarthObjectsWebService.Dto.Nasa.V1;
using Riok.Mapperly.Abstractions;

namespace NearEarthObjectsWebService.Utility.Mappers;

[Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
public static partial class SoloMappings
{
    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public static partial Dto.V1.NearEarthObject MapToV1Dto(this NearEarthObject source);
}
