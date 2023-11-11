using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProfesorDto
    {
        public int Id { get; set; }
        public int IdDepartamentofk { get; set; }
        public int IdPersonafk { get; set; }
    }
}