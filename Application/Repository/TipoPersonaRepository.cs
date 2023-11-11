using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class TipoPersonaRepository : GenericRepository<TipoPersona>, ITipoPersona
{
    private readonly UniversidadAContext _context;
    public TipoPersonaRepository(UniversidadAContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<TipoPersona> GetByIdAsync(int id)
    {
        return await _context.TipoPersonas
                            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<TipoPersona>> GetAllAsync()
    {
        return await _context.TipoPersonas.ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<TipoPersona> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.TipoPersonas as IQueryable<TipoPersona>;
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