using Domain.Entities;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class AsignaturaRepository : GenericRepository<Asignatura>, IAsignatura
{
    private readonly UniversidadAContext _context;
    public AsignaturaRepository(UniversidadAContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Asignatura> GetByIdAsync(int id)
    {
        return await _context.Asignaturas
                            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Asignatura>> GetAllAsync()
    {
        return await _context.Asignaturas.ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Asignatura> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Asignaturas as IQueryable<Asignatura>;
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
    public async Task<IEnumerable<Asignatura>> GetAsignaturasCuatriCurso()
    {
        return await _context.Asignaturas
                                    .Where(p => p.Cuatrimestre == 1 && p.Curso == 3 && p.IdGradofk == 7).ToListAsync();
    }

    public async Task<IEnumerable<Asignatura>> GetAsignaturasIngenieriaInformatica()
    {
        return await _context.Asignaturas.Where(p => p.IdGradofk == 4).ToListAsync();
    }

    public async Task<IEnumerable<AsignaturasAnyoNif>> GetAsignaturasAnyoNif()
    {
        var resultado = await _context.Asignaturas
            .Include(a => a.alumnoMatriculaAsignaturas)
                .ThenInclude(ama => ama.Persona)
                .ThenInclude(p => p.AlumnoMatriculaAsignaturas)
                .ThenInclude(ama => ama.CursoEscolar)
            .Where(a => a.alumnoMatriculaAsignaturas.Any(ama => ama.Persona.Nif == "26902806M"))
            .Select(a => new AsignaturasAnyoNif
            {
                Nombre = a.Nombre,
                AnyoInicio = a.alumnoMatriculaAsignaturas
                    .Where(ama => ama.Persona.Nif == "26902806M" && ama.CursoEscolar != null)
                    .Select(ama => ama.CursoEscolar.AnyoInicio)
                    .FirstOrDefault(),
                AnyoFinal = a.alumnoMatriculaAsignaturas
                    .Where(ama => ama.Persona.Nif == "26902806M" && ama.CursoEscolar != null)
                    .Select(ama => ama.CursoEscolar.AnyoFinal)
                    .FirstOrDefault()
            })
            .ToListAsync();

        return resultado;
    }

    public async Task<IEnumerable<Asignatura>> GetAsignaturaSinProfesor(){
        return await _context.Asignaturas
                                        .Where(asignatura => asignatura.IdProfesorfk == null)
                                        .ToListAsync();
    }
}