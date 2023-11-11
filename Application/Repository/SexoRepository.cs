using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class SexoRepository : GenericRepository<Sexo>, ISexo
{
    private readonly UniversidadAContext _context;
    public SexoRepository(UniversidadAContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Sexo> GetByIdAsync(int id)
    {
        return await _context.Sexos
                            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Sexo>> GetAllAsync()
    {
        return await _context.Sexos.ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Sexo> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Sexos as IQueryable<Sexo>;
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