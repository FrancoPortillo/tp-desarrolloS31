using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CORE.DTO
{
    public class PermisoAusenciaDTO
    {
        public required DateTime FechaInicio { get; set; }
        public required DateTime FechaFin { get; set; }
        public required DateTime FechaSolicitado { get; set; }
        public required string Estado { get; set; }
        public required string Detalles { get; set; }
        public required string Tipo { get; set; }
        public required int IdEmpleado { get; set; }
        public List<DocumentacionDTO> Documentaciones { get; set; } = new List<DocumentacionDTO>();
    }
    public class PermisoAusenciaDTOConId : PermisoAusenciaDTO
    {
        public required int Id { get; set; }
    }
}
