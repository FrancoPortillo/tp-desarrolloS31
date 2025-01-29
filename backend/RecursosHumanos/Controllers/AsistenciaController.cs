using Servicios.Servicios;
using Servicios.Validadores;
using Microsoft.AspNetCore.Http;
using CORE.DTO;
using Microsoft.AspNetCore.Mvc;
using CORE.DTO.Core.DTO;

namespace RecursosHumanos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AsistenciaController : ControllerBase
    {
        private readonly IAsistencia _asistencia;
        private readonly ILogger<AsistenciaController> _logger;

        public AsistenciaController(IAsistencia asistencia, ILogger<AsistenciaController> logger)
        {
            _asistencia = asistencia;
            _logger = logger;
        }
        [HttpPost("Registrar")]
        public async Task<ActionResult> RegistrarAsistencia(List<AsistenciaDTO> asistencias)
        {
            try
            {
                await _asistencia.RegistrarAsistencia(asistencias).ConfigureAwait(false);
                return Ok("Asistencia registrada con éxito.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar asistencia.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error del back.");
            }
        }
        [HttpGet("ObtenerInasistencias/{idEmpleado}")]
        public async Task<ActionResult<int>> ObtenerInasistencias(int idEmpleado)
        {
            var inasistencias = await _asistencia.ObtenerInasistencias(idEmpleado).ConfigureAwait(false);
            return Ok(inasistencias);
        }
        [HttpPost("Agregar")]
        public async Task<ActionResult<string>> Agregar(AsistenciaDTO asistencia)
        {
            _logger.LogInformation("Intentando agregar una nueva Asistencia: {@Asistencia}", asistencia);

            var validador = new AsistenciaAgregarValidador();
            var validadorResultado = validador.Validate(asistencia);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            var nuevoId = await _asistencia.Agregar(asistencia).ConfigureAwait(false);
            if (nuevoId > 0)
            {
                _logger.LogInformation("Asistencia agregada con éxito, ID: {NuevoId}", nuevoId);
                return Ok("Asistencia creada con éxito");
            }

            _logger.LogWarning("Error al crear la Asistencia.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la Asistencia.");
        }

        [HttpPut("Modificar")]
        public async Task<ActionResult<string>> Modificar(AsistenciaDTOConId asistencia)
        {
            var validador = new AsistenciaModificarValidador();
            var validadorResultado = validador.Validate(asistencia);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            var nuevoId = await _asistencia.Modificar(asistencia).ConfigureAwait(false);

            if (nuevoId > 0)
            {
                return Ok($"Asistencia modificada con éxito, ID: {nuevoId}");
            }

            return NotFound("No se encontró la Asistencia para modificar.");
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var resultado = await _asistencia.Eliminar(id).ConfigureAwait(false);
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
            _logger.LogInformation("Iniciando el proceso de obtener asistencias.");

            var asistencias = await _asistencia.Obtener().ConfigureAwait(false);

            if (asistencias != null && asistencias.Count > 0)
            {
                _logger.LogInformation("Se obtuvieron {Count} asistencias correctamente.", asistencias.Count);
                return Ok(asistencias);
            }
            else
            {
                _logger.LogWarning("No se encontraron asistencias.");
                return NotFound("No se encontraron asistencias.");
            }
        }

        [HttpGet("ObtenerIndividual/{id}")]
        public async Task<ActionResult<AsistenciaDTOConId>> ObtenerIndividual(int id)
        {
            var asistencia = await _asistencia.ObtenerIndividual(id).ConfigureAwait(false);
            if (asistencia != null)
            {
                return Ok(asistencia);
            }
            else
            {
                return NotFound("No se encontró la Asistencia.");
            }
        }
    }
}