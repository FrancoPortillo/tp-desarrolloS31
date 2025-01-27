using FluentValidation;
using CORE.DTO;

namespace Servicios.Validadores
{
    public class LLegadaTardeAgregarValidador : AbstractValidator<LLegadaTardeDTO>
    {
        public LLegadaTardeAgregarValidador()
        {
            RuleFor(lg => lg.Fecha)
                .NotEmpty().WithMessage("La fecha es obligatoria.")
                .Must(fecha => fecha != default(DateTime)).WithMessage("La fecha es inválida.");

            RuleFor(lg => lg.MinutosTarde)
                .GreaterThan(0).WithMessage("Los minutos tarde deben ser mayores a 0.")
                .LessThanOrEqualTo(120).WithMessage("Los minutos tarde no pueden ser más de 120.");
        }
    }
}