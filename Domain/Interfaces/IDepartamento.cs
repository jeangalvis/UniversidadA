using Domain.Entities;
using Domain.Views;

namespace Domain.Interfaces
{
    public interface IDepartamento : IGeneric<Departamento>
    {
        Task<IEnumerable<Departamento>> GetDepartamentoProfesoresInformatica();
        Task<IEnumerable<DepartamentoAsignaturaSinImpartir>> GetDepartamentoConAsignaturaSinImpartir();
        Task<IEnumerable<ProfesoresxDepartamento>> GetProfesoresxDepartamentos();
        Task<IEnumerable<ProfesoresxDepartamento>> GetCantidadProfesoresxDepartamentos();
        Task<IEnumerable<Departamento>> GetDepartamentoNoImpartieronAsignatura();
    }
}