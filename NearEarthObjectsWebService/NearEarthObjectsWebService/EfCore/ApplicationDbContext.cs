using Microsoft.EntityFrameworkCore;
using NearEarthObjectsWebService.EfCore.Tables;

namespace NearEarthObjectsWebService.EfCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asteroid> Asteroids { get; set; } = null!;

    public virtual DbSet<SetupOrbitingBody> SetupOrbitingBodies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asteroid>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Asteroids");

            entity.HasOne(d => d.OrbitingBody).WithMany(p => p.Asteroids)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asteroids_SetupOrbitingBody");

            entity.Property(e => e.IsSoftDeleted).HasDefaultValueSql("0");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<SetupOrbitingBody>(entity =>
        {
            entity.HasKey(e => e.OrbitingBodyId).HasName("PK_SetupOrbitingBodies");

            entity.Property(e => e.IsEnabled).HasDefaultValueSql("1");

            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getutcdate())");
        });
    }
}
