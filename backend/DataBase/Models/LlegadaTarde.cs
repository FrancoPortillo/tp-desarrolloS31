using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("LLegadaTarde")]
    public class LLegadaTarde
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Autoincremental
        public int Id { get; set; }
        [Required]
        public required DateTime Fecha { get; set; }
        [Required]
        public required string Motivo { get; set; }
        [Required]
        public required int MinutosTarde{ get; set; }
        [ForeignKey("IdEmpleado")]
        public required Empleado Empleado { get; set; }
    }
}