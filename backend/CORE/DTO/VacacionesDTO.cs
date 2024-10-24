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

        public required DateTime FechaFin{ get; set; }

        public required Boolean Aprobado { get; set; }

        public required int IdEmpleado  { get; set; }

    }

    //Agrego idempleado que es la fk?

    public class VacacionesDTOConId : VacacionesDTO
    {
        public int Id { get; set; }
    }
}
}

