using Microsoft.EntityFrameworkCore;
using NearEarthObjectsWebService.EfCore.Interfaces;

namespace NearEarthObjectsWebService.EfCore;

public class ApplicationDbContextFactory : IApplicationDbContextFactory<ApplicationDbContext>
{
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _environment;

    private const string DbConnection = "NearEarthObjects";
    private const int MaxRetryCount = 3;
    private const double MaximumDelayRetrySeconds = 3.0;

    public ApplicationDbContextFactory(IConfiguration configuration, IHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public ApplicationDbContext CreateDbContext()
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseSqlServer(GetConnectionString(), options =>
        {
            options.EnableRetryOnFailure(MaxRetryCount, TimeSpan.FromSeconds(MaximumDelayRetrySeconds), null);
        });

        if (!_environment.IsProduction())
        {
            builder.EnableSensitiveDataLogging();
        }

        return new ApplicationDbContext(builder.Options);
    }

    public async Task<ApplicationDbContext> CreateDbContextAsync()
    {
        return await Task.FromResult(CreateDbContext());
    }

    public ApplicationDbContext CreateReadOnlyDbContext()
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseSqlServer(GetConnectionString(), options =>
            {
                options.EnableRetryOnFailure(MaxRetryCount, TimeSpan.FromSeconds(MaximumDelayRetrySeconds), null);
            })
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        return new ApplicationDbContext(builder.Options);
    }

    public async Task<ApplicationDbContext> CreateReadOnlyDbContextAsync()
    {
        return await Task.FromResult(CreateReadOnlyDbContext());
    }

    private string GetConnectionString()
    {
        return _configuration.GetConnectionString(DbConnection)
            ?? throw new InvalidOperationException($"Connection string '{DbConnection}' not found.");
    }
}
