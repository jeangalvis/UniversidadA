using Domain.Entities;
using Domain.Views;

namespace Domain.Interfaces
{
    public interface IGrado : IGeneric<Grado>
    {
        Task<IEnumerable<GradosConAsignaturas>> GetGradosConAsignaturas();
        Task<IEnumerable<GradosConAsignaturas>> GetGradosConAsignaturasMas40();
        Task<IEnumerable<GradoSumaCreditos>> GetGradoSumaCreditos();
    }
}