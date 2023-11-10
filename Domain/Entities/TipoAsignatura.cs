using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TipoAsignatura : BaseEntity
    {
        public string Descripcion { get; set; }
        public ICollection<Asignatura> Asignaturas { get; set; }
    }
}