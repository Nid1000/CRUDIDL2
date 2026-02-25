using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbModel.demoDb;

[Table("mascota_tipo")]
[Index("Codigo", Name = "uq_mascota_tipo_codigo", IsUnique = true)]
public partial class MascotaTipo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("codigo")]
    [StringLength(30)]
    public string Codigo { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(80)]
    public string Descripcion { get; set; } = null!;

    [Column("user_create")]
    public int UserCreate { get; set; }

    [Column("user_update")]
    public int? UserUpdate { get; set; }

    [Column("date_created", TypeName = "timestamp")]
    public DateTime? DateCreated { get; set; }

    [Column("date_update", TypeName = "timestamp")]
    public DateTime? DateUpdate { get; set; }

    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();
}
