using Domain.Entities;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamento
{
    private readonly UniversidadAContext _context;
    public DepartamentoRepository(UniversidadAContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Departamento> GetByIdAsync(int id)
    {
        return await _context.Departamentos
                            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Departamento>> GetAllAsync()
    {
        return await _context.Departamentos.ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Departamento> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Departamentos as IQueryable<Departamento>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }

    public async Task<IEnumerable<Departamento>> GetDepartamentoProfesoresInformatica()
    {
        return await _context.Departamentos
                                        .Include(p => p.Profesores)
                                        .ThenInclude(p => p.Asignaturas)
                                        .ThenInclude(p => p.Grado)
                                        .Where(p => p.Profesores.Any(p => p.Asignaturas.Any(p => p.Grado.Nombre == "Grado en Ingeniería Informática (Plan 2015)")))
                                        .GroupBy(p => new { p.Id, p.Nombre })
                                        .Select(p => new Departamento
                                        {
                                            Id = p.Key.Id,
                                            Nombre = p.Key.Nombre
                                        })
                                        .ToListAsync();
    }

    public async Task<IEnumerable<DepartamentoAsignaturaSinImpartir>> GetDepartamentoConAsignaturaSinImpartir()
    {
        var resultado = (from departamento in _context.Departamentos
                         where (from asignatura in _context.Asignaturas
                                where asignatura.IdDepartamentofk == departamento.Id &&
                                      !_context.AlumnoMatriculaAsignaturas.Any(ama => ama.IdAsignaturafk == asignatura.Id)
                                select asignatura).Any()
                         from asignatura in _context.Asignaturas
                         where asignatura.IdDepartamentofk == departamento.Id &&
                               !_context.AlumnoMatriculaAsignaturas.Any(ama => ama.IdAsignaturafk == asignatura.Id)
                         select new DepartamentoAsignaturaSinImpartir
                         {
                             NombreDepartamento = departamento.Nombre,
                             NombreAsignatura = asignatura.Nombre
                         }).ToListAsync();

        return await resultado;
    }

    public async Task<IEnumerable<ProfesoresxDepartamento>> GetProfesoresxDepartamentos()
    {
        var resultado = await _context.Departamentos
        .Where(departamento => departamento.Profesores.Any())
        .OrderByDescending(departamento => departamento.Profesores.Count())
        .Select(departamento => new ProfesoresxDepartamento
        {
            NombreDepartamento = departamento.Nombre,
            NumeroProfesores = departamento.Profesores.Count()
        })
        .ToListAsync();

        return resultado;
    }
    public async Task<IEnumerable<ProfesoresxDepartamento>> GetCantidadProfesoresxDepartamentos()
    {
        var resultado = await _context.Departamentos
        .Select(departamento => new ProfesoresxDepartamento
        {
            NombreDepartamento = departamento.Nombre,
            NumeroProfesores = departamento.Profesores.Count()
        })
        .ToListAsync();

        return resultado;
    }
    public async Task<IEnumerable<Departamento>> GetDepartamentoNoImpartieronAsignatura()
    {
        return await _context.Departamentos
                    .Include(p => p.Profesores)
                    .ThenInclude(p => p.Asignaturas)
                    .Where(p => !p.Profesores.Any(p => p.Asignaturas.Any())).ToListAsync();
    }
}