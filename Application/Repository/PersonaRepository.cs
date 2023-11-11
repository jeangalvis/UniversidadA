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

    public async Task<IEnumerable<Persona>> GetAlumnosxNombre()
    {
        return await _context.Personas
                                    .Where(p => p.IdTipoPersonafk == 2)
                                    .OrderBy(p => p.Apellido1)
                                    .ThenBy(p => p.Apellido2)
                                    .ThenBy(p => p.Nombre)
                                    .Select(p => new Persona { Apellido1 = p.Apellido1, Apellido2 = p.Apellido2, Nombre = p.Nombre })
                                    .ToListAsync();
    }
    public async Task<IEnumerable<Persona>> GetAlumnosSinTelefono()
    {
        return await _context.Personas
                                    .Where(p => p.IdTipoPersonafk == 2 && p.Telefono == null)
                                    .Select(p => new Persona { Apellido1 = p.Apellido1, Apellido2 = p.Apellido2, Nombre = p.Nombre })
                                    .ToListAsync();
    }
    public async Task<IEnumerable<Persona>> GetAlumnosNacieron1999()
    {
        return await _context.Personas
                                    .Where(p => p.IdTipoPersonafk == 2 && p.FechaNacimiento.Year == 1999)
                                    .ToListAsync();
    }

    public async Task<IEnumerable<Persona>> GetProfesoresSinTelefono(){
        return await _context.Personas
                                    .Where(p => p.IdTipoPersonafk == 1 && p.Telefono == null && p.Nif.EndsWith("K"))
                                    .ToListAsync();
    }
}