using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AlumnoMatriculaAsignaturaDto
    {
        public int Id { get; set; }
        public int IdPersonafk { get; set; }
        public int IdAsignaturafk { get; set; }
        public int IdCursoEscolarfk { get; set; }

    }
}