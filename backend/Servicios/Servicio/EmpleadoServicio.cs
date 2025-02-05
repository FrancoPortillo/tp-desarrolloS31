using CORE.DTO;
using Data.Contexto;
using Data.Models;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Servicios.Validadores;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public interface IEmpleado
    {
        Task<int> Agregar(EmpleadoDTOConId empleado);
        Task<bool> Eliminar(int id);
        Task<int> Modificar(EmpleadoDTOConId empleado);
        Task<EmpleadoDTOConId> ObtenerIndividual(int id);
        Task<EmpleadoDTOConId> ObtenerPorEmail(string email);
        Task<List<EmpleadoDTOConId>> Obtener();
    }

    public class EmpleadoServicio : IEmpleado
    {
        private readonly BdRrhhContext _db;

        public EmpleadoServicio(BdRrhhContext db)
        {
            _db = db;
        }

        public async Task<EmpleadoDTOConId> ObtenerPorEmail(string email)
        {
            var empleadoModelo = await _db.Empleado.FirstOrDefaultAsync(x => x.Email == email).ConfigureAwait(false);

            if (empleadoModelo != null)
            {
                return empleadoModelo.Adapt<EmpleadoDTOConId>();
            }

            throw new KeyNotFoundException("Empleado no encontrado");
        }

        public async Task<int> Agregar(EmpleadoDTOConId empleado)
        {
            var validador = new EmpleadoAgregarValidador();
            var validadorResultado = validador.Validate(empleado);

            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            var nuevoEmpleado = empleado.Adapt<Empleado>();
            await _db.Empleado.AddAsync(nuevoEmpleado).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return nuevoEmpleado.Id;
        }

        public async Task<int> Modificar(EmpleadoDTOConId empleado)
        {
            var validador = new EmpleadoModificarValidador();
            var validadorResultado = validador.Validate(empleado);

            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            var empleadoModelo = await _db.Empleado.FirstOrDefaultAsync(x => x.Id == empleado.Id).ConfigureAwait(false);

            if (empleadoModelo == null)
            {
                throw new KeyNotFoundException("Empleado no encontrado");
            }

            empleadoModelo.Nombre = empleado.Nombre;
            empleadoModelo.Apellido = empleado.Apellido;
            empleadoModelo.Legajo = empleado.Legajo;
            empleadoModelo.Email = empleado.Email;
            empleadoModelo.Edad = empleado.Edad;
            empleadoModelo.Puesto = empleado.Puesto;
            empleadoModelo.Rol = empleado.Rol;
            empleadoModelo.Dni = empleado.Dni;
            empleadoModelo.IdEmpresa = empleado.IdEmpresa;
            empleadoModelo.Telefono = empleado.Telefono;
            empleadoModelo.FotoPerfil = empleado.FotoPerfil; // Actualizar la foto de perfil

            await _db.SaveChangesAsync().ConfigureAwait(false);

            return empleadoModelo.Id;
        }

        public async Task<bool> Eliminar(int id)
        {
            var empleadoModelo = await _db.Empleado.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            if (empleadoModelo != null)
            {
                _db.Empleado.Remove(empleadoModelo);
                await _db.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

            throw new KeyNotFoundException("Empleado no encontrado");
        }

        public async Task<List<EmpleadoDTOConId>> Obtener()
        {
            var empleados = await _db.Empleado.ToListAsync().ConfigureAwait(false);
            return empleados.Adapt<List<EmpleadoDTOConId>>();
        }

        public async Task<EmpleadoDTOConId> ObtenerIndividual(int id)
        {
            var empleadoModelo = await _db.Empleado.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            if (empleadoModelo != null)
            {
                return empleadoModelo.Adapt<EmpleadoDTOConId>();
            }

            throw new KeyNotFoundException("Empleado no encontrado");
        }
    }
}