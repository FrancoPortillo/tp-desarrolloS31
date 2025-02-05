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
    public interface IDocumentacion
    {
        Task<int> Agregar(DocumentacionDTO documentacion);
        Task<bool> Eliminar(int id);
        Task<int> Modificar(DocumentacionDTOConId documentacion);
        Task<DocumentacionDTOConId> ObtenerIndividual(int id);
        Task<List<DocumentacionDTOConId>> Obtener();
    }

    public class DocumentacionServicio : IDocumentacion
    {
        private readonly BdRrhhContext _db;

        public DocumentacionServicio(BdRrhhContext db)
        {
            _db = db;
        }

        public async Task<int> Agregar(DocumentacionDTO documentacion)
        {
            // FluentValidation
            var validador = new DocumentacionAgregarValidador();
            var validadorResultado = validador.Validate(documentacion);

            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            // Mapster
            var nuevaDocumentacion = documentacion.Adapt<Documentacion>();
            await _db.Documentacion.AddAsync(nuevaDocumentacion).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return nuevaDocumentacion.Id;
        }

        public async Task<int> Modificar(DocumentacionDTOConId documentacion)
        {
            var validador = new DocumentacionAgregarValidador();
            var validadorResultado = validador.Validate(documentacion);

            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            var documentacionModelo = await _db.Documentacion.FirstOrDefaultAsync(x => x.Id == documentacion.Id).ConfigureAwait(false);

            if (documentacionModelo == null)
            {
                throw new KeyNotFoundException("No se encontró la documentación con el ID especificado.");
            }

            documentacionModelo.NombreArchivo = documentacion.NombreArchivo;
            documentacionModelo.Contenido = documentacion.Content;
            documentacionModelo.IdEmpleado = documentacion.IdEmpleado;

            await _db.SaveChangesAsync().ConfigureAwait(false);

            return documentacionModelo.Id;
        }

        public async Task<bool> Eliminar(int id)
        {
            var documentacionModelo = await _db.Documentacion.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            if (documentacionModelo != null)
            {
                _db.Documentacion.Remove(documentacionModelo);
                await _db.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

            throw new KeyNotFoundException("No se encontró la documentación con el ID especificado.");
        }

        public async Task<List<DocumentacionDTOConId>> Obtener()
        {
            var documentaciones = await _db.Documentacion.ToListAsync().ConfigureAwait(false);
            return documentaciones.Adapt<List<DocumentacionDTOConId>>();
        }

        public async Task<DocumentacionDTOConId> ObtenerIndividual(int id)
        {
            var documentacionModelo = await _db.Documentacion.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            if (documentacionModelo != null)
            {
                return documentacionModelo.Adapt<DocumentacionDTOConId>();
            }

            throw new KeyNotFoundException("No se encontró la documentación con el ID especificado.");
        }
    }
}