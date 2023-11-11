using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AsignaturaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Creditos { get; set; }
        public int IdTipoAsignaturafk { get; set; }
        public int Curso { get; set; }
        public int Cuatrimestre { get; set; }
        public int IdProfesorfk { get; set; }
        public int IdGradofk { get; set; }
    }
}