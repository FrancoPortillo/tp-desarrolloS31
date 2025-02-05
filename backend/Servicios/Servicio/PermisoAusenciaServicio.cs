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
    public interface IPermisoAusencia
    {
        Task<int> Agregar(PermisoAusenciaDTO permiso);
        Task<bool> Eliminar(int id);
        Task<int> Modificar(PermisoAusenciaDTOConId permiso);
        Task<PermisoAusenciaDTOConId> ObtenerIndividual(int id);
        Task<List<PermisoAusenciaDTOConId>> Obtener();
        Task<List<PermisoAusenciaDTOConId>> ObtenerPorEmpleado(int id);
    }

    public class PermisoAusenciaServicio : IPermisoAusencia
    {
        private readonly BdRrhhContext _db;

        public PermisoAusenciaServicio(BdRrhhContext db)
        {
            _db = db;
        }

        public async Task<int> Agregar(PermisoAusenciaDTO permiso)
        {
            // FluentValidation
            var validador = new PermisoAgregarValidador();
            var validadorResultado = validador.Validate(permiso);

            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            // Mapster
            var nuevoPermiso = permiso.Adapt<Data.Models.PermisoAusencia>();
            await _db.PermisoAusencia.AddAsync(nuevoPermiso).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return nuevoPermiso.Id;
        }

        public async Task<int> Modificar(PermisoAusenciaDTOConId permiso)
        {
            var validador = new PermisoModificarValidador();
            var validadorResultado = validador.Validate(permiso);

            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            var permisoModelo = await _db.PermisoAusencia
                .Include(p => p.Documentaciones) // Incluir la relación de Documentaciones
                .FirstOrDefaultAsync(x => x.Id == permiso.Id)
                .ConfigureAwait(false);

            if (permisoModelo == null)
            {
                throw new KeyNotFoundException("No se encontró el permiso con el ID especificado.");
            }

            permisoModelo.FechaInicio = permiso.FechaInicio;
            permisoModelo.FechaFin = permiso.FechaFin;
            permisoModelo.FechaSolicitado = permiso.FechaSolicitado;
            permisoModelo.Tipo = Enum.Parse<Tipo>(permiso.Tipo, true); // Convertir el string a la enumeración
            permisoModelo.Detalles = permiso.Detalles;
            permisoModelo.Estado = Enum.Parse<EstadoPermiso>(permiso.Estado, true); // Convertir el string a la enumeración
            permisoModelo.IdEmpleado = permiso.IdEmpleado;

            // Actualizar la lista de Documentaciones
            permisoModelo.Documentaciones.Clear();
            permisoModelo.Documentaciones.AddRange(permiso.Documentaciones.Select(d => d.Adapt<Documentacion>()));

            await _db.SaveChangesAsync().ConfigureAwait(false);

            return permisoModelo.Id;
        }

        public async Task<bool> Eliminar(int id)
        {
            var permisoModelo = await _db.PermisoAusencia.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            if (permisoModelo != null)
            {
                _db.PermisoAusencia.Remove(permisoModelo);
                await _db.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

            throw new KeyNotFoundException("No se encontró el permiso con el ID especificado.");
        }

        public async Task<List<PermisoAusenciaDTOConId>> Obtener()
        {
            var permisos = await _db.PermisoAusencia.ToListAsync().ConfigureAwait(false);
            return permisos.Adapt<List<PermisoAusenciaDTOConId>>();
        }

        public async Task<PermisoAusenciaDTOConId> ObtenerIndividual(int id)
        {
            var permisoModelo = await _db.PermisoAusencia
                .Include(p => p.Documentaciones) // Incluir la relación de Documentaciones
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            if (permisoModelo != null)
            {
                return permisoModelo.Adapt<PermisoAusenciaDTOConId>();
            }

            throw new KeyNotFoundException("No se encontró el permiso con el ID especificado.");
        }
        public async Task<List<PermisoAusenciaDTOConId>> ObtenerPorEmpleado(int id)
        {
            var permisosModelo = await _db.PermisoAusencia
                .Include(p => p.Documentaciones)
                .Where(x => x.IdEmpleado == id)
                .ToListAsync()
                .ConfigureAwait(false);

            if (permisosModelo.Any())
            {
                return permisosModelo.Adapt<List<PermisoAusenciaDTOConId>>();
            }

            throw new KeyNotFoundException("No se encontraron permisos para el empleado con el ID especificado.");
        }
    }
}