using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("Empresa")]
    public partial class Empresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Autoincremental
        public int Id { get; set; } 
        [Required]
        public required string Nombre{ get; set; }
        [Required]
        public  required string Telefono { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public  required string Direccion { get; set; }  
    }
}


