using Domain.Entities;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class GradoRepository : GenericRepository<Grado>, IGrado
{
    private readonly UniversidadAContext _context;
    public GradoRepository(UniversidadAContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Grado> GetByIdAsync(int id)
    {
        return await _context.Grados
                            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Grado>> GetAllAsync()
    {
        return await _context.Grados.ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Grado> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Grados as IQueryable<Grado>;
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

    public async Task<IEnumerable<GradosConAsignaturas>> GetGradosConAsignaturas()
    {
        var resultado = await _context.Grados
        .Select(grado => new GradosConAsignaturas
        {
            NombreGrado = grado.Nombre,
            NumeroAsignaturas = grado.Asignaturas.Count()
        })
        .OrderByDescending(grado => grado.NumeroAsignaturas)
        .ToListAsync();

        return resultado;
    }
    public async Task<IEnumerable<GradosConAsignaturas>> GetGradosConAsignaturasMas40()
    {
        var resultado = await _context.Grados
        .Where(grado => grado.Asignaturas.Count() > 40)
        .Select(grado => new GradosConAsignaturas
        {
            NombreGrado = grado.Nombre,
            NumeroAsignaturas = grado.Asignaturas.Count()
        })
        .ToListAsync();

        return resultado;
    }
    public async Task<IEnumerable<GradoSumaCreditos>> GetGradoSumaCreditos()
    {
        var resultado = await _context.Grados
            .SelectMany(grado => grado.Asignaturas
                .GroupBy(asignatura => asignatura.TipoAsignatura.Descripcion)
                .Select(grupo => new GradoSumaCreditos
                {
                    NombreGrado = grado.Nombre,
                    TipoAsignatura = grupo.Key,
                    SumaCreditos = grupo.Sum(asignatura => asignatura.Creditos)
                })
            )
            .OrderByDescending(resumen => resumen.SumaCreditos)
            .ToListAsync();

        return resultado;
    }
}