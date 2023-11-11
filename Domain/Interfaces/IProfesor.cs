using Domain.Entities;
using Domain.Views;

namespace Domain.Interfaces
{
    public interface IProfesor : IGeneric<Profesor>
    {
        Task<IEnumerable<Profesor>> GetProfesoresSinDepartamento();
    }
}