using Domain.Entities;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class ProfesorRepository : GenericRepository<Profesor>, IProfesor
{
    private readonly UniversidadAContext _context;
    public ProfesorRepository(UniversidadAContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Profesor> GetByIdAsync(int id)
    {
        return await _context.Profesores
                            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Profesor>> GetAllAsync()
    {
        return await _context.Profesores.ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Profesor> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Profesores as IQueryable<Profesor>;
        if (!string.IsNullOrEmpty(search))
        {
            // query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }

    public async Task<IEnumerable<Profesor>> GetProfesoresSinDepartamento()
    {
        return await _context.Profesores
                                    .Where(profesor => profesor.IdDepartamentofk == null)
                                    .ToListAsync();
    }

    public async Task<IEnumerable<Profesor>> GetProfesoresSinAsignatura()
    {
        return await _context.Profesores
                            .Where(profesor => !_context.Asignaturas.Any(asignatura => asignatura.IdProfesorfk == profesor.Id))
                            .ToListAsync();
    }
}