using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CursoEscolarDto
    {
        public int Id { get; set; }
        public int AnyoInicio { get; set; }
        public int AnyoFinal { get; set; }
    }
}