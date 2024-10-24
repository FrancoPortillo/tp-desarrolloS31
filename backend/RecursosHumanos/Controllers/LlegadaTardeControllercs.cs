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
        private readonly ILLegadaTarde _LLegadaTarde;

        public LLegadaTardeController(ILLegadaTarde LLegadaTarde)
        {
            _LLegadaTarde = LLegadaTarde;
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult<string>> Agregar(LLegadaTardeDTO LLegadaTarde)
        {
            Log.Information("Intentando agregar una nueva LLegadaTarde: {@LLegadaTarde}", LLegadaTarde);

            var validador = new LLegadaTardeAgregarValidador();
            var validadorResultado = validador.Validate(LLegadaTarde);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            var nuevoId = await _LLegadaTarde.Agregar(LLegadaTarde).ConfigureAwait(false);
            if (nuevoId > 0)
            {
                return Ok($"LLegadaTarde creada con éxito");
                Log.Information("LLegadaTarde agregada con éxito, ID: {NuevoId}", nuevoId);
            }

            Log.Warning("Error al crear la LLegadaTarde.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la LLegadaTarde.");
        }

        [HttpPut("Modificar")]
        public async Task<ActionResult<string>> Modificar(LLegadaTardeDTOConId LLegadaTarde)
        {
            var validador = new LLegadaTardeModificarValidador();
            var validadorResultado = validador.Validate(LLegadaTarde);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            var nuevoId = await _LLegadaTarde.Modificar(LLegadaTarde).ConfigureAwait(false);

            if (nuevoId > 0)
            {
                return Ok($"LLegadaTarde modificada con éxito, ID: {nuevoId}");
            }

            return NotFound("No se encontró la LLegadaTarde para modificar.");
        }




        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var resultado = await _LLegadaTarde.Eliminar(id).ConfigureAwait(false);
            if (resultado)
            {
                return Ok("LLegadaTarde eliminada con éxito.");
            }
            else
            {
                return NotFound("No se encontró la LLegadaTarde.");
            }
        }
        [HttpGet("Obtener")]
        public async Task<ActionResult<List<LLegadaTardeDTOConId>>> Obtener()
        {
            Log.Information("Iniciando el proceso de obtener tareas.");

            var LLegadaTarde = await _LLegadaTarde.Obtener().ConfigureAwait(false);

            if (LLegadaTarde != null && LLegadaTarde.Count > 0)
            {
                Log.Information("Se obtuvieron {Count} tareas correctamente.", LLegadaTarde.Count);
                return Ok(LLegadaTarde);
            }
            else
            {
                Log.Warning("No se encontraron LLegadaTarde.");
                return NotFound("No se encontraron LLegadaTarde.");
            }
        }


        [HttpGet("ObtenerIndividual/{id}")]
        public async Task<ActionResult<EmpresaDTOConId>> ObtenerIndividual(int id)
        {
            var LLegadaTarde = await _LLegadaTarde.ObtenerIndividual(id).ConfigureAwait(false);
            if (LLegadaTarde != null)
            {
                return Ok(LLegadaTarde);
            }
            else
            {
                return NotFound("No se encontró la LLegadaTarde.");
            }
        }
    }
}