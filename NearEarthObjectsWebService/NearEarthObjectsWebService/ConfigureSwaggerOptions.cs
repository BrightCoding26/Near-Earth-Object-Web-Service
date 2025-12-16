using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NearEarthObjectsWebService;

public sealed class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    private const string VersionAll = "all";

    public void Configure( SwaggerGenOptions options )
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                []
            }
        });

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
