using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class CursoEscolarRepository : GenericRepository<CursoEscolar>, ICursoEscolar
{
    private readonly UniversidadAContext _context;
    public CursoEscolarRepository(UniversidadAContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<CursoEscolar> GetByIdAsync(int id)
    {
        return await _context.CursoEscolares
                            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<CursoEscolar>> GetAllAsync()
    {
        return await _context.CursoEscolares.ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<CursoEscolar> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.CursoEscolares as IQueryable<CursoEscolar>;
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