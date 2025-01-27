using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("Asistencia")]
    public class Asistencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Autoincremental
        public int Id { get; set; } 
        [Required]
        public required DateTime Fecha { get; set; }
        [Required]
        public required Boolean Presente { get; set; }
        [ForeignKey("IdEmpleado")]
        public required Empleado Empleado { get; set; }  //fk
    }
}