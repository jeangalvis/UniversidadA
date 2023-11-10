using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AlumnoMatriculaAsignatura : BaseEntity
    {
        public int IdPersonafk { get; set; }
        public Persona Persona { get; set; }
        public int IdAsignaturafk { get; set; }
        public Asignatura Asignatura { get; set; }
        public int IdCursoEscolarfk { get; set; }
        public CursoEscolar CursoEscolar { get; set; }
    }
}