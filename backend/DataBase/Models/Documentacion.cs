using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Documentacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int Id { get; set; }
        [Required]
        public required string NombreArchivo { get; set; }
        [Required]
        public required byte[] Contenido { get; set; }
        [ForeignKey("IdEmpleado")]
        public required int IdEmpleado { get; set; }
    }
}
