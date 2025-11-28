using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NearEarthObjectsWebService.Utility;

public sealed class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    private const string VersionAll = "all";

    public void Configure( SwaggerGenOptions options )
    {
        options.SwaggerDoc(VersionAll, CreateOpenApiInfo(VersionAll));

        foreach (var version in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(version.GroupName, CreateOpenApiInfo(version.ApiVersion.ToString()));
        }

        options.DocInclusionPredicate((docName, api) => docName == api.GroupName || docName == VersionAll);
    }

    private static OpenApiInfo CreateOpenApiInfo(string version) => new()
    {
        Title = "Near Earth Objects",
        Version = version,
        Description = "Near Earth Objects API"
    };
}
