using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Profesor : BaseEntity
    {
        public int IdDepartamentofk { get; set; }
        public Departamento Departamento { get; set; }
        public int IdPersonafk { get; set; }
        public Persona Persona { get; set; }
        public ICollection<Asignatura> Asignaturas { get; set; }
    }
}
