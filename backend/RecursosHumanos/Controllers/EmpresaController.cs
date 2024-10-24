using Mapster;
using Core;
using Core.DTO;
using Servicios;
using Microsoft.AspNetCore.Mvc;
using Servicios.Validadores;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresa _empresa;

        public EmpresaController(IEmpresa Empresa)
        {
            _empresa = Empresa;
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult<string>> Agregar(EmpresaDTO Empresa)
        {
            Log.Information("Intentando agregar una nueva Empresa: {@Empresa}", Empresa);

            var validador = new EmpresaAgregarValidador();
            var validadorResultado = validador.Validate(Empresa);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            var nuevoId = await _empresa.Agregar(Empresa).ConfigureAwait(false);
            if (nuevoId > 0)
            {
                return Ok($"Empresa creada con éxito");
                Log.Information("Empresa agregada con éxito, ID: {NuevoId}", nuevoId);
            }

            Log.Warning("Error al crear la Empresa.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la Empresa.");
        }

        [HttpPut("Modificar")]
        public async Task<ActionResult<string>> Modificar(DTOConId Empresa)
        {
            var validador = new EmpresaModificarValidador();
            var validadorResultado = validador.Validate(Empresa);

            if (!validadorResultado.IsValid)
            {
                return BadRequest(validadorResultado.Errors);
            }

            var nuevoId = await _empresa.Modificar(Empresa).ConfigureAwait(false);

            if (nuevoId > 0)
            {
                return Ok($"Empresa modificada con éxito, ID: {nuevoId}");
            }

            return NotFound("No se encontró la Empresa para modificar.");
        }




        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var resultado = await _empresa.Eliminar(id).ConfigureAwait(false);
            if (resultado)
            {
                return Ok("Empresa eliminada con éxito.");
            }
            else
            {
                return NotFound("No se encontró la Empresa.");
            }
        }
        [HttpGet("Obtener")]
        public async Task<ActionResult<List<EmpresaDTOConId>>> Obtener()
        {
            Log.Information("Iniciando el proceso de obtener tareas.");

            var empresa = await _empresa.Obtener().ConfigureAwait(false);

            if (empresa != null && empresa.Count > 0)
            {
                Log.Information("Se obtuvieron {Count} tareas correctamente.", empresa.Count);
                return Ok(empresa);
            }
            else
            {
                Log.Warning("No se encontraron empresas.");
                return NotFound("No se encontraron empresas.");
            }
        }


        [HttpGet("ObtenerIndividual/{id}")]
        public async Task<ActionResult<EmpresaDTOConId>> ObtenerIndividual(int id)
        {
            var Empresa = await _empresa.ObtenerIndividual(id).ConfigureAwait(false);
            if (Empresa != null)
            {
                return Ok(Empresa);
            }
            else
            {
                return NotFound("No se encontró la Empresa.");
            }
        }
    }
}