using FluentValidation;
using CORE.DTO;

namespace Servicios.Validadores
{
    public class EmpleadoAgregarValidador : AbstractValidator<EmpleadoDTO>
    {
        public EmpleadoAgregarValidador()
        {
            RuleFor(e => e.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .Matches(@"^\d{10}$").WithMessage("El teléfono debe tener 10 dígitos.");
                
            RuleFor(e => e.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.");

            RuleFor(e => e.Apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio.");
            
            RuleFor(e => e.Legajo)
                .GreaterThan(0).WithMessage("El legajo debe ser un número positivo.");

            RuleFor(e => e.Edad)
                .InclusiveBetween(18, 65).WithMessage("La edad debe estar entre 18 y 65 años.");

            RuleFor(e => e.Dni)
                .NotEmpty().WithMessage("El DNI es obligatorio.")
                .Matches(@"^\d{8}$").WithMessage("El DNI debe tener 8 dígitos.");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El email no tiene un formato válido.");

            RuleFor(e => e.Puesto)
                .NotEmpty().WithMessage("El puesto es obligatorio.");

            RuleFor(e => e.Rol)
                .NotEmpty().WithMessage("El rol es obligatorio.");
        }
    }
}