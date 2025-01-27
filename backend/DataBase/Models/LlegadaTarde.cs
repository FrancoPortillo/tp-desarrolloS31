using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public partial class LLegadaTarde
    {

        public int Id { get; set; } //PK ¿¿Como las referenciamos?????
        public required DateTime Fecha { get; set; }

        public required int MinutosTarde{ get; set; }

        public required string Idempleado { get; set; }

        public required int IdEmpleado { get; set; }

    }
}