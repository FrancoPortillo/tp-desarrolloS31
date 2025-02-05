using CORE.DTO;
using FluentValidation;

namespace Servicios.Validadores
{
    public class DocumentacionAgregarValidador : AbstractValidator<DocumentacionDTO>
    {
        public DocumentacionAgregarValidador()
        {
            RuleFor(d => d.NombreArchivo)
                .NotEmpty().WithMessage("El nombre del archivo es obligatorio.");

            RuleFor(d => d.Content)
                .NotEmpty().WithMessage("El contenido del archivo es obligatorio.");

            RuleFor(d => d.IdEmpleado)
                .GreaterThan(0).WithMessage("El ID del empleado debe ser un número positivo.");
        }
    }
}