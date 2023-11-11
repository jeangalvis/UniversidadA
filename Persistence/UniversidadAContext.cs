using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class UniversidadAContext : DbContext
    {
        public UniversidadAContext(DbContextOptions<UniversidadAContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<AlumnoMatriculaAsignatura> AlumnoMatriculaAsignaturas { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<CursoEscolar> CursoEscolares { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Grado> Grados { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Sexo> Sexos { get; set; }
        public DbSet<TipoAsignatura> TipoAsignaturas { get; set; }
        public DbSet<TipoPersona> TipoPersonas { get; set; }
    }
}