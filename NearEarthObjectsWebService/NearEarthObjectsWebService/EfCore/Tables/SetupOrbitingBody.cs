using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NearEarthObjectsWebService.EfCore.Tables;

public class SetupOrbitingBody
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int OrbitingBodyId { get; init; }

    [StringLength(7)]
    [Unicode(false)]
    public string Name { get; init; } = null!;

    public bool? IsEnabled { get; init; } = true;

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

    [InverseProperty("OrbitingBody")]
    public virtual ICollection<Asteroid> Asteroids { get; init; } = new List<Asteroid>();
}
