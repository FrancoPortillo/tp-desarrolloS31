﻿using FluentValidation;
using CORE.DTO;

namespace Servicios.Validadores
{
    public class EmpleadoModificarValidador : AbstractValidator<EmpleadoDTOConId>
    {
        public EmpleadoModificarValidador()
        {
            RuleFor(e => e.Dni)
                .NotEmpty().WithMessage("El DNI es obligatorio.")
                .Matches(@"^\d{8}$").WithMessage("El DNI debe tener 8 dígitos.");

            RuleFor(e => e.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .Matches(@"^\d{10}$").WithMessage("El teléfono debe tener 10 dígitos.")
                .Matches(@"^\+?\d{1,3}?[-.\s]?\(?\d{1,4}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,9}$")
                .WithMessage("El teléfono debe ser un número válido.");

            RuleFor(a => a.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.");

            RuleFor(a => a.Apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio.");

            RuleFor(a => a.Legajo)
                .GreaterThan(0).WithMessage("El legajo debe ser un número positivo.");

            RuleFor(a => a.Edad)
                .InclusiveBetween(18, 65).WithMessage("La edad debe estar entre 18 y 65 años.");

            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El email no tiene un formato válido.");

            RuleFor(a => a.Puesto)
                .NotEmpty().WithMessage("El puesto es obligatorio.");

            RuleFor(a => a.Rol)
                .NotEmpty().WithMessage("El rol es obligatorio.");
        }
    }
}