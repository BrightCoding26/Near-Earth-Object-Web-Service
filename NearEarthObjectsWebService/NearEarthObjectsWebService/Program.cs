using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NearEarthObjectsWebService.EfCore;
using NearEarthObjectsWebService.EfCore.Interfaces;
using NearEarthObjectsWebService.Utility;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("NearEarthObjects")
    ?? throw new InvalidOperationException("Connection string 'NearEarthObjects' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddTransient<IApplicationDbContextFactory<ApplicationDbContext>, ApplicationDbContextFactory>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
{
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
    options.DefaultApiVersion = ApiVersion.Default;
    options.AssumeDefaultVersionWhenUnspecified = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("all", new OpenApiInfo
    {
        Title = "NearEarthObjects",
        Version = "all",
        Description = "API for NearEarthObjects Web Service."
    });

    o.DocInclusionPredicate((docName, api) => docName == api.GroupName || docName == "all");
});

var app = builder.Build();

var dbContextFactory = app.Services.GetRequiredService<IApplicationDbContextFactory<ApplicationDbContext>>();
await using var dbContext = await dbContextFactory.CreateDbContextAsync();

await RetryPolicy.ExecuteAsync(async () => await dbContext.Database.MigrateAsync());

const string UsePathBase = "/api";
app.UsePathBase(UsePathBase);

if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var groupName in apiVersionDescriptionProvider.ApiVersionDescriptions.Select(d => d.GroupName))
        {
            options.SwaggerEndpoint($"{UsePathBase}/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
        }

        options.SwaggerEndpoint($"{UsePathBase}/swagger/all/swagger.json", "All Versions");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();
