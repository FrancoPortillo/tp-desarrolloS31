using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public partial class Empleado
    {

        public int Id { get; set; } //PK ¿¿Como las referenciamos?????
        public required string Nombre { get; set; }

        public required string Apellido{ get; set; }

        public required int Legajo{ get; set; }

        public required int Edad {  get; set; }

        public required string Dni { get; set; }

        public required string Email { get; set; }

        public required string Puesto { get; set; }

        public required string IdEmpresa { get; set; }

        public virtual Empresa Empresa { get; set; }  //fk



    }
}