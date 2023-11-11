namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IAlumnoMatriculaAsignatura AlumnoMatriculaAsignaturas { get; }
        IAsignatura Asignaturas { get; }
        ICursoEscolar CursoEscolares { get; }
        IDepartamento Departamentos { get; }
        IGrado Grados { get; }
        IPersona Personas { get; }
        IProfesor Profesores { get; }
        ISexo Sexos { get; }
        ITipoAsignatura TipoAsignaturas { get; }
        ITipoPersona TipoPersonas { get; }
        Task<int> SaveAsync();
    }
}
