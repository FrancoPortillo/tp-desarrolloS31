using Mapster;
using Core.DTO;
using Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Servicios.Validadores;
using Servicios.Servicios;

namespace RecursosHumanos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresa _empresa;
        private readonly ILogger<EmpresaController> _logger;

        public EmpresaController(IEmpresa empresa, ILogger<EmpresaController> logger)
        {
            _empresa = empresa;
            _logger = logger;
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult<string>> Agregar(EmpresaDTO empresa)
        {
            _logger.LogInformation("Intentando agregar una nueva Empresa: {@Empresa}", empresa);

            var validador = new EmpresaAgregarValidador();
            var validadorResultado = validador.Validate(empresa);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            try
            {
                var nuevoId = await _empresa.Agregar(empresa).ConfigureAwait(false);
                if (nuevoId > 0)
                {
                    _logger.LogInformation("Empresa agregada con éxito, ID: {NuevoId}", nuevoId);
                    return Ok("Empresa creada con éxito");
                }

                _logger.LogWarning("Error al crear la Empresa.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la Empresa.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar la Empresa.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPut("Modificar")]
        public async Task<ActionResult<string>> Modificar(EmpresaDTOConId empresa)
        {
            var validador = new EmpresaModificarValidador();
            var validadorResultado = validador.Validate(empresa);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            try
            {
                var nuevoId = await _empresa.Modificar(empresa).ConfigureAwait(false);
                if (nuevoId > 0)
                {
                    return Ok($"Empresa modificada con éxito, ID: {nuevoId}");
                }

                return NotFound("No se encontró la Empresa para modificar.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar la Empresa.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            try
            {
                var resultado = await _empresa.Eliminar(id).ConfigureAwait(false);
                if (resultado)
                {
                    return Ok("Empresa eliminada con éxito.");
                }
                return NotFound("No se encontró la Empresa.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la Empresa.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("Obtener")]
        public async Task<ActionResult<List<EmpresaDTOConId>>> Obtener()
        {
            _logger.LogInformation("Iniciando el proceso de obtener empresas.");

            try
            {
                var empresas = await _empresa.Obtener().ConfigureAwait(false);
                if (empresas != null && empresas.Count > 0)
                {
                    _logger.LogInformation("Se obtuvieron {Count} empresas correctamente.", empresas.Count);
                    return Ok(empresas);
                }
                _logger.LogWarning("No se encontraron empresas.");
                return NotFound("No se encontraron empresas.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las empresas.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("ObtenerIndividual/{id}")]
        public async Task<ActionResult<EmpresaDTOConId>> ObtenerIndividual(int id)
        {
            try
            {
                var empresa = await _empresa.ObtenerIndividual(id).ConfigureAwait(false);
                if (empresa != null)
                {
                    return Ok(empresa);
                }
                return NotFound("No se encontró la Empresa.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la Empresa.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}