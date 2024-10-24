using FluentValidation;
using Core.DTO;

namespace Servicios.Validadores
{
    public class EmpresaAgregarValidador : AbstractValidator<EmpresaDTO>
    {
        public EmpresaAgregarValidador()
        {
            RuleFor(e => e.Nombre) 
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(20).WithMessage("El nombre no debe tener más de 20 caracteres.");

            RuleFor(e => e.Direccion)  
                .NotEmpty().WithMessage("La dirección es obligatoria.")
                .MaximumLength(250).WithMessage("La dirección no debe tener más de 250 caracteres.");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .MaximumLength(250).WithMessage("El email no debe tener más de 250 caracteres.");

            RuleFor(e => e.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .MaximumLength(250).WithMessage("El teléfono no debe tener más de 250 caracteres.");
        }
    }
}

























/*using FluentValidation;
using Core.DTO;

namespace Servicios.Validadores;

public class EmpresaAgregarValidador : AbstractValidator<EmpresaDTO>
{
    public EmpresaAgregarValidador()
    {
        entity.Property(e => e.Nombre).IsRequired();
        entity.Property(e => e.direccion).IsRequired();
        entity.Property(e => e.email).IsRequired();
        entity.Property(e => e.telefono).IsRequired();
        
        RuleFor(e => e.nombre)
               .NotEmpty().WithMessage("El nombre es obligatorio.")
               .MaximumLength(20).WithMessage("El nombre no debe tener más de 20 caracteres.");

        RuleFor(e => e.direccion)
                .NotEmpty().WithMessage("La direccion es obligatoria.")
                .MaximumLength(250).WithMessage("La direccion no debe tener más de 250 caracteres.");

        RuleFor(e => e.email)
            .NotEmpty().WithMessage("El mail es obligatorio.")
                .MaximumLength(250).WithMessage("El mail no debe tener más de 250 caracteres.");

        RuleFor(e => e.telefono)
           .NotEmpty().WithMessage("El telefono es obligatorio.")
               .MaximumLength(250).WithMessage("El telefono no debe tener más de 250 caracteres.");


        // .NotNull().WithMessage("Es necesario indicar si la empresa está completada."); //
    }
} */
