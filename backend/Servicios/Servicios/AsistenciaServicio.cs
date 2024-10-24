using Core.DTO;
using Data.Contexto;
using Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Servicios.Validadores;
using Data.Models;


namespace Servicios.Servicios
{
    public interface IAsistencia
    {
        Task<int> Agregar(EmpresaDTO Asistencia);
        Task<bool> Eliminar(int id);
        Task<int> Modificar(EmpresaDTOConId Asistencia);

        Task<EmpresaDTOConId> ObtenerIndividual(int id);

        Task<List<EmpresaDTOConId>> Obtener();
    }

    public class AsistenciaServicio : IAsistencia
    {
        private readonly BdRrhhContext _db;


        public AsistenciaServicio(BdRrhhContext db)
        {
            _db = db;
        }

        public async Task<int> Agregar(EmpresaDTO Asistencia)
        {


            //fluentValidation
            var validador = new EmpresaAgregarValidador();
            var validadorResultado = validador.Validate(Asistencia);





            //mapster
            var nuevaAsistencia = Asistencia.Adapt<Data.Models.Asistencia>();
            await _db.Asistencia.AddAsync(nuevaAsistencia).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return nuevaAsistencia.Id;

        }

        public async Task<int> Modificar(EmpresaDTOConId Asistencia)
        {
            var validador = new EmpresaModificarValidador();
            var validadorResultado = validador.Validate(Asistencia);

            // Validar la Asistencia
            if (!validadorResultado.IsValid)
            {
                return -1;
            }

            var AsistenciaModelo = await _db.Asistencia.FirstOrDefaultAsync(x => x.Id == Asistencia.Id).ConfigureAwait(false);

            if (AsistenciaModelo == null)
            {
                return -1;
            }


            AsistenciaModelo.fecha = Asistencia.Fecha;
            AsistenciaModelo.Presente = Asistencia.Presente;
            
            

            await _db.SaveChangesAsync().ConfigureAwait(false);

            return AsistenciaModelo.Id;
        }


        public async Task<bool> Eliminar(int id)
        {
            var EmpresaModelo = _db.Asistencia.FirstOrDefault(x => x.Id == id);

            if (EmpresaModelo != null)
            {
                _db.Asistencia.Remove(EmpresaModelo);
                await _db.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

            throw new Exception("No es posible encontrar esa Asistencia");
        }

        public async Task<List<EmpresaDTOConId>> Obtener()
        {
            var Asistencia = _db.Asistencia.ToList();
            return Asistencia.Adapt<List<EmpresaDTOConId>>();

        }

        public async Task<EmpresaDTOConId> ObtenerIndividual(int id)
        {
            var EmpresaModelo = _db.Asistencia.FirstOrDefault(x => x.Id == id);

            if (EmpresaModelo != null)
            {
                return EmpresaModelo.Adapt<EmpresaDTOConId>();
            }

            throw new Exception("No es posible encontrar esa Asistencia");
        }
    }
}