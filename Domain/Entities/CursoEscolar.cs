using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CursoEscolar : BaseEntity
    {
        public int AnyoInicio { get; set; }
        public int AnyoFinal { get; set; }
    }
}