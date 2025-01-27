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

        public required string Telefono {  get; set; }

        public required string Dni { get; set; }

        public required int Edad { get; set; }
        public required string Email { get; set; }

        public required string Puesto  { get; set; }

        public required string Rol { get; set; }

        public  required int Legajo { get; set; }

    }

   

    public class EmpleadoDTOConId : EmpleadoDTO
    {
        public int Id { get; set; }

        public int IdEmpresa { get; set; }
    }
}

