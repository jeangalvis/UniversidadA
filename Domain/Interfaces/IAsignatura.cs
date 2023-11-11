using Domain.Entities;
using Domain.Views;

namespace Domain.Interfaces
{
    public interface IAsignatura : IGeneric<Asignatura>
    {
        Task<IEnumerable<Asignatura>> GetAsignaturasCuatriCurso();
        Task<IEnumerable<Asignatura>> GetAsignaturasIngenieriaInformatica();
        Task<IEnumerable<AsignaturasAnyoNif>> GetAsignaturasAnyoNif();
    }
}