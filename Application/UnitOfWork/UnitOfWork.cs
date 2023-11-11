using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly UniversidadAContext context;
        private IAlumnoMatriculaAsignatura _alumnoMatriculaAsignatura;
        private IAsignatura _asignatura;
        private ICursoEscolar _cursoEscolar;
        private IDepartamento _departamento;
        private IGrado _grado;
        private IPersona _persona;
        private IProfesor _profesor;
        private ISexo _sexo;
        private ITipoAsignatura _tipoAsignatura;
        private ITipoPersona _tipoPersona;

        public UnitOfWork(UniversidadAContext _context)
        {
            context = _context;
        }

        public IAlumnoMatriculaAsignatura AlumnoMatriculaAsignaturas
        {
            get
            {
                if (_alumnoMatriculaAsignatura == null)
                {
                    _alumnoMatriculaAsignatura = new AlumnoMatriculaAsignaturaRepository(context);
                }
                return _alumnoMatriculaAsignatura;
            }
        }

        public IAsignatura Asignaturas
        {
            get
            {
                if (_asignatura == null)
                {
                    _asignatura = new AsignaturaRepository(context);
                }
                return _asignatura;
            }
        }

        public ICursoEscolar CursoEscolares
        {
            get
            {
                if (_cursoEscolar == null)
                {
                    _cursoEscolar = new CursoEscolarRepository(context);
                }
                return _cursoEscolar;
            }
        }

        public IDepartamento Departamentos
        {
            get
            {
                if (_departamento == null)
                {
                    _departamento = new DepartamentoRepository(context);
                }
                return _departamento;
            }
        }

        public IGrado Grados
        {
            get
            {
                if (_grado == null)
                {
                    _grado = new GradoRepository(context);
                }
                return _grado;
            }
        }

        public IPersona Personas
        {
            get
            {
                if (_persona == null)
                {
                    _persona = new PersonaRepository(context);
                }
                return _persona;
            }
        }

        public IProfesor Profesores
        {
            get
            {
                if (_profesor == null)
                {
                    _profesor = new ProfesorRepository(context);
                }
                return _profesor;
            }
        }

        public ISexo Sexos
        {
            get
            {
                if (_sexo == null)
                {
                    _sexo = new SexoRepository(context);
                }
                return _sexo;
            }
        }

        public ITipoAsignatura TipoAsignaturas
        {
            get
            {
                if (_tipoAsignatura == null)
                {
                    _tipoAsignatura = new TipoAsignaturaRepository(context);
                }
                return _tipoAsignatura;
            }
        }

        public ITipoPersona TipoPersonas
        {
            get
            {
                if (_tipoPersona == null)
                {
                    _tipoPersona = new TipoPersonaRepository(context);
                }
                return _tipoPersona;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
