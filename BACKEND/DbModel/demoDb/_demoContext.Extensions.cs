using Microsoft.EntityFrameworkCore;

namespace DbModel.demoDb;

public partial class _demoContext
{
    public virtual DbSet<Casa> Casa { get; set; }
    public virtual DbSet<MascotaTipo> MascotaTipo { get; set; }
    public virtual DbSet<Mascota> Mascota { get; set; }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Casa>(entity =>
        {
            entity.ToTable("casa");
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.DateUpdate).ValueGeneratedOnAddOrUpdate();

            entity.HasOne(d => d.IdPropietarioPersonaNavigation)
                .WithMany()
                .HasForeignKey(d => d.IdPropietarioPersona)
                .HasConstraintName("fk_casa_propietario_persona");
        });

        modelBuilder.Entity<MascotaTipo>(entity =>
        {
            entity.ToTable("mascota_tipo");
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.HasIndex(e => e.Codigo).IsUnique().HasDatabaseName("uq_mascota_tipo_codigo");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.DateUpdate).ValueGeneratedOnAddOrUpdate();
        });

        modelBuilder.Entity<Mascota>(entity =>
        {
            entity.ToTable("mascota");
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.DateUpdate).ValueGeneratedOnAddOrUpdate();

            entity.HasOne(d => d.IdDuenioPersonaNavigation)
                .WithMany()
                .HasForeignKey(d => d.IdDuenioPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_mascota_duenio_persona");

            entity.HasOne(d => d.IdMascotaTipoNavigation)
                .WithMany(p => p.Mascota)
                .HasForeignKey(d => d.IdMascotaTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_mascota_tipo");

            entity.HasOne(d => d.IdCasaNavigation)
                .WithMany(p => p.Mascota)
                .HasForeignKey(d => d.IdCasa)
                .HasConstraintName("fk_mascota_casa");
        });
    }
}
