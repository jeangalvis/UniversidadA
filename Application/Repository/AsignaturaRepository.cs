using Domain.Entities;
using Domain.Interfaces;
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
}