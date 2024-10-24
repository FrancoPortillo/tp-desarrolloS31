using Core.DTO;
using CORE.DTO;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleado _Empleado;

        public EmpleadoController(IEmpleado Empleado)
        {
            _Empleado = Empleado;
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult<string>> Agregar(EmpleadoDTO Empleado)
        {
            //Log.Information("Intentando agregar una nueva Empleado: {@Empleado}", Empleado);

            var validador = new EmpleadoAgregarValidador();
            var validadorResultado = validador.Validate(Empleado);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            var nuevoId = await _Empleado.Agregar(Empleado).ConfigureAwait(false);
            if (nuevoId > 0)
            {
                return Ok($"Empleado creada con éxito");
                //Log.Information("Empleado agregada con éxito, ID: {NuevoId}", nuevoId);
            }

            //Log.Warning("Error al crear la Empleado.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la Empleado.");
        }

        [HttpPut("Modificar")]
        public async Task<ActionResult<string>> Modificar(EmpleadoDTOConId Empleado)
        {
            var validador = new EmpleadoModificarValidador();
            var validadorResultado = validador.Validate(Empleado);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            var nuevoId = await _Empleado.Modificar(Empleado).ConfigureAwait(false);

            if (nuevoId > 0)
            {
                return Ok($"Empleado modificada con éxito, ID: {nuevoId}");
            }

            return NotFound("No se encontró la Empleado para modificar.");
        }




        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var resultado = await _Empleado.Eliminar(id).ConfigureAwait(false);
            if (resultado)
            {
                return Ok("Empleado eliminada con éxito.");
            }
            else
            {
                return NotFound("No se encontró la Empleado.");
            }
        }
        [HttpGet("Obtener")]
        public async Task<ActionResult<List<EmpleadoDTOConId>>> Obtener()
        {
            //Log.Information("Iniciando el proceso de obtener tareas.");

            var Empleado = await _Empleado.Obtener().ConfigureAwait(false);

            if (Empleado != null && Empleado.lenght > 0)
            {
                //Log.Information("Se obtuvieron {Count} tareas correctamente.", Empleado.Count);
                return Ok(Empleado);
            }
            else
            {
                //Log.Warning("No se encontraron Empleado.");
                return NotFound("No se encontraron Empleado.");
            }
        }


        [HttpGet("ObtenerIndividual/{id}")]
        public async Task<ActionResult<EmpresaDTOConId>> ObtenerIndividual(int id)
        {
            var Empleado = await _Empleado.ObtenerIndividual(id).ConfigureAwait(false);
            if (Empleado != null)
            {
                return Ok(Empleado);
            }
            else
            {
                return NotFound("No se encontró la Empleado.");
            }
        }
    }
}