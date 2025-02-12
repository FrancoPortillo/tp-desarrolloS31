using CORE.DTO;
using Microsoft.AspNetCore.Mvc;
using Servicios.Servicios;

namespace RecursosHumanos.Controllers
{
    public class DocumentacionController : Controller
    {
        private readonly IDocumentacion _documentacionServicio;

        public DocumentacionController(IDocumentacion documentacionServicio)
        {
            _documentacionServicio = documentacionServicio;
        }
        [HttpGet("Descargar/{id}")]
        public async Task<string> Descargar(int idDocumentacion)
        {
            var ruta = await _documentacionServicio.Descargar(idDocumentacion);
            return ruta;
        }
        [HttpPost("SubirArchivo")]
        public async Task<string> SubirArchivo(int idDocumentacion, IFormFile archivo)
        {
            var ruta = await _documentacionServicio.SubirArchivo(idDocumentacion, archivo);
            return ruta;
        }
        [HttpGet("Obtener")]
        public async Task<ActionResult<List<DocumentacionDTOConId>>> Obtener()
        {
            var documentaciones = await _documentacionServicio.Obtener();
            return Ok(documentaciones);
        }

        [HttpGet("ObtenerIndividual/{id}")]
        public async Task<ActionResult<DocumentacionDTOConId>> ObtenerIndividual(int id)
        {
            var documentacion = await _documentacionServicio.ObtenerIndividual(id);
            if (documentacion == null)
            {
                return NotFound();
            }
            return Ok(documentacion);
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult<int>> Agregar(DocumentacionDTO documentacion)
        {
            var id = await _documentacionServicio.Agregar(documentacion);
            return CreatedAtAction(nameof(ObtenerIndividual), new { id }, id);
        }

        [HttpPut("Modificar/{id}")]
        public async Task<ActionResult> Modificar(int id, DocumentacionDTOConId documentacion)
        {
            if (id != documentacion.Id)
            {
                return BadRequest("El ID de la documentación no coincide.");
            }

            await _documentacionServicio.Modificar(documentacion);
            return NoContent();
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var eliminado = await _documentacionServicio.Eliminar(id);
            if (!eliminado)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
