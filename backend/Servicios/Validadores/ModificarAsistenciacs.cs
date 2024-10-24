﻿using FluentValidation;
using Core.DTO;

namespace Servicios.Validadores;

public class AsistenciaModificarValidador : AbstractValidator <AsistenciaDTOConId>
{
    public AsistenciaModificarValidador()
    {
        RuleFor(a => a.Fecha)
            .NotEmpty().WithMessage("La fecha es obligatoria.")
            .Must(fecha => fecha != default(DateTime)).WithMessage("La fecha es inválida.");


        RuleFor(a => a.Presente)
           .Equal(true).WithMessage("El presente es obligatorio.");


    }
}