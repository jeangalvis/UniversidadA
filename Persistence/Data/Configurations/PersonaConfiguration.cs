using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("persona");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasMaxLength(10).IsRequired();

        builder.Property(p => p.Nif).HasColumnType("varchar").HasMaxLength(9).IsRequired();
        builder.Property(p => p.Nombre).HasColumnType("varchar").HasMaxLength(25).IsRequired();
        builder.Property(p => p.Apellido1).HasColumnType("varchar").HasMaxLength(50).IsRequired();
        builder.Property(p => p.Apellido2).HasColumnType("varchar").HasMaxLength(50).IsRequired();
        builder.Property(p => p.Ciudad).HasColumnType("varchar").HasMaxLength(25).IsRequired();
        builder.Property(p => p.Direccion).HasColumnType("varchar").HasMaxLength(50).IsRequired();
        builder.Property(p => p.Telefono).HasColumnType("varchar").HasMaxLength(9).IsRequired(false);
        builder.Property(p => p.FechaNacimiento).IsRequired();

        builder.HasOne(p => p.Sexo)
            .WithMany(p => p.Personas)
            .HasForeignKey(p => p.IdSexofk);

        builder.HasOne(p => p.TipoPersona)
            .WithMany(p => p.Personas)
            .HasForeignKey(p => p.IdTipoPersonafk);
    }
}