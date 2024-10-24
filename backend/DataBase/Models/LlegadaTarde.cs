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
        public required DateTime fecha { get; set; }

        public required Timer MinutosTarde{ get; set; }

        public required string Idempleado { get; set; }

        public virtual Empleado Empleado { get; set; }  //fk



    }
}