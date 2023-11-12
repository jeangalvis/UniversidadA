using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class GradoSumaCreditosDto
    {
        public string NombreGrado { get; set; }
        public string TipoAsignatura { get; set; }
        public float SumaCreditos { get; set; }
    }
}