using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class AsignaturaConfiguration : IEntityTypeConfiguration<Asignatura>
{
    public void Configure(EntityTypeBuilder<Asignatura> builder)
    {
        builder.ToTable("asignatura");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasMaxLength(10).IsRequired();
        builder.Property(p => p.Nombre).HasColumnType("varchar").HasMaxLength(100).IsRequired();
        builder.Property(p => p.Creditos).IsRequired();
        builder.Property(p => p.Curso).HasMaxLength(3).IsRequired();
        builder.Property(p => p.Cuatrimestre).HasMaxLength(3).IsRequired();

        builder.HasOne(p => p.TipoAsignatura)
            .WithMany(p => p.Asignaturas)
            .HasForeignKey(p => p.IdTipoAsignaturafk);

        builder.HasOne(p => p.Profesor)
            .WithMany(p => p.Asignaturas)
            .HasForeignKey(p => p.IdProfesorfk);

        builder.HasOne(p => p.Grado)
            .WithMany(p => p.Asignaturas)
            .HasForeignKey(p => p.IdGradofk);
    }
}