using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Data.Models
{
    [Table("Inasistencia")]
    public class Inasistencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Autoincremental
        public required int Id { get; set; }
        [Required]
        public required Tipo Tipo { get; set; }
        [Required]
        public required DateTime Fecha { get; set; }
        [Required]
        public required string Detalles { get; set; }
        [Required]
        public List<Documentacion> Documentaciones { get; set; } = new List<Documentacion>();

        [ForeignKey("IdEmpleado")]
        public required int IdEmpleado { get; set; }
    }
    public enum Tipo
    {
        Familiar,
        Medico,
        Otro
    }
}
