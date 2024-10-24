using FluentValidation;
using Core.DTO;

namespace Servicios.Validadores;

public class LLegadaTardeModificarValidador : AbstractValidator<LlegadaTardeDTOConId>
{
    public LLegadaTardeModificarValidador()
    {
        RuleFor(lg => lg.Fecha)
           .NotEmpty().WithMessage("La fecha es obligatoria.")
           .Must(fecha => fecha != default(DateTime)).WithMessage("La fecha es inválida.");


        RuleFor(lg => lg.MinutosTarde)
            .NotEmpty().WithMessage("La fecha es obligatoria.")
            .LessThanOrEqualTo(120).WithMessage("Los minutos tarde no pueden ser más de 60.");
        ;


    }



}
}
