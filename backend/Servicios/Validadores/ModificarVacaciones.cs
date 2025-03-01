﻿using FluentValidation;
using CORE.DTO;

namespace Servicios.Validadores
{
    public class VacacionesModificarValidador : AbstractValidator<VacacionesDTOConId>
    {
        public VacacionesModificarValidador()
        {
            RuleFor(fi => fi.FechaInicio)
                .NotEmpty().WithMessage("La fecha de inicio es obligatoria.")
                .Must(fecha => fecha != default(DateTime)).WithMessage("La fecha es inválida.");

            RuleFor(ff => ff.FechaFin)
                .NotEmpty().WithMessage("La fecha de fin es obligatoria.")
                .Must(fecha => fecha != default(DateTime)).WithMessage("La fecha es inválida.")
                .GreaterThan(fi => fi.FechaInicio).WithMessage("La fecha de fin debe ser posterior a la fecha de inicio.");

            RuleFor(ff => ff.Estado)
                .NotNull().WithMessage("El estado de aprobación es obligatorio.");
        }
    }
}