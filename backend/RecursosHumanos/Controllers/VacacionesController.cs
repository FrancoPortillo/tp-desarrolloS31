using Mapster;
using Core;
using Core.DTO;
using Servicios;
using Microsoft.AspNetCore.Mvc;
using Servicios.Validadores;
using CORE.DTO.Core.DTO;
using CORE.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private readonly IVacaciones _Vacaciones;

        public VacacionesController(IVacaciones Vacaciones)
        {
            _Vacaciones = Vacaciones;
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult<string>> Agregar(VacacionesDTO Vacaciones)
        {
            Log.Information("Intentando agregar una nueva Vacaciones: {@Vacaciones}", Vacaciones);

            var validador = new VacacionesAgregarValidador();
            var validadorResultado = validador.Validate(Vacaciones);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            var nuevoId = await _Vacaciones.Agregar(Vacaciones).ConfigureAwait(false);
            if (nuevoId > 0)
            {
                return Ok($"Vacaciones creada con éxito");
                Log.Information("Vacaciones agregada con éxito, ID: {NuevoId}", nuevoId);
            }

            Log.Warning("Error al crear la Vacaciones.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la Vacaciones.");
        }

        [HttpPut("Modificar")]
        public async Task<ActionResult<string>> Modificar(DTOConId Vacaciones)
        {
            var validador = new VacacionesModificarValidador();
            var validadorResultado = validador.Validate(Vacaciones);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            var nuevoId = await _Vacaciones.Modificar(Vacaciones).ConfigureAwait(false);

            if (nuevoId > 0)
            {
                return Ok($"Vacaciones modificada con éxito, ID: {nuevoId}");
            }

            return NotFound("No se encontró la Vacaciones para modificar.");
        }




        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var resultado = await _Vacaciones.Eliminar(id).ConfigureAwait(false);
            if (resultado)
            {
                return Ok("Vacaciones eliminada con éxito.");
            }
            else
            {
                return NotFound("No se encontró la Vacaciones.");
            }
        }
        [HttpGet("Obtener")]
        public async Task<ActionResult<List<VacacionesDTOConId>>> Obtener()
        {
            Log.Information("Iniciando el proceso de obtener tareas.");

            var Vacaciones = await _Vacaciones.Obtener().ConfigureAwait(false);

            if (Vacaciones != null && Vacaciones.Count > 0)
            {
                Log.Information("Se obtuvieron {Count} tareas correctamente.", Vacaciones.Count);
                return Ok(Vacaciones);
            }
            else
            {
                Log.Warning("No se encontraron Vacaciones.");
                return NotFound("No se encontraron Vacaciones.");
            }
        }


        [HttpGet("ObtenerIndividual/{id}")]
        public async Task<ActionResult<EmpresaDTOConId>> ObtenerIndividual(int id)
        {
            var Vacaciones = await _Vacaciones.ObtenerIndividual(id).ConfigureAwait(false);
            if (Vacaciones != null)
            {
                return Ok(Vacaciones);
            }
            else
            {
                return NotFound("No se encontró la Vacaciones.");
            }
        }
    }
}