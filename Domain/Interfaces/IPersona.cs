using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPersona : IGeneric<Persona>
    {
        Task<IEnumerable<Persona>> GetAlumnosxNombre();
        Task<IEnumerable<Persona>> GetAlumnosSinTelefono();
        Task<IEnumerable<Persona>> GetAlumnosNacieron1999();
        Task<IEnumerable<Persona>> GetProfesoresSinTelefono();
    }
}