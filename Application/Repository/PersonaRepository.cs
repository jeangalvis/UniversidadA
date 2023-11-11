using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class PersonaRepository : GenericRepository<Persona>, IPersona
{
    private readonly UniversidadAContext _context;
    public PersonaRepository(UniversidadAContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Persona> GetByIdAsync(int id)
    {
        return await _context.Personas
                            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Persona>> GetAllAsync()
    {
        return await _context.Personas.ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Persona> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Personas as IQueryable<Persona>;
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