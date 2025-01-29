using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("Empleado")]
    public class Empleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Autoincremental
        public int Id { get; set; }
        [Required] 
        public required string Nombre { get; set; }
        [Required]
        public required string Apellido{ get; set; }
        [Required]
        public required int Legajo{ get; set; }
        [Required]
        public required int Edad {  get; set; }
        [Required]
        public required string Dni { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Puesto { get; set; }
        [Required]
        public required string Telefono { get; set; }
        [Required]
        public required string Rol { get; set; }
        [ForeignKey("IdEmpresa")]
        public required Empresa Empresa { get; set; }  //fk
    }
}