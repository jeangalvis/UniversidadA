using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class AlumnoMatriculaAsignaturaConfiguration : IEntityTypeConfiguration<AlumnoMatriculaAsignatura>
{
    public void Configure(EntityTypeBuilder<AlumnoMatriculaAsignatura> builder)
    {
        builder.ToTable("alumno_se_matricula_asignatura");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasMaxLength(10).IsRequired();

        builder.HasOne(p => p.Persona)
            .WithMany(p => p.AlumnoMatriculaAsignaturas)
            .HasForeignKey(p => p.IdPersonafk);

        builder.HasOne(p => p.Asignatura)
            .WithMany(p => p.alumnoMatriculaAsignaturas)
            .HasForeignKey(p => p.IdAsignaturafk);

        builder.HasOne(p => p.CursoEscolar)
            .WithMany(p => p.AlumnoMatriculaAsignaturas)
            .HasForeignKey(p => p.IdCursoEscolarfk);
    }
}