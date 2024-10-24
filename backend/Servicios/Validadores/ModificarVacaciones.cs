using FluentValidation;
using Core.DTO;

namespace Servicios.Validadores;

public class VacacionesModificarValidador : AbstractValidator<VacacionesDTOConId>
{
    public VacacionesModificarValidador()
    {
        RuleFor(fi => fi.FechaInicio)
               .NotEmpty().WithMessage("La fecha de inicio es obligatoria.")
               .Must(fecha => fecha != default(DateTime)).WithMessage("La fecha es inválida.");

        RuleFor(ff => ff.FechaFin)
            .NotEmpty().WithMessage("La fecha de fin es obligatoria.")
            .Must(fecha => fecha != default(DateTime)).WithMessage("La fecha es inválida.");



        RuleFor(ff => ff.Aprobado)
               .Equal(true).WithMessage("Vacaciones rechazadas.");


    }
}
