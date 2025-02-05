using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class DocumentacionDTO
    {
        public required string NombreArchivo {  get; set; }
        public required byte [] Content { get; set; }
        public required int IdEmpleado { get; set; }    }
    public class DocumentacionDTOConId : DocumentacionDTO 
    {
        public required int Id { get; set; }
    }
}
