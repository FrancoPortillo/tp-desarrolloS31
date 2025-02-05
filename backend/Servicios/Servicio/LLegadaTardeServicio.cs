using CORE.DTO;
using Data.Contexto;
using Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Servicios.Validadores;
using FluentValidation;

namespace Servicios.Servicios
{
    public interface ILlegadaTarde
    {
        Task<int> Agregar(LLegadaTardeDTO llegadaTarde);
        Task<bool> Eliminar(int id);
        Task<int> Modificar(LLegadaTardeDTOConId llegadaTarde);
        Task<LLegadaTardeDTOConId> ObtenerIndividual(int id);
        Task<int> ObtenerLlegadasTarde(int idempleado, DateTime startDate, DateTime endDate);
        Task<List<LLegadaTardeDTOConId>> Obtener();
    }

    public class LlegadaTardeServicio : ILlegadaTarde
    {
        private readonly BdRrhhContext _db;

        public LlegadaTardeServicio(BdRrhhContext db)
        {
            _db = db;
        }
        public async Task<int> ObtenerLlegadasTarde(int idEmpleado, DateTime startDate, DateTime endDate)
        {
            var llegadasTarde = await _db.LLegadaTarde
            .Where(l => l.IdEmpleado == idEmpleado && l.Fecha >= startDate && l.Fecha <= endDate)
            .CountAsync()
            .ConfigureAwait(false);

            return llegadasTarde;
        }
        public async Task<int> Agregar(LLegadaTardeDTO llegadaTarde)
        {
            // FluentValidation
            var validador = new LLegadaTardeAgregarValidador();
            var validadorResultado = validador.Validate(llegadaTarde);

            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            // Mapster
            var nuevaLlegadaTarde = llegadaTarde.Adapt<Data.Models.LLegadaTarde>();
            await _db.LLegadaTarde.AddAsync(nuevaLlegadaTarde).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return nuevaLlegadaTarde.Id;
        }

        public async Task<int> Modificar(LLegadaTardeDTOConId llegadaTarde)
        {
            var validador = new LLegadaTardeModificarValidador();
            var validadorResultado = validador.Validate(llegadaTarde);

            // Validar la LlegadaTarde
            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            var llegadaTardeModelo = await _db.LLegadaTarde.FirstOrDefaultAsync(x => x.Id == llegadaTarde.Id).ConfigureAwait(false);

            if (llegadaTardeModelo == null)
            {
                throw new KeyNotFoundException("Llegada tarde no encontrada");
            }

            llegadaTardeModelo.Fecha = llegadaTarde.Fecha;
            llegadaTardeModelo.MinutosTarde = llegadaTarde.MinutosTarde;
            llegadaTardeModelo.IdEmpleado = llegadaTarde.IdEmpleado;

            await _db.SaveChangesAsync().ConfigureAwait(false);

            return llegadaTardeModelo.Id;
        }

        public async Task<bool> Eliminar(int id)
        {
            var llegadaTardeModelo = await _db.LLegadaTarde.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            if (llegadaTardeModelo != null)
            {
                _db.LLegadaTarde.Remove(llegadaTardeModelo);
                await _db.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

            throw new KeyNotFoundException("Llegada tarde no encontrada");
        }

        public async Task<List<LLegadaTardeDTOConId>> Obtener()
        {
            var llegadasTarde = await _db.LLegadaTarde.ToListAsync().ConfigureAwait(false);
            return llegadasTarde.Adapt<List<LLegadaTardeDTOConId>>();
        }

        public async Task<LLegadaTardeDTOConId> ObtenerIndividual(int id)
        {
            var llegadaTardeModelo = await _db.LLegadaTarde.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            if (llegadaTardeModelo != null)
            {
                return llegadaTardeModelo.Adapt<LLegadaTardeDTOConId>();
            }

            throw new KeyNotFoundException("Llegada tarde no encontrada");
        }
    }
}