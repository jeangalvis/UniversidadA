using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AsignaturasAnyoNifDto
    {
        public string Nombre { get; set; }
        public int AnyoInicio { get; set; }
        public int AnyoFinal { get; set; }
    }
}