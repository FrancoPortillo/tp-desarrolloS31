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
    public interface IVacaciones
    {
        Task<int> Agregar(VacacionesDTO vacaciones);
        Task<bool> Eliminar(int id);
        Task<int> Modificar(VacacionesDTOConId vacaciones);
        Task<VacacionesDTOConId> ObtenerIndividual(int id);
        Task<List<VacacionesDTOConId>> Obtener();
    }

    public class VacacionesServicio : IVacaciones
    {
        private readonly BdRrhhContext _db;

        public VacacionesServicio(BdRrhhContext db)
        {
            _db = db;
        }

        public async Task<int> Agregar(VacacionesDTO vacaciones)
        {
            // FluentValidation
            var validador = new VacacionesAgregarValidador();
            var validadorResultado = validador.Validate(vacaciones);

            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            // Mapster
            var nuevaVacaciones = vacaciones.Adapt<Data.Models.Vacaciones>();
            await _db.Vacaciones.AddAsync(nuevaVacaciones).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return nuevaVacaciones.Id;
        }

        public async Task<int> Modificar(VacacionesDTOConId vacaciones)
        {
            var validador = new VacacionesModificarValidador();
            var validadorResultado = validador.Validate(vacaciones);

            // Validar las vacaciones
            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            var vacacionesModelo = await _db.Vacaciones.FirstOrDefaultAsync(x => x.Id == vacaciones.Id).ConfigureAwait(false);

            if (vacacionesModelo == null)
            {
                throw new KeyNotFoundException("Vacaciones no encontradas");
            }

            vacacionesModelo.FechaInicio = vacaciones.FechaInicio;
            vacacionesModelo.FechaFin = vacaciones.FechaFin;
            vacacionesModelo.Aprobado = vacaciones.Aprobado;
            vacacionesModelo.IdEmpleado = vacaciones.IdEmpleado;

            await _db.SaveChangesAsync().ConfigureAwait(false);

            return vacacionesModelo.Id;
        }

        public async Task<bool> Eliminar(int id)
        {
            var vacacionesModelo = await _db.Vacaciones.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            if (vacacionesModelo != null)
            {
                _db.Vacaciones.Remove(vacacionesModelo);
                await _db.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

            throw new KeyNotFoundException("Vacaciones no encontradas");
        }

        public async Task<List<VacacionesDTOConId>> Obtener()
        {
            var vacaciones = await _db.Vacaciones.ToListAsync().ConfigureAwait(false);
            return vacaciones.Adapt<List<VacacionesDTOConId>>();
        }

        public async Task<VacacionesDTOConId> ObtenerIndividual(int id)
        {
            var vacacionesModelo = await _db.Vacaciones.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            if (vacacionesModelo != null)
            {
                return vacacionesModelo.Adapt<VacacionesDTOConId>();
            }

            throw new KeyNotFoundException("Vacaciones no encontradas");
        }
    }
}