using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public partial class Vacaciones
    {

        public int Id { get; set; } //PK ¿¿Como las referenciamos?????
        public required DateTime FechaInicio { get; set; }

        public required DateTime FechaFin { get; set; }

        public required Boolean aprobado { get; set; }

        public required string Idempleado { get; set; }

        public virtual Empleado Empleado { get; set; }  //fk
    }
}