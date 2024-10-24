using FluentValidation;
using Core.DTO;

namespace Servicios.Validadores;

public class EmpresaModificarValidador : AbstractValidator<EmpresaDTOConId>
{
    public EmpresaModificarValidador()
    {
        RuleFor(e => e.Nombre)
               .NotEmpty().WithMessage("El nombre es obligatorio.")
               .MaximumLength(20).WithMessage("El nombre no debe tener más de 20 caracteres.");

        RuleFor(e => e.Direccion)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MaximumLength(250).WithMessage("La apellido no debe tener más de 250 caracteres.");

        RuleFor(e => e.Telefono)
            .NotEmpty().WithMessage("El telefono es obligatorio.")
                .MaximumLength(250).WithMessage("El telefono no debe tener más de 250 caracteres.");



        RuleFor(e => e.Email)
            .NotEmpty().WithMessage("El email es obligatorio.")
                .MaximumLength(250).WithMessage("El email no debe tener más de 250 caracteres.");



    }
}
