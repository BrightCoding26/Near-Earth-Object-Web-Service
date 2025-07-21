using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NearEarthObjectsWebService.EfCore.Tables;

// All distance measurements are in Kilometers and all time measurements are in seconds.
// e.g., RelativeVelocity = km/s or EstimatedDiameter =
public class Asteroid
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; init; }

    [StringLength(20)]
    [Unicode(false)]
    public string Name { get; init; } = null!;

    // Jpl stands for Jet Propulsion Laboratory
    [StringLength(2048)]
    [Unicode]
    public string NasaJplUrl { get; init; } = null!;

    [Column(TypeName = "decimal(2, 2)")]
    public decimal AbsoluteMagnitude { get; init; }

    [Column(TypeName = "decimal(11, 10)")]
    public decimal EstimatedDiameter { get; init; }

    [Column(TypeName = "datetime")]
    public DateTime CloseApproachDate { get; init; }

    [Column(TypeName = "decimal(11, 10)")]
    public decimal RelativeVelocity { get; init; }

    [Column(TypeName = "decimal(11, 10)")]
    public decimal MissDistance { get; init; }

    public int OrbitingBodyId { get; init; }

    public bool IsPotentiallyHazardous { get; init; }

    public bool IsSentryObject { get; init; }

    [StringLength(2048)]
    [Unicode]
    public string? SentryDataUrl { get; init; }

    public bool? IsSoftDeleted { get; init; } = false;

    [StringLength(20)]
    [Unicode(false)]
    public string CreatedBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedDateTime { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDateTime { get; set; }

    [ForeignKey("OrbitingBodyId")]
    [InverseProperty("Asteroids")]
    public virtual SetupOrbitingBody OrbitingBody { get; set; } = null!;
}
