using Core.DTO;
using Data.Contexto;
using Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Servicios.Validadores;
using FluentValidation;

namespace Servicios.Servicios
{
    public interface IEmpresa
    {
        Task<int> Agregar(EmpresaDTO empresa);
        Task<bool> Eliminar(int id);
        Task<int> Modificar(EmpresaDTOConId empresa);
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

        public async Task<int> Agregar(EmpresaDTO empresa)
        {
            // FluentValidation
            var validador = new EmpresaAgregarValidador();
            var validadorResultado = validador.Validate(empresa);

            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            // Mapster
            var nuevaEmpresa = empresa.Adapt<Data.Models.Empresa>();
            await _db.Empresa.AddAsync(nuevaEmpresa).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return nuevaEmpresa.Id;
        }

        public async Task<int> Modificar(EmpresaDTOConId empresa)
        {
            var validador = new EmpresaModificarValidador();
            var validadorResultado = validador.Validate(empresa);

            // Validar la Empresa
            if (!validadorResultado.IsValid)
            {
                throw new ValidationException(validadorResultado.Errors);
            }

            var empresaModelo = await _db.Empresa.FirstOrDefaultAsync(x => x.Id == empresa.Id).ConfigureAwait(false);

            if (empresaModelo == null)
            {
                throw new KeyNotFoundException("Empresa no encontrada");
            }

            empresaModelo.Nombre = empresa.Nombre;
            empresaModelo.Direccion = empresa.Direccion;
            empresaModelo.Telefono = empresa.Telefono;
            empresaModelo.Email = empresa.Email;

            await _db.SaveChangesAsync().ConfigureAwait(false);

            return empresaModelo.Id;
        }

        public async Task<bool> Eliminar(int id)
        {
            var empresaModelo = await _db.Empresa.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            if (empresaModelo != null)
            {
                _db.Empresa.Remove(empresaModelo);
                await _db.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

            throw new KeyNotFoundException("Empresa no encontrada");
        }

        public async Task<List<EmpresaDTOConId>> Obtener()
        {
            var empresas = await _db.Empresa.ToListAsync().ConfigureAwait(false);
            return empresas.Adapt<List<EmpresaDTOConId>>();
        }

        public async Task<EmpresaDTOConId> ObtenerIndividual(int id)
        {
            var empresaModelo = await _db.Empresa.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            if (empresaModelo != null)
            {
                return empresaModelo.Adapt<EmpresaDTOConId>();
            }

            throw new KeyNotFoundException("Empresa no encontrada");
        }
    }
}