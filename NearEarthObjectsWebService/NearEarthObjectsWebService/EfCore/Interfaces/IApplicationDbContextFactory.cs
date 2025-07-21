using Microsoft.EntityFrameworkCore;

namespace NearEarthObjectsWebService.EfCore.Interfaces;

public interface IApplicationDbContextFactory<TContext> where TContext : DbContext
{
    public TContext CreateDbContext();

    public Task<TContext> CreateDbContextAsync();

    public TContext CreateReadOnlyDbContext();

    public Task<TContext> CreateReadOnlyDbContextAsync();
}
