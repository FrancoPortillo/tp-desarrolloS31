import { useEffect, useState } from 'react';
import { obtenerEmpleados, eliminarEmpleado } from '../../Utils/Axios';
import './Empleados.css';
import { NavLink } from 'react-router-dom';

export const ListadoEmpleados = ({ onAgregarEmpleado }) => {
  const [empleados, setEmpleados] = useState([]);
  const [orden, setOrden] = useState({ campo: 'nombre', ascendente: true });

  useEffect(() => {
    const fetchEmpleados = async () => {
      try {
        const data = await obtenerEmpleados();
        setEmpleados(data);
      } catch (error) {
        console.error('Error al obtener empleados:', error);
      }
    };

    fetchEmpleados();
  }, []);

  const ordenarPor = (campo) => {
    const nuevaOrden = orden.campo === campo ? !orden.ascendente : true;
    setOrden({ campo, ascendente: nuevaOrden });

    const empleadosOrdenados = [...empleados].sort((a, b) => {
      if (a[campo] < b[campo]) return nuevaOrden ? -1 : 1;
      if (a[campo] > b[campo]) return nuevaOrden ? 1 : -1;
      return 0;
    });

    setEmpleados(empleadosOrdenados);
  };

  const handleEliminar = async (id) => {
    try {
      await eliminarEmpleado(id);
      setEmpleados(empleados.filter(empleado => empleado.id !== id));
    } catch (error) {
      console.error('Error al eliminar empleado:', error);
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

      <button className="boton-agregar" onClick={onAgregarEmpleado}>Agregar Empleado</button>
    </div>
  );
};

export default ListadoEmpleados;