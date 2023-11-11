using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class TipoAsignaturaConfiguration : IEntityTypeConfiguration<TipoAsignatura>
{
    public void Configure(EntityTypeBuilder<TipoAsignatura> builder)
    {
        builder.ToTable("tipo_asignatura");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasMaxLength(10).IsRequired();

        builder.Property(p => p.Descripcion).HasColumnType("varchar").HasMaxLength(25).IsRequired();
    }
}