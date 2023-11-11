using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class AlumnoMatriculaAsignaturaRepository : GenericRepository<AlumnoMatriculaAsignatura>, IAlumnoMatriculaAsignatura
{
    private readonly UniversidadAContext _context;
    public AlumnoMatriculaAsignaturaRepository(UniversidadAContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<AlumnoMatriculaAsignatura> GetByIdAsync(int id)
    {
        return await _context.AlumnoMatriculaAsignaturas
                            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<AlumnoMatriculaAsignatura>> GetAllAsync()
    {
        return await _context.AlumnoMatriculaAsignaturas.ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<AlumnoMatriculaAsignatura> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.AlumnoMatriculaAsignaturas as IQueryable<AlumnoMatriculaAsignatura>;
        if (!string.IsNullOrEmpty(search))
        {
            //query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}