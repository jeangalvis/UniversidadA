using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Asignatura : BaseEntity
    {
        public string Nombre { get; set; }
        public float Creditos { get; set; }
        public int IdTipoAsignaturafk { get; set; }
        public TipoAsignatura TipoAsignatura { get; set; }
        public int Curso { get; set; }
        public int Cuatrimestre { get; set; }
        public int? IdProfesorfk { get; set; }
        public Profesor Profesor { get; set; }
        public int IdGradofk { get; set; }
        public Grado Grado { get; set; }
        public ICollection<AlumnoMatriculaAsignatura> alumnoMatriculaAsignaturas { get; set; }

    }
}