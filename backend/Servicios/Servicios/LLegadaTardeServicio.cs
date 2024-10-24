using Core.DTO;
using Data.Contexto;
using Data;
using Mapster;
using Microsoft.EntityFrameworkCore;  //HAY QUE CAMBIAR EMPLEADO X LLEGADA TARDE
using Servicios.Validadores;
using CORE.DTO;


namespace Servicios.Servicios
{
    //Se comenta para evitar duplicidad
    //public interface IEmpleado
    //{
    //    Task<int> Agregar(EmpleadoDTO Empleado);
    //    Task<bool> Eliminar(int id);
    //    Task<int> Modificar(EmpleadoDTOConId Empleado);

    //    Task<EmpleadoDTOConId> ObtenerIndividual(int id);

    //    Task<List<EmpleadoDTOConId>> Obtener();
    //}

    //public class EmpleadoServicio : IEmpleado
    //{
    //    private readonly BdRrhhContext _db;


    //    public EmpleadoServicio(BdRrhhContext db)
    //    {
    //        _db = db;
    //    }

    //    public async Task<int> Agregar(EmpleadoDTO Empleado)
    //    {


    //        //fluentValidation
    //        var validador = new EmpleadoAgregarValidador();
    //        var validadorResultado = validador.Validate(Empleado);





    //        //mapster
    //        var nuevaEmpleado = Empleado.Adapt<Data.Models.Empleado>();
    //        await _db.Empleado.AddAsync(nuevaEmpleado).ConfigureAwait(false);
    //        await _db.SaveChangesAsync().ConfigureAwait(false);
    //        return nuevaEmpleado.Id;

    //    }

    //    public async Task<int> Modificar(EmpleadoDTOConId Empleado)
    //    {
    //        var validador = new EmpleadoModificarValidador();
    //        var validadorResultado = validador.Validate(Empleado);

    //        // Validar la Empleado
    //        if (!validadorResultado.IsValid)
    //        {
    //            return -1;
    //        }

    //        var EmpresaModelo = await _db.Empleado.FirstOrDefaultAsync(x => x.Id == Empleado.Id).ConfigureAwait(false);

    //        if (EmpleadoModelo == null)
    //        {
    //            return -1;
    //        }

    //        EmpleadoModelo.Nombre = Empleado.Nombre;
    //        EmpleadoModelo.apellido = Empleado.apellido;
    //        EmpleadoModelo.legajo = Empleado.legajo;
    //        EmpleadoModelo.email = Empleado.Email;
    //        EmpleadoModelo.edad = Empleado.Edad;
    //        Empleado.Modelo.Puesto = Empleado.Puesto;




    //        await _db.SaveChangesAsync().ConfigureAwait(false);

    //        return EmpleadoModelo.Id;
    //    }


    //    public async Task<bool> Eliminar(int id)
    //    {
    //        var EmpleadoModelo = _db.Empleado.FirstOrDefault(x => x.Id == id);

    //        if (EmpresaModelo != null)
    //        {
    //            _db.Empleado.Remove(EmpleadoModelo);
    //            await _db.SaveChangesAsync().ConfigureAwait(false);
    //            return true;
    //        }

    //        throw new Exception("No es posible encontrar esa Empleado");
    //    }

    //    public async Task<List<EmpresaDTOConId>> Obtener()
    //    {
    //        var Empleado = _db.Empleado.ToList();
    //        return Empleado.Adapt<List<EmpresaDTOConId>>();

    //    }

    //    public async Task<EmpresaDTOConId> ObtenerIndividual(int id)
    //    {
    //        var EmpresaModelo = _db.Empleado.FirstOrDefault(x => x.Id == id);

    //        if (EmpresaModelo != null)
    //        {
    //            return EmpresaModelo.Adapt<EmpresaDTOConId>();
    //        }

    //        throw new Exception("No es posible encontrar esa Empleado");
    //    }
    //}
}