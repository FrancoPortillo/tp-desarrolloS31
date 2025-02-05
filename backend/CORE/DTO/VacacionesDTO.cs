using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class VacacionesDTO
    {
        public required DateTime FechaInicio { get; set; }

        public required DateTime FechaFin { get; set; }

        public required string Estado { get; set; }

        public required int IdEmpleado { get; set; }

    }
    public class VacacionesDTOConId : VacacionesDTO
    {
        public int Id { get; set; }
    }
}

