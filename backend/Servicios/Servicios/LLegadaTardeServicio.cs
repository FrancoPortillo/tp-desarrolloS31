using Core.DTO;
using Data.Contexto;
using Data;
using Mapster;
using Microsoft.EntityFrameworkCore;  //HAY QUE CAMBIAR EMPLEADO X LLEGADA TARDE
using Servicios.Validadores;
using CORE.DTO;


namespace Servicios.Servicios
{
    
    public interface ILlegadaTarde
    {
        Task<int> Agregar(LlegadaTardeDTO LlegadaTarde);
        Task<bool> Eliminar(int id);
        Task<int> Modificar(LlegadaTardeDTOConId LlegadaTarde);

        Task<LlegadaTardeDTOConId> ObtenerIndividual(int id);

        Task<List<LlegadaTardeDTOConId>> Obtener();
    }

    public class LlegadaTardeServicio : ILlegadaTarde
    {
        private readonly BdRrhhContext _db;


        public LlegadaTardeServicio(BdRrhhContext db)
        {
            _db = db;
        }

        public async Task<int> Agregar(LlegadaTardeDTO LlegadaTarde)
        {


            //fluentValidation
            var validador = new LlegadaTardeAgregarValidador();
            var validadorResultado = validador.Validate(LlegadaTarde);





            //mapster
            var nuevaLlegadaTarde = LlegadaTarde.Adapt<Data.Models.LlegadaTarde>();
            await _db.LlegadaTarde.AddAsync(nuevaLlegadaTarde).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return nuevaLlegadaTarde.Id;

        }

        public async Task<int> Modificar(LlegadaTardeDTOConId LlegadaTarde)
        {
            var validador = new LlegadaTardeModificarValidador();
            var validadorResultado = validador.Validate(LlegadaTarde);

            // Validar la LlegadaTarde
            if (!validadorResultado.IsValid)
            {
                return -1;
            }

            var EmpresaModelo = await _db.LlegadaTarde.FirstOrDefaultAsync(x => x.Id == LlegadaTarde.Id).ConfigureAwait(false);

            if (LlegadaTardeModelo == null)
            {
                return -1;
            }

            LlegadaTardeModelo.Nombre = LlegadaTarde.Nombre;
            LlegadaTardeModelo.apellido = LlegadaTarde.apellido;
            LlegadaTardeModelo.legajo = LlegadaTarde.legajo;
            LlegadaTardeModelo.email = LlegadaTarde.Email;
            LlegadaTardeModelo.edad = LlegadaTarde.Edad;
            LlegadaTarde.Modelo.Puesto = LlegadaTarde.Puesto;




            await _db.SaveChangesAsync().ConfigureAwait(false);

            return LlegadaTardeModelo.Id;
        }


        public async Task<bool> Eliminar(int id)
        {
            var LlegadaTardeModelo = _db.LlegadaTarde.FirstOrDefault(x => x.Id == id);

            if (EmpresaModelo != null)
            {
                _db.LlegadaTarde.Remove(LlegadaTardeModelo);
                await _db.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

            throw new Exception("No es posible encontrar esa LlegadaTarde");
        }

        public async Task<List<EmpresaDTOConId>> Obtener()
        {
            var LlegadaTarde = _db.LlegadaTarde.ToList();
            return LlegadaTarde.Adapt<List<EmpresaDTOConId>>();

        }

        public async Task<EmpresaDTOConId> ObtenerIndividual(int id)
        {
            var EmpresaModelo = _db.LlegadaTarde.FirstOrDefault(x => x.Id == id);

            if (EmpresaModelo != null)
            {
                return EmpresaModelo.Adapt<EmpresaDTOConId>();
            }

            throw new Exception("No es posible encontrar esa LlegadaTarde");
        }
    }
}