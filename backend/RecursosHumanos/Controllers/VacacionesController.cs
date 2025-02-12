using Mapster;
using CORE.DTO;
using Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Servicios.Validadores;

namespace RecursosHumanos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacacionesController : ControllerBase
    {
        private readonly IVacaciones _vacaciones;
        private readonly ILogger<VacacionesController> _logger;

        public VacacionesController(IVacaciones vacaciones, ILogger<VacacionesController> logger)
        {
            _vacaciones = vacaciones;
            _logger = logger;
        }
        [HttpGet("calendario")]
        public async Task<IActionResult> ObtenerCalendarioVacaciones()
        {
            var calendario = await _vacaciones.ObtenerCalendarioVacaciones();
            return Ok(calendario);
        }
        
        [HttpPost("Agregar")]
        public async Task<ActionResult<string>> Agregar(VacacionesDTO vacaciones)
        {
            _logger.LogInformation("Intentando agregar una nueva Vacaciones: {@Vacaciones}", vacaciones);

            var validador = new VacacionesAgregarValidador();
            var validadorResultado = validador.Validate(vacaciones);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            try
            {
                var nuevoId = await _vacaciones.Agregar(vacaciones).ConfigureAwait(false);
                if (nuevoId > 0)
                {
                    _logger.LogInformation("Vacaciones agregada con éxito, ID: {NuevoId}", nuevoId);
                    return Ok("Vacaciones creada con éxito");
                }

                _logger.LogWarning("Error al crear la Vacaciones.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la Vacaciones.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar la Vacaciones.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPatch("Modificar/{estado}/{id}")]
        public async Task<ActionResult> ModificarEstado(int id, string estado)
        {

            try
            {
                var vacacion = await _vacaciones.ModificarEstado(id, estado);
                if (vacacion != null)
                {
                    return Ok(vacacion);
                }
                return NotFound("No se encontró la vacacion para modificar.");
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
            try
            {
                var resultado = await _vacaciones.Eliminar(id).ConfigureAwait(false);
                if (resultado)
                {
                    return Ok("Vacaciones eliminada con éxito.");
                }
                return NotFound("No se encontró la Vacaciones.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la Vacaciones.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("Obtener")]
        public async Task<ActionResult<List<VacacionesDTOConId>>> Obtener()
        {
            _logger.LogInformation("Iniciando el proceso de obtener vacaciones.");

            try
            {
                var vacaciones = await _vacaciones.Obtener().ConfigureAwait(false);
                if (vacaciones != null && vacaciones.Count > 0)
                {
                    _logger.LogInformation("Se obtuvieron {Count} vacaciones correctamente.", vacaciones.Count);
                    return Ok(vacaciones);
                }
                _logger.LogWarning("No se encontraron vacaciones.");
                return NotFound("No se encontraron vacaciones.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las vacaciones.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("ObtenerIndividual/{id}")]
        public async Task<ActionResult<VacacionesDTOConId>> ObtenerIndividual(int id)
        {
            try
            {
                var vacaciones = await _vacaciones.ObtenerIndividual(id).ConfigureAwait(false);
                if (vacaciones != null)
                {
                    return Ok(vacaciones);
                }
                return NotFound("No se encontró la Vacaciones.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la Vacaciones.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
        [HttpGet("ObtenerPorEmpleado/{id}")]
        public async Task<ActionResult<List<VacacionesDTOConId>>> ObtenerPorEmpleado(int id)
        {
            try
            {
                var vacaciones = await _vacaciones.ObtenerPorEmpleado(id);
                return Ok(vacaciones);
            }
            catch (Exception ex)
            {
                // Manejar la excepción y devolver una respuesta adecuada
                return StatusCode(500, new { message = "Error al obtener las vacaciones", details = ex.Message });
            }

        }
    }
}