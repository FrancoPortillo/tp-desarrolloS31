using Core.DTO;
using Data.Contexto;
using Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Servicios.Validadores;


namespace Servicios.Servicios
{
    public interface IEmpresa
    {
        Task<int> Agregar(EmpresaDTO Empresa);
        Task<bool> Eliminar(int id);
        Task<int> Modificar(EmpresaDTOConId Empresa);

        Task<EmpresaDTOConId> ObtenerIndividual(int id);

        Task<List<EmpresaDTOConId>> Obtener();
    }

    public class EmpresaServicio : IEmpresa
    {
        private readonly BdRrhhContext _db;


        public EmpresaServicio(BdRrhhContext db)
        {
            _db = db;
        }

        public async Task<int> Agregar(EmpresaDTO Empresa)
        {


            //fluentValidation
            var validador = new EmpresaAgregarValidador();
            var validadorResultado = validador.Validate(Empresa);
           


        

        //mapster
        var nuevaEmpresa = Empresa.Adapt<Data.Models.Empresa>();
        await _db.Empresa.AddAsync(nuevaEmpresa).ConfigureAwait(false);
        await _db.SaveChangesAsync().ConfigureAwait(false);
            return nuevaEmpresa.Id;
        
    }

        public async Task<int> Modificar(EmpresaDTOConId Empresa)
        {
            var validador = new EmpresaModificarValidador();
            var validadorResultado = validador.Validate(Empresa);

            // Validar la Empresa
            if (!validadorResultado.IsValid)
            {
                return -1;
            }

            var EmpresaModelo = await _db.Empresa.FirstOrDefaultAsync(x => x.Id == Empresa.Id).ConfigureAwait(false);

            if (EmpresaModelo == null)
            {
                return -1;
            }

            EmpresaModelo.Nombre = Empresa.Nombre;
            EmpresaModelo.direccion = Empresa.Direccion;
            EmpresaModelo.Telefono = Empresa.Telefono;
            EmpresaModelo.Email=Empresa.Email;


            await _db.SaveChangesAsync().ConfigureAwait(false);

            return EmpresaModelo.Id;
        }


        public async Task<bool> Eliminar(int id)
        {
            var EmpresaModelo = _db.Empresa.FirstOrDefault(x => x.Id == id);

            if (EmpresaModelo != null)
            {
                _db.Empresa.Remove(EmpresaModelo);
                await _db.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

            throw new Exception("No es posible encontrar esa Empresa");
        }

        public async Task<List<EmpresaDTOConId>> Obtener()
        {
            var Empresa = _db.Empresa.ToList();
            return Empresa.Adapt<List<EmpresaDTOConId>>();

        }

        public async Task<EmpresaDTOConId> ObtenerIndividual(int id)
        {
            var EmpresaModelo = _db.Empresa.FirstOrDefault(x => x.Id == id);

            if (EmpresaModelo != null)
            {
                return EmpresaModelo.Adapt<EmpresaDTOConId>();
            }

            throw new Exception("No es posible encontrar esa Empresa");
        }
    }
} 