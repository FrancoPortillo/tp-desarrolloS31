using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public partial class Empresa
    {
        
        public int Id { get; set; } //PK ¿¿Como las referenciamos?????
        public required string Nombre{ get; set; }

        public  required string Telefono { get; set; }

        public required string Email { get; set; }

        public  required string direccion { get; set; } 


    }
}


