using Core.DTO;
using Data.Contexto;
using Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Servicios.Validadores;
using CORE.DTO;


namespace Servicios.Servicios
{
    public interface IVacaciones
    {
        Task<int> Agregar(VacacionesDTO Vacaciones);
        Task<bool> Eliminar(int id);
        Task<int> Modificar(VacacionesDTOConId Vacaciones);

        Task<VacacionesDTOConId> ObtenerIndividual(int id);

        Task<List<VacacionesDTOConId>> Obtener();
    }

    public class VacacionesServicio : IVacaciones
    {
        private readonly BdRrhhContext _db;


        public VacacionesServicio(BdRrhhContext db)
        {
            _db = db;
        }

        public async Task<int> Agregar(VacacionesDTO Vacaciones)
        {


            //fluentValidation
            var validador = new VacacionesAgregarValidador();
            var validadorResultado = validador.Validate(Vacaciones);





            //mapster
            var nuevaVacaciones = Vacaciones.Adapt<Data.Models.Vacaciones>();
            await _db.Vacaciones.AddAsync(nuevaVacaciones).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return nuevaVacaciones.Id;

        }

        public async Task<int> Modificar(VacacionesDTOConId Vacaciones)
        {
            var validador = new VacacionesmodificarValidador();
            var validadorResultado = validador.Validate(Vacaciones);

            // Validar la Vacaciones
            if (!validadorResultado.IsValid)
            {
                return -1;
            }

            var VacacionesModelo = await _db.Vacaciones.FirstOrDefaultAsync(x => x.Id == Vacaciones.Id).ConfigureAwait(false);

            if (VacacionesModelo == null)
            {
                return -1;
            }

            VacacionesModelo.FechaInicio = Vacaciones.FechaInicio;
            VacacionesModelo.FechaFin = Vacaciones.FechaFin;
            VacacionesModelo.Aprobado = Vacaciones.Aprobado;
            
            




            await _db.SaveChangesAsync().ConfigureAwait(false);

            return VacacionesModelo.Id;
        }


        public async Task<bool> Eliminar(int id)
        {
            var VacacionesModelo = _db.Vacaciones.FirstOrDefault(x => x.Id == id);

            if (VacacionesModelo != null)
            {
                _db.Vacaciones.Remove(VacacionesModelo);
                await _db.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

            throw new Exception("No es posible encontrar esa Vacaciones");
        }

        public async Task<List<VacacionesDTOConId>> Obtener()
        {
            var Vacaciones = _db.Vacaciones.ToList();
            return Vacaciones.Adapt<List<VacacionesDTOConId>>();

        }

        public async Task<VacacionesDTOConId> ObtenerIndividual(int id)
        {
            var VacacionesModelo = _db.Vacaciones.FirstOrDefault(x => x.Id == id);

            if (VacacionesModelo != null)
            {
                return VacacionesModelo.Adapt<EmpresaDTOConId>();
            }

            throw new Exception("No es posible encontrar esa Vacaciones");
        }
    }
}