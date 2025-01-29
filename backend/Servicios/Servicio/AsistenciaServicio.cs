using Core.DTO;
using Data.Contexto;
using Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Servicios.Validadores;
using Data.Models;
using CORE.DTO.Core.DTO;
using FluentValidation;

namespace Servicios.Servicios
{
    public interface IAsistencia
    {
        Task<int> Agregar(AsistenciaDTO asistencia);
        Task<bool> Eliminar(int id);
        Task<int> Modificar(AsistenciaDTOConId asistencia);
        Task<AsistenciaDTOConId> ObtenerIndividual(int id);
        Task<int> ObtenerInasistencias(int idEmpleado);
        Task RegistrarAsistencia(List<AsistenciaDTOConId> asistencias);
        Task<List<AsistenciaDTOConId>> Obtener();
    }

    public class AsistenciaServicio : IAsistencia
    {
        private readonly BdRrhhContext _db;

        public AsistenciaServicio(BdRrhhContext db)
        {
            _db = db;
        }
        public async Task RegistrarAsistencia(List<AsistenciaDTOConId> asistencias)
        {
            foreach (var asistencia in asistencias)
            {
                var nuevaAsistencia = asistencia.Adapt<Data.Models.Asistencia>();
                await _db.Asistencia.AddAsync(nuevaAsistencia).ConfigureAwait(false);
            }
            await _db.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task<int> ObtenerInasistencias(int idEmpleado)
        {
            var inasistencias = await _db.Asistencia
            .Where(a => a.Empleado.Id == idEmpleado && !a.Presente)
            .CountAsync()
            .ConfigureAwait(false);

            return inasistencias;
        }
        public async Task<int> Agregar(AsistenciaDTO asistencia)
        {
            // FluentValidation
            var validador = new AsistenciaAgregarValidador();
            var validadorResultado = validador.Validate(asistencia);

            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            // Mapster
            var nuevaAsistencia = asistencia.Adapt<Data.Models.Asistencia>();
            await _db.Asistencia.AddAsync(nuevaAsistencia).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return nuevaAsistencia.Id;
        }

        public async Task<int> Modificar(AsistenciaDTOConId asistencia)
        {
            var validador = new AsistenciaModificarValidador();
            var validadorResultado = validador.Validate(asistencia);

            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            var asistenciaModelo = await _db.Asistencia.FirstOrDefaultAsync(x => x.Id == asistencia.Id).ConfigureAwait(false);

            if (asistenciaModelo == null)
            {
                throw new KeyNotFoundException("No se encontró la asistencia con el ID especificado.");
            }

            asistenciaModelo.Fecha = asistencia.Fecha;
            asistenciaModelo.Presente = asistencia.Presente;
            asistenciaModelo.Empleado.Id = asistencia.IdEmpleado;

            await _db.SaveChangesAsync().ConfigureAwait(false);

            return asistenciaModelo.Id;
        }

        public async Task<bool> Eliminar(int id)
        {
            var asistenciaModelo = await _db.Asistencia.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            if (asistenciaModelo != null)
            {
                _db.Asistencia.Remove(asistenciaModelo);
                await _db.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

            throw new KeyNotFoundException("No se encontró la asistencia con el ID especificado.");
        }

        public async Task<List<AsistenciaDTOConId>> Obtener()
        {
            var asistencias = await _db.Asistencia.ToListAsync().ConfigureAwait(false);
            return asistencias.Adapt<List<AsistenciaDTOConId>>();
        }

        public async Task<AsistenciaDTOConId> ObtenerIndividual(int id)
        {
            var asistenciaModelo = await _db.Asistencia.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            if (asistenciaModelo != null)
            {
                return asistenciaModelo.Adapt<AsistenciaDTOConId>();
            }

            throw new KeyNotFoundException("No se encontró la asistencia con el ID especificado.");
        }
    }
}