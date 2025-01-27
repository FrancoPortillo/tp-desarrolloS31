using FluentValidation;
using CORE.DTO;
using CORE.DTO.Core.DTO;

namespace Servicios.Validadores
{
    public class AsistenciaAgregarValidador : AbstractValidator<AsistenciaDTO>
    {
        public AsistenciaAgregarValidador()
        {
            RuleFor(a => a.Fecha)
                .NotEmpty().WithMessage("La fecha es obligatoria.")
                .Must(fecha => fecha != default(DateTime)).WithMessage("La fecha es inválida.");

            RuleFor(a => a.Presente)
                .NotNull().WithMessage("El estado de presencia es obligatorio.");
        }
    }
}