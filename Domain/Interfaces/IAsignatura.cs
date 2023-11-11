using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAsignatura : IGeneric<Asignatura>
    {
        Task<IEnumerable<Asignatura>> GetAsignaturasCuatriCurso();
    }
}