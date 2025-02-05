using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("Vacaciones")]
    public partial class Vacaciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Autoincremental
        public int Id { get; set; }
        [Required]
        public required DateTime FechaSolicitado { get; set; }
        [Required]
        public required DateTime FechaInicio { get; set; }
        [Required]
        public required DateTime FechaFin { get; set; }
        [Required]
        public required EstadoPermiso Estado { get; set; }
        [ForeignKey("IdEmpleado")]
        public required int IdEmpleado { get; set; }  //fk
    }
}