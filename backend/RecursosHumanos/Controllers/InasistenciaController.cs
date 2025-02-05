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
    public class InasistenciaController : ControllerBase
    {
        private readonly IInasistencia _inasistencia;
        private readonly ILogger<InasistenciaController> _logger;

        public InasistenciaController(IInasistencia inasistencia, ILogger<InasistenciaController> logger)
        {
            _inasistencia = inasistencia;
            _logger = logger;
        }
        [HttpPost("Registrar")]
        //public async Task<ActionResult> RegistrarAsistencia(List<AsistenciaDTO> asistencias)
        //{
        //    try
        //    {
        //        await _asistencia.RegistrarAsistencia(asistencias).ConfigureAwait(false);
        //        return Ok("Asistencia registrada con éxito.");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error al registrar asistencia.");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error del back.");
        //    }
        //}
        [HttpGet("ObtenerInasistencias/{idEmpleado}")]
        public async Task<ActionResult<int>> ObtenerInasistencias(int idEmpleado)
        {
            DateTime startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            var inasistencias = await _inasistencia.ObtenerInasistencias(idEmpleado, startDate, endDate).ConfigureAwait(false);
            return Ok(inasistencias);
        }
        [HttpPost("Agregar")]
        public async Task<ActionResult<string>> Agregar(InasistenciaDTO inasistencia)
        {
            _logger.LogInformation("Intentando agregar una nueva Asistencia: {@inasistencia}", inasistencia);

            var validador = new InasistenciaAgregarValidador();
            var validadorResultado = validador.Validate(inasistencia);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            var nuevoId = await _inasistencia.Agregar(inasistencia).ConfigureAwait(false);
            if (nuevoId > 0)
            {
                _logger.LogInformation("Inasistencia agregada con éxito, ID: {NuevoId}", nuevoId);
                return Ok("Inasistencia creada con éxito");
            }

            _logger.LogWarning("Error al crear la Inasistencia.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la Inasistencia.");
        }

        [HttpPut("Modificar")]
        public async Task<ActionResult<string>> Modificar(InasistenciaDTOConId inasistencia)
        {
            var validador = new InasistenciaModificarValidador();
            var validadorResultado = validador.Validate(inasistencia);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            var nuevoId = await _inasistencia.Modificar(inasistencia).ConfigureAwait(false);

            if (nuevoId > 0)
            {
                return Ok($"Inasistencia modificada con éxito, ID: {nuevoId}");
            }

            return NotFound("No se encontró la Inasistencia para modificar.");
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var resultado = await _inasistencia.Eliminar(id).ConfigureAwait(false);
            if (resultado)
            {
                return Ok("Inasistencia eliminada con éxito.");
            }
            else
            {
                return NotFound("No se encontró la Inasistencia.");
            }
        }

        [HttpGet("Obtener")]
        public async Task<ActionResult<List<AsistenciaDTOConId>>> Obtener()
        {
            _logger.LogInformation("Iniciando el proceso de obtener asistencias.");

            var inasistencias = await _inasistencia.Obtener().ConfigureAwait(false);

            if (inasistencias != null && inasistencias.Count > 0)
            {
                _logger.LogInformation("Se obtuvieron {Count} inasistencias correctamente.", inasistencias.Count);
                return Ok(inasistencias);
            }
            else
            {
                _logger.LogWarning("No se encontraron inasistencias.");
                return NotFound("No se encontraron inasistencias.");
            }
        }

        [HttpGet("ObtenerIndividual/{id}")]
        public async Task<ActionResult<InasistenciaDTOConId>> ObtenerIndividual(int id)
        {
            var inasistencia = await _inasistencia.ObtenerIndividual(id).ConfigureAwait(false);
            if (inasistencia != null)
            {
                return Ok(inasistencia);
            }
            else
            {
                return NotFound("No se encontró la Inasistencia.");
            }
        }
    }
}