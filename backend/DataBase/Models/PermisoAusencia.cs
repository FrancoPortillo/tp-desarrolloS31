using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("PermisoAusencia")]
    public class PermisoAusencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Autoincremental
        public required int Id { get; set; }
        [Required]
        public required DateTime FechaInicio { get; set; }
        [Required]
        public required DateTime FechaFin { get; set; }
        [Required]
        public required DateTime FechaSolicitado { get; set; }
        [Required]
        public required Tipo Tipo { get; set; }
        [Required]
        public required string Detalles { get; set; }
        [Required]
        public required EstadoPermiso Estado { get; set; }
        [ForeignKey("IdEmpleado")]
        public required int IdEmpleado { get; set; }
        [Required]
        public List<Documentacion> Documentaciones { get; set; } = new List<Documentacion>();
    }
    public enum EstadoPermiso
    {
        Pendiente,
        Aprobado,
        Rechazado
    }
}
