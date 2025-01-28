using CORE.DTO;
using Microsoft.AspNetCore.Mvc;
using Servicios.Validadores;
using Servicios.Servicios;

namespace RecursosHumanos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleado _empleado;

        public EmpleadoController(IEmpleado empleado)
        {
            _empleado = empleado;
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult<string>> Agregar(EmpleadoDTO empleado)
        {
            var validador = new EmpleadoAgregarValidador();
            var validadorResultado = validador.Validate(empleado);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            try
            {
                var nuevoId = await _empleado.Agregar(empleado).ConfigureAwait(false);
                if (nuevoId > 0)
                {
                    return Ok("Empleado creado con éxito");
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el empleado.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPut("Modificar")]
        public async Task<ActionResult<string>> Modificar(EmpleadoDTOConId empleado)
        {
            var validador = new EmpleadoModificarValidador();
            var validadorResultado = validador.Validate(empleado);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            try
            {
                var nuevoId = await _empleado.Modificar(empleado).ConfigureAwait(false);
                if (nuevoId > 0)
                {
                    return Ok($"Empleado modificado con éxito, ID: {nuevoId}");
                }
                return NotFound("No se encontró el empleado para modificar.");
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
                var resultado = await _empleado.Eliminar(id).ConfigureAwait(false);
                if (resultado)
                {
                    return Ok("Empleado eliminado con éxito.");
                }
                return NotFound("No se encontró el empleado.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("Obtener")]
        public async Task<ActionResult<List<EmpleadoDTOConId>>> Obtener()
        {
            try
            {
                var empleados = await _empleado.Obtener().ConfigureAwait(false);
                if (empleados != null && empleados.Count > 0)
                {
                    return Ok(empleados);
                }
                return NotFound("No se encontraron empleados.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("ObtenerIndividual/{id}")]
        public async Task<ActionResult<EmpleadoDTOConId>> ObtenerIndividual(int id)
        {
            try
            {
                var empleado = await _empleado.ObtenerIndividual(id).ConfigureAwait(false);
                if (empleado != null)
                {
                    return Ok(empleado);
                }
                return NotFound("No se encontró el empleado.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
        [HttpGet("ObtenerPorEmail/{email}")]
        public async Task<ActionResult<EmpleadoDTOConId>> ObtenerPorEmail(string email)
        {
            try
            {
                var empleado = await _empleado.ObtenerPorEmail(email).ConfigureAwait(false);
                if (empleado != null)
                {
                    return Ok(empleado);
                }
                return NotFound("No se encontró el empleado.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}