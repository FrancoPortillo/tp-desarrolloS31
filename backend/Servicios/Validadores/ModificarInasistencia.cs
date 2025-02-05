using CORE.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Validadores
{
    public class InasistenciaModificarValidador : AbstractValidator<InasistenciaDTO>
    {
        public  InasistenciaModificarValidador()
        {
            RuleFor(i => i.Fecha)
                .NotEmpty().WithMessage("La fecha es obligatoria.")
                .Must(fecha => fecha != default(DateTime)).WithMessage("La fecha es inválida.");

            RuleFor(i => i.Detalles)
                .NotNull().WithMessage("El detalle del permiso es obligatorio.")
                .NotEmpty().WithMessage("El detalle del permiso no puede estar vacío.");

            RuleFor(i => i.Tipo)
                .NotEmpty().WithMessage("El tipo de permiso es obligatorio.");

            RuleFor(i => i.IdEmpleado)
                .GreaterThan(0).WithMessage("El ID del empleado debe ser un número positivo.");
        }
    }
}
