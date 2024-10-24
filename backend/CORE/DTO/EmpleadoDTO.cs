using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class EmpleadoDTO
    {
        public required  string  Nombre { get; set; }

        public required string Apellido { get; set; }

        public required string telefono {  get; set; }

        public required int edad { get; set; }
        public required string email { get; set; }

        public required string puesto  { get; set; }

    }

   

    public class EmpleadoDTOConId : EmpleadoDTO
    {
        public int Id { get; set; }
        public int legajo { get; set; }
    }
}

