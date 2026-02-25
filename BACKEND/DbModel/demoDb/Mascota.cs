using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbModel.demoDb;

[Table("mascota")]
[Index("IdDuenioPersona", Name = "fk_mascota_duenio_persona")]
[Index("IdMascotaTipo", Name = "fk_mascota_tipo")]
[Index("IdCasa", Name = "fk_mascota_casa")]
public partial class Mascota
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_duenio_persona")]
    public int IdDuenioPersona { get; set; }

    [Column("id_mascota_tipo")]
    public int IdMascotaTipo { get; set; }

    [Column("id_casa")]
    public int? IdCasa { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// 'M' = Macho, 'H' = Hembra (según tabla: enum('M','H'))
    /// </summary>
    [Column("sexo")]
    public string? Sexo { get; set; }

    [Column("fecha_nacimiento")]
    public DateTime? FechaNacimiento { get; set; }

    [Column("color")]
    [StringLength(60)]
    public string? Color { get; set; }

    [Column("observaciones")]
    [StringLength(300)]
    public string? Observaciones { get; set; }

    [Column("estado")]
    public bool Estado { get; set; }

    [Column("user_create")]
    public int UserCreate { get; set; }

    [Column("user_update")]
    public int? UserUpdate { get; set; }

    [Column("date_created", TypeName = "timestamp")]
    public DateTime? DateCreated { get; set; }

    [Column("date_update", TypeName = "timestamp")]
    public DateTime? DateUpdate { get; set; }

    [ForeignKey("IdCasa")]
    public virtual Casa? IdCasaNavigation { get; set; }

    [ForeignKey("IdDuenioPersona")]
    public virtual Persona IdDuenioPersonaNavigation { get; set; } = null!;

    [ForeignKey("IdMascotaTipo")]
    public virtual MascotaTipo IdMascotaTipoNavigation { get; set; } = null!;
}
