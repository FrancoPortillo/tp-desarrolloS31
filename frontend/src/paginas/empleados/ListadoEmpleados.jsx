import { useState } from 'react';
import { eliminarEmpleado } from '../../Utils/Axios';
import './Empleados.css';
import { NavLink } from 'react-router-dom';
import Boton from '../../componentes/Boton/Boton';
import Swal from 'sweetalert2';

export const ListadoEmpleados = ({ empleados, onAgregarEmpleado, onEliminarEmpleado, onOrdenarEmpleados, currentUserId }) => {
  const [orden, setOrden] = useState({ campo: 'nombre', ascendente: true });

  const ordenarPor = (campo) => {
    const nuevaOrden = orden.campo === campo ? !orden.ascendente : true;
    setOrden({ campo, ascendente: nuevaOrden });

    const empleadosOrdenados = [...empleados].sort((a, b) => {
      if (a[campo] < b[campo]) return nuevaOrden ? -1 : 1;
      if (a[campo] > b[campo]) return nuevaOrden ? 1 : -1;
      return 0;
    });

    onOrdenarEmpleados(empleadosOrdenados);
  };

  const handleEliminar = async (id) => {
    if (id === currentUserId) {
      Swal.fire({
        icon: 'warning',
        title: 'Advertencia',
        text: 'No puedes eliminar tu propio usuario.',
        confirmButtonText: 'Entendido'
      });
      return;
    }
    try {
      await eliminarEmpleado(id);
      onEliminarEmpleado(id);
      Swal.fire({
        icon: 'success',
        title: 'Éxito',
        text: 'Empleado eliminado correctamente.',
        confirmButtonText: 'OK'
      });
    } catch (error) {
      console.error('Error al eliminar empleado:', error);
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: 'Error al eliminar el empleado.',
        confirmButtonText: 'OK'
      });
    }
  };

  return (
    <div className="empleados-container">
      <table className="empleados-table">
        <thead>
          <tr>
            <th onClick={() => ordenarPor('nombre')}>Nombre</th>
            <th onClick={() => ordenarPor('apellido')}>Apellido</th>
            <th onClick={() => ordenarPor('legajo')}>Legajo</th>
            <th>Puesto</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody className="fondo">
          {empleados.map((empleado) => (
            <tr key={empleado.id}>
              <td>{empleado.nombre}</td>
              <td>{empleado.apellido}</td>
              <td>{empleado.legajo}</td>
              <td>{empleado.puesto}</td>
              <td>
                <NavLink to={`/perfil/${empleado.id}`}>
                  <button className="boton-ver-perfil">Ver Perfil</button>
                </NavLink>
                <button className="boton-eliminar" onClick={() => handleEliminar(empleado.id)}>Eliminar</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <Boton texto="Agregar Empleado" onClick={onAgregarEmpleado} />
    </div>
  );
};

export default ListadoEmpleados;