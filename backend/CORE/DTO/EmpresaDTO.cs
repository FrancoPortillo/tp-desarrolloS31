using System;

namespace Core.DTO
{
    public class EmpresaDTO
    {
        public required string Nombre { get; set; }

        public required string Telefono { get; set; }

        public required string Email { get; set; }

        public required string Direccion { get; set; }
    }

    public class EmpresaDTOConId : EmpresaDTO
    {
        public int Id { get; set; }
    }
}


















