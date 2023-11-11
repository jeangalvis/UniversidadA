using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class GradoConfiguration : IEntityTypeConfiguration<Grado>
{
    public void Configure(EntityTypeBuilder<Grado> builder)
    {
        builder.ToTable("grado");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasMaxLength(10).IsRequired();

        builder.Property(p => p.Nombre).HasColumnType("varchar").HasMaxLength(100).IsRequired();
    }
}