using CORE.DTO;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Servicios.Servicios;
using Servicios.Validadores;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecursosHumanos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermisoAusenciaController : ControllerBase
    {
        private readonly IPermisoAusencia _permisoAusenciaServicio;

        public PermisoAusenciaController(IPermisoAusencia permisoAusenciaServicio)
        {
            _permisoAusenciaServicio = permisoAusenciaServicio;
        }
        [HttpGet("ObtenerPorEmpleado/{id}")]
        public async Task<ActionResult<List<PermisoAusenciaDTOConId>>> ObtenerPorEmpleado(int id)
        {
            try
            {
                var permisos = await _permisoAusenciaServicio.ObtenerPorEmpleado(id);
                return Ok(permisos);
            }
            catch (Exception ex)
            {
                // Manejar la excepción y devolver una respuesta adecuada
                return StatusCode(500, new { message = "Error al obtener permisos de ausencia", details = ex.Message });
            }

        }
        [HttpGet("Obtener")]
        public async Task<ActionResult<List<PermisoAusenciaDTOConId>>> Obtener()
        {
            var permisos = await _permisoAusenciaServicio.Obtener();
            return Ok(permisos);
        }

        [HttpGet("ObtenerIndividual/{id}")]
        public async Task<ActionResult<PermisoAusenciaDTOConId>> ObtenerIndividual(int id)
        {
            var permiso = await _permisoAusenciaServicio.ObtenerIndividual(id);
            if (permiso == null)
            {
                return NotFound();
            }
            return Ok(permiso);
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult<int>> Agregar(PermisoAusenciaDTO permiso)
        {
            var id = await _permisoAusenciaServicio.Agregar(permiso);
            return Ok(id);
        }

        [HttpPut("Modificar")]
        public async Task<ActionResult> Modificar(PermisoAusenciaDTOConId permiso)
        {
            var validador = new PermisoModificarValidador();
            var validadorResultado = validador.Validate(permiso);
            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }
            try
            {
                var nuevoId = await _permisoAusenciaServicio.Modificar(permiso).ConfigureAwait(false);
                if (nuevoId > 0)
                {
                    return Ok($"Permiso modificado con éxito, ID: {nuevoId}");
                }
                return NotFound("No se encontró el permiso para modificar.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
        [HttpPatch("Modificar/{estado}/{id}")]
        public async Task<ActionResult> ModificarEstado(int id, string estado)
        {
            
            try
            {
                var permiso = await _permisoAusenciaServicio.ModificarEstado(id, estado);
                if (permiso != null)
                {
                    return Ok(permiso);
                }
                return NotFound("No se encontró el permiso para modificar.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }

        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var eliminado = await _permisoAusenciaServicio.Eliminar(id);
            if (!eliminado)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}