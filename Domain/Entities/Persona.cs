using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Persona : BaseEntity
    {
        public string Nif { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdSexofk { get; set; }
        public Sexo Sexo { get; set; }
        public int IdTipoPersonafk { get; set; }
        public TipoPersona TipoPersona { get; set; }
        public ICollection<Profesor> Profesores { get; set; }
        public ICollection<AlumnoMatriculaAsignatura> AlumnoMatriculaAsignaturas { get; set; }

    }
}