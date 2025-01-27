using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    using System;

    namespace Core.DTO
    {
        public class AsistenciaDTO
        {
            public required DateTime Fecha { get; set; }

            public required Boolean Presente { get; set; }
        }
        public class AsistenciaDTOConId : AsistenciaDTO
        {
            public int Id { get; set; }
            public required int IdEmpleado { get; set; }
        }
    }
}