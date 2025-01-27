using Mapster;
using Servicios.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Servicios.Validadores;
using CORE.DTO;

namespace RecursosHumanos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LlegadaTardeController : ControllerBase
    {
        private readonly ILlegadaTarde _llegadaTarde;
        private readonly ILogger<LlegadaTardeController> _logger;

        public LlegadaTardeController(ILlegadaTarde llegadaTarde, ILogger<LlegadaTardeController> logger)
        {
            _llegadaTarde = llegadaTarde;
            _logger = logger;
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult<string>> Agregar(LLegadaTardeDTO llegadaTarde)
        {
            _logger.LogInformation("Intentando agregar una nueva LlegadaTarde: {@LlegadaTarde}", llegadaTarde);

            var validador = new LLegadaTardeAgregarValidador();
            var validadorResultado = validador.Validate(llegadaTarde);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            try
            {
                var nuevoId = await _llegadaTarde.Agregar(llegadaTarde).ConfigureAwait(false);
                if (nuevoId > 0)
                {
                    _logger.LogInformation("LlegadaTarde agregada con éxito, ID: {NuevoId}", nuevoId);
                    return Ok("LlegadaTarde creada con éxito");
                }

                _logger.LogWarning("Error al crear la LlegadaTarde.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la LlegadaTarde.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar la LlegadaTarde.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPut("Modificar")]
        public async Task<ActionResult<string>> Modificar(LLegadaTardeDTOConId llegadaTarde)
        {
            var validador = new LLegadaTardeModificarValidador();
            var validadorResultado = validador.Validate(llegadaTarde);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            try
            {
                var nuevoId = await _llegadaTarde.Modificar(llegadaTarde).ConfigureAwait(false);
                if (nuevoId > 0)
                {
                    return Ok($"LlegadaTarde modificada con éxito, ID: {nuevoId}");
                }

                return NotFound("No se encontró la LlegadaTarde para modificar.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar la LlegadaTarde.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            try
            {
                var resultado = await _llegadaTarde.Eliminar(id).ConfigureAwait(false);
                if (resultado)
                {
                    return Ok("LlegadaTarde eliminada con éxito.");
                }
                return NotFound("No se encontró la LlegadaTarde.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la LlegadaTarde.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("Obtener")]
        public async Task<ActionResult<List<LLegadaTardeDTOConId>>> Obtener()
        {
            _logger.LogInformation("Iniciando el proceso de obtener llegadas tarde.");

            try
            {
                var llegadasTarde = await _llegadaTarde.Obtener().ConfigureAwait(false);
                if (llegadasTarde != null && llegadasTarde.Count > 0)
                {
                    _logger.LogInformation("Se obtuvieron {Count} llegadas tarde correctamente.", llegadasTarde.Count);
                    return Ok(llegadasTarde);
                }
                _logger.LogWarning("No se encontraron llegadas tarde.");
                return NotFound("No se encontraron llegadas tarde.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las llegadas tarde.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("ObtenerIndividual/{id}")]
        public async Task<ActionResult<LLegadaTardeDTOConId>> ObtenerIndividual(int id)
        {
            try
            {
                var llegadaTarde = await _llegadaTarde.ObtenerIndividual(id).ConfigureAwait(false);
                if (llegadaTarde != null)
                {
                    return Ok(llegadaTarde);
                }
                return NotFound("No se encontró la LlegadaTarde.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la LlegadaTarde.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}