using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class InasistenciaDTO
    {
        public required DateTime Fecha { get; set; }
        public required string Tipo { get; set; }
        public required string Detalles { get; set; }
        public required int IdEmpleado { get; set; }
        public List<DocumentacionDTO> Documentaciones { get; set; } = new List<DocumentacionDTO>();
        
    }
    public class InasistenciaDTOConId : InasistenciaDTO
    {
        public required int Id { get; set; }
    }
}
