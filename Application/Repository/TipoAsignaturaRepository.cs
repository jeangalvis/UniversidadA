using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class TipoAsignaturaRepository : GenericRepository<TipoAsignatura>, ITipoAsignatura
{
    private readonly UniversidadAContext _context;
    public TipoAsignaturaRepository(UniversidadAContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<TipoAsignatura> GetByIdAsync(int id)
    {
        return await _context.TipoAsignaturas
                            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<TipoAsignatura>> GetAllAsync()
    {
        return await _context.TipoAsignaturas.ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<TipoAsignatura> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.TipoAsignaturas as IQueryable<TipoAsignatura>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Descripcion.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}