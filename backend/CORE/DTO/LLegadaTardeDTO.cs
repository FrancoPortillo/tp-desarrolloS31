using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CORE.DTO
{
    public class LLegadaTardeDTO
    {
        public required DateTime Fecha { get; set; }
        public required string Motivo { get; set; }
        public required int MinutosTarde{ get; set; }

    }
    public class LLegadaTardeDTOConId : LLegadaTardeDTO
    {
        public int Id { get; set; }
        public int IdEmpleado { get; set; }
    }
}

