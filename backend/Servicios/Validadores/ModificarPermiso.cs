using CORE.DTO;
using Data.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Validadores
{
    public class PermisoModificarValidador : AbstractValidator<PermisoAusenciaDTOConId>
    {
        public PermisoModificarValidador()
        {
            RuleFor(a => a.FechaInicio)
                .NotEmpty().WithMessage("La fecha de inicio es obligatoria.")
                .Must(fecha => fecha != default(DateTime)).WithMessage("La fecha de inicio es inválida.");

            RuleFor(a => a.FechaFin)
                .NotEmpty().WithMessage("La fecha de fin es obligatoria.")
                .Must(fecha => fecha != default(DateTime)).WithMessage("La fecha de fin es inválida.")
                .GreaterThan(a => a.FechaInicio).WithMessage("La fecha de fin debe ser posterior a la fecha de inicio.");

            RuleFor(a => a.FechaSolicitado)
                .NotEmpty().WithMessage("La fecha de solicitud es obligatoria.")
                .Must(fecha => fecha != default(DateTime)).WithMessage("La fecha de solicitud es inválida.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de solicitud no puede ser futura.");

            RuleFor(a => a.Estado)
                .NotEmpty().WithMessage("El estado es obligatorio.")
                .IsEnumName(typeof(EstadoPermiso), caseSensitive: false).WithMessage("El estado es inválido.");

            RuleFor(a => a.Detalles)
                .NotNull().WithMessage("El detalle del permiso es obligatorio.")
                .NotEmpty().WithMessage("El detalle del permiso no puede estar vacío.");

            RuleFor(a => a.Tipo)
                .NotEmpty().WithMessage("El tipo de permiso es obligatorio.");

            RuleFor(a => a.IdEmpleado)
                .GreaterThan(0).WithMessage("El ID del empleado debe ser un número positivo.");
        }
    }
}
