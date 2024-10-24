using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{

    public partial class Asistencia
    {
        

        public int Id { get; set; } 
        public required DateTime Fecha { get; set; }

        public required Boolean Presente { get; set; }

        public required string Idempleado { get; set; }

        public virtual Empleado Empleado { get; set; }  //fk

    }
}