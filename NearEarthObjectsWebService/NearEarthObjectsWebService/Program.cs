using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NearEarthObjectsWebService;
using NearEarthObjectsWebService.EfCore;
using NearEarthObjectsWebService.EfCore.Interfaces;
using NearEarthObjectsWebService.Middlewares;
using NearEarthObjectsWebService.Services;
using NearEarthObjectsWebService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("NearEarthObjects")
    ?? throw new InvalidOperationException("Connection string 'NearEarthObjects' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<IApplicationDbContextFactory<ApplicationDbContext>, ApplicationDbContextFactory>();
builder.Services.AddTransient<INasaService, NasaService>();
builder.Services.AddTransient<INearEarthObjectsService, NearEarthObjectsService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["Jwt:Key"] ??
            throw new InvalidOperationException("JWT Key not found.");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddMvcCore().AddApiExplorer();
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

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var dbContextFactory = app.Services.GetRequiredService<IApplicationDbContextFactory<ApplicationDbContext>>();
await using var dbContext = await dbContextFactory.CreateDbContextAsync();

await RetryPolicy.ExecuteAsync(async () => await dbContext.Database.MigrateAsync());

const string UsePathBase = "/api";

app.UseForwardedHeaders();
app.UsePathBase(UsePathBase);
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        app.UseStaticFiles();
        options.InjectStylesheet("/swagger-ui/SwaggerDark.css");

        options.SwaggerEndpoint($"{UsePathBase}/swagger/all/swagger.json", "All");

        foreach (var groupName in app.DescribeApiVersions().Select(d => d.GroupName))
        {
            options.SwaggerEndpoint($"{UsePathBase}/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
        }
    });
}

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers().RequireAuthorization();
app.MapGet("/", context =>
{
    context.Response.Redirect(UsePathBase.Trim('/') + "/" + "swagger");
    return Task.CompletedTask;
});

await app.RunAsync();