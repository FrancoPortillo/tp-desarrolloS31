using Mapster;
using Core;
using Core.DTO;
using Servicios;
using Microsoft.AspNetCore.Mvc;
using Servicios.Validadores;
using CORE.DTO.Core.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private readonly IAsistencia _Asistencia;

        public AsistenciaController(IAsistencia Asistencia)
        {
            _Asistencia = Asistencia;
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult<string>> Agregar(AsistenciaDTO Asistencia)
        {
            Log.Information("Intentando agregar una nueva Asistencia: {@Asistencia}", Asistencia);

            var validador = new AsistenciaAgregarValidador();
            var validadorResultado = validador.Validate(Asistencia);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            var nuevoId = await _Asistencia.Agregar(Asistencia).ConfigureAwait(false);
            if (nuevoId > 0)
            {
                return Ok($"Asistencia creada con éxito");
                Log.Information("Asistencia agregada con éxito, ID: {NuevoId}", nuevoId);
            }

            Log.Warning("Error al crear la Asistencia.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la Asistencia.");
        }

        [HttpPut("Modificar")]
        public async Task<ActionResult<string>> Modificar(AsistenciaDTOConId Asistencia)
        {
            var validador = new AsistenciaModificarValidador();
            var validadorResultado = validador.Validate(Asistencia);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            var nuevoId = await _Asistencia.Modificar(Asistencia).ConfigureAwait(false);

            if (nuevoId > 0)
            {
                return Ok($"Asistencia modificada con éxito, ID: {nuevoId}");
            }

            return NotFound("No se encontró la Asistencia para modificar.");
        }




        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var resultado = await _Asistencia.Eliminar(id).ConfigureAwait(false);
            if (resultado)
            {
                return Ok("Asistencia eliminada con éxito.");
            }
            else
            {
                return NotFound("No se encontró la Asistencia.");
            }
        }
        [HttpGet("Obtener")]
        public async Task<ActionResult<List<AsistenciaDTOConId>>> Obtener()
        {
            Log.Information("Iniciando el proceso de obtener tareas.");

            var Asistencia = await _Asistencia.Obtener().ConfigureAwait(false);

            if (Asistencia != null && Asistencia.Count > 0)
            {
                Log.Information("Se obtuvieron {Count} tareas correctamente.", Asistencia.Count);
                return Ok(Asistencia);
            }
            else
            {
                Log.Warning("No se encontraron Asistencia.");
                return NotFound("No se encontraron Asistencia.");
            }
        }


        [HttpGet("ObtenerIndividual/{id}")]
        public async Task<ActionResult<EmpresaDTOConId>> ObtenerIndividual(int id)
        {
            var Asistencia = await _empresa.ObtenerIndividual(id).ConfigureAwait(false);
            if (Asistencia != null)
            {
                return Ok(Asistencia);
            }
            else
            {
                return NotFound("No se encontró la Asistencia.");
            }
        }
    }
}