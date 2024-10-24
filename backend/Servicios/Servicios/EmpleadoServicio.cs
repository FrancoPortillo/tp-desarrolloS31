using Core.DTO;
using Data.Contexto;
using Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Servicios.Validadores;
using CORE.DTO;
using FluentValidation;

namespace Servicios.Servicios
{
    public interface IEmpleado
    {
        Task<int> Agregar(EmpleadoDTO Empleado);
        Task<bool> Eliminar(int id);
        Task<int> Modificar(EmpleadoDTOConId Empleado);
        Task<EmpleadoDTOConId> ObtenerIndividual(int id);
        Task<List<EmpleadoDTOConId>> Obtener();
    }

    public class EmpleadoServicio : IEmpleado
    {
        private readonly BdRrhhContext _db;

        public EmpleadoServicio(BdRrhhContext db)
        {
            _db = db;
        }

        public async Task<int> Agregar(EmpleadoDTO Empleado)
        {
            var validador = new EmpleadoAgregarValidador();
            var validadorResultado = validador.Validate(Empleado);

            if (!validadorResultado.IsValid)
            {
                throw new Exception("Validación fallida");
            }

            var nuevoEmpleado = Empleado.Adapt<Data.Models.Empleado>();
            await _db.Empleado.AddAsync(nuevoEmpleado).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return nuevoEmpleado.Id;
        }

        public async Task<int> Modificar(EmpleadoDTOConId Empleado)
        {
            var validador = new EmpleadoModificarValidador();
            var validadorResultado = validador.Validate(Empleado);

            if (!validadorResultado.IsValid)
            {
                throw new Exception("Validación fallida");
            }

            var empleadoModelo = await _db.Empleado.FirstOrDefaultAsync(x => x.Id == Empleado.Id).ConfigureAwait(false);

            if (empleadoModelo == null)
            {
                throw new Exception("Empleado no encontrado");
            }

            empleadoModelo.Nombre = Empleado.Nombre;
            empleadoModelo.Apellido = Empleado.Apellido;
            empleadoModelo.Legajo = Empleado.legajo;
            empleadoModelo.Email = Empleado.email;
            empleadoModelo.Edad = Empleado.edad;
            empleadoModelo.Puesto = Empleado.puesto;

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

            throw new Exception("Empleado no encontrado");
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

            throw new Exception("Empleado no encontrado");
        }
    }
}

