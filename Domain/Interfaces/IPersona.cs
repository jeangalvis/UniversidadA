using Domain.Entities;
using Domain.Views;

namespace Domain.Interfaces
{
    public interface IPersona : IGeneric<Persona>
    {
        Task<IEnumerable<Persona>> GetAlumnosxNombre();
        Task<IEnumerable<Persona>> GetAlumnosSinTelefono();
        Task<IEnumerable<Persona>> GetAlumnosNacieron1999();
        Task<IEnumerable<Persona>> GetProfesoresSinTelefono();
        Task<IEnumerable<Persona>> GetAlumnasEnSistemas();
        Task<IEnumerable<Persona>> GetProfesoresConDepartamento();
        Task<IEnumerable<Persona>> GetAlumnosMatriculadosAnyo();
        Task<IEnumerable<ProfesoresConDepartamento>> GetProfesoresConDepartamentos();
        Task<TotalAlumnos> GetTotalAlumnos();
        Task<TotalAlumnos> GetAlumnosDe1999();
    }
}