using Core.DTO;
using Data.Contexto;
using Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Servicios.Validadores;
using Data.Models;
using FluentValidation;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CORE.DTO;

namespace Servicios.Servicios
{
    public interface IInasistencia
    {
        Task<int> Agregar(InasistenciaDTO inasistencia);
        Task<bool> Eliminar(int id);
        Task<int> Modificar(InasistenciaDTOConId inasistencia);
        Task<InasistenciaDTOConId> ObtenerIndividual(int id);
        Task<List<InasistenciaDTOConId>> Obtener();
        Task<List<InasistenciaDTOConId>> ObtenerPorEmpleado(int id);
        Task<int> ObtenerInasistencias(int idEmpleado, DateTime startDate, DateTime endDate);
    }

    public class InasistenciaServicio : IInasistencia
    {
        private readonly BdRrhhContext _db;

        public InasistenciaServicio(BdRrhhContext db)
        {
            _db = db;
        }

        public async Task<int> Agregar(InasistenciaDTO inasistencia)
        {
            // FluentValidation
            var validador = new InasistenciaAgregarValidador();
            var validadorResultado = validador.Validate(inasistencia);

            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            // Mapster
            var nuevaInasistencia = inasistencia.Adapt<Data.Models.Inasistencia>();
            await _db.Inasistencia.AddAsync(nuevaInasistencia).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return nuevaInasistencia.Id;
        }

        public async Task<int> Modificar(InasistenciaDTOConId inasistencia)
        {
            var validador = new InasistenciaModificarValidador();
            var validadorResultado = validador.Validate(inasistencia);

            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            var inasistenciaModelo = await _db.Inasistencia
                .Include(p => p.Documentaciones) // Incluir la relación de Documentaciones
                .FirstOrDefaultAsync(x => x.Id == inasistencia.Id)
                .ConfigureAwait(false);

            if (inasistenciaModelo == null)
            {
                throw new KeyNotFoundException("No se encontró la asistencia con el ID especificado.");
            }

            inasistenciaModelo.Fecha = inasistencia.Fecha;
            inasistenciaModelo.Tipo = Enum.Parse<Tipo>(inasistencia.Tipo, true); // Convertir el string a la enumeración
            inasistenciaModelo.Detalles = inasistencia.Detalles;
            inasistenciaModelo.IdEmpleado = inasistencia.IdEmpleado;

            // Actualizar la lista de Documentaciones
            inasistenciaModelo.Documentaciones.Clear();
            inasistenciaModelo.Documentaciones.AddRange(inasistencia.Documentaciones.Select(d => d.Adapt<Documentacion>()));

            await _db.SaveChangesAsync().ConfigureAwait(false);

            return inasistencia.Id;
        }

        public async Task<bool> Eliminar(int id)
        {
            var inasistenciaModelo = await _db.Inasistencia.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            if (inasistenciaModelo != null)
            {
                _db.Inasistencia.Remove(inasistenciaModelo);
                await _db.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

            throw new KeyNotFoundException("No se encontró la inasistencia con el ID especificado.");
        }

        public async Task<List<InasistenciaDTOConId>> Obtener()
        {
            var inasistencias = await _db.Inasistencia.ToListAsync().ConfigureAwait(false);
            return inasistencias.Adapt<List<InasistenciaDTOConId>>();
        }

        public async Task<InasistenciaDTOConId> ObtenerIndividual(int id)
        {
            var inasistenciaModelo = await _db.Inasistencia
                .Include(p => p.Documentaciones) // Incluir la relación de Documentaciones
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            if (inasistenciaModelo != null)
            {
                return inasistenciaModelo.Adapt<InasistenciaDTOConId>();
            }

            throw new KeyNotFoundException("No se encontró la inasistencia con el ID especificado.");
        }
        public async Task<List<InasistenciaDTOConId>> ObtenerPorEmpleado(int id)
        {
            var inasistenciaModelo = await _db.Inasistencia
                .Include(p => p.Documentaciones)
                .Where(x => x.IdEmpleado == id)
                .ToListAsync()
                .ConfigureAwait(false);

            if (inasistenciaModelo.Any())
            {
                return inasistenciaModelo.Adapt<List<InasistenciaDTOConId>>();
            }

            throw new KeyNotFoundException("No se encontraron inasistencias para el empleado con el ID especificado.");
        }
        public async Task<int> ObtenerInasistencias(int idEmpleado, DateTime startDate, DateTime endDate)
        {
            var inasistencias = await _db.Inasistencia
            .Where(i => i.IdEmpleado == idEmpleado && i.Fecha >= startDate && i.Fecha <= endDate)
            .CountAsync()
            .ConfigureAwait(false);

            return inasistencias;
        }
    }
}