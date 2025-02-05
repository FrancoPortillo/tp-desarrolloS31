using CORE.DTO;
using Microsoft.AspNetCore.Mvc;
using Servicios.Servicios;
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
            var permisos = await _permisoAusenciaServicio.ObtenerPorEmpleado(id);
            return Ok(permisos);
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
            return CreatedAtAction(nameof(ObtenerIndividual), new { id }, id);
        }

        [HttpPut("Modificar/{id}")]
        public async Task<ActionResult> Modificar(int id, PermisoAusenciaDTOConId permiso)
        {
            if (id != permiso.Id)
            {
                return BadRequest("El ID del permiso no coincide.");
            }

            await _permisoAusenciaServicio.Modificar(permiso);
            return NoContent();
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