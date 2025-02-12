import { useState, useEffect } from 'react';
import { eliminarEmpleado, obtenerEmpleadosEliminados } from '../../Utils/Axios';
import './Empleados.css';
import { NavLink } from 'react-router-dom';
import Boton from '../../componentes/Boton/Boton';
import { Snackbar, Alert } from '@mui/material';

export const ListadoEmpleados = ({ empleados, onAgregarEmpleado, onEliminarEmpleado, onOrdenarEmpleados, currentUserId }) => {
  const [orden, setOrden] = useState({ campo: 'nombre', ascendente: true });
  const [mostrarEliminados, setMostrarEliminados] = useState(false);
  const [empleadosEliminados, setEmpleadosEliminados] = useState([]);
  const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'success' });

  useEffect(() => {
    const fetchEmpleadosEliminados = async () => {
      try {
        const data = await obtenerEmpleadosEliminados();
        setEmpleadosEliminados(data);
      } catch (error) {
        console.error('Error al obtener empleados eliminados:', error);
      }
    };

    fetchEmpleadosEliminados();
  }, []);

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
      setSnackbar({ open: true, message: 'No puedes eliminar tu propio usuario.', severity: 'warning' });
      return;
    }
    try {
      await eliminarEmpleado(id);
      onEliminarEmpleado(id);
      setSnackbar({ open: true, message: 'Empleado eliminado correctamente.', severity: 'success' });
    } catch (error) {
      console.error('Error al eliminar empleado:', error);
      setSnackbar({ open: true, message: 'Error al eliminar el empleado.', severity: 'error' });
    }
  };

  const handleCloseSnackbar = () => {
    setSnackbar({ ...snackbar, open: false });
  };

  return (
    <div className="empleados-container">
      <button onClick={() => setMostrarEliminados(!mostrarEliminados)}>
        {mostrarEliminados ? 'Ocultar Eliminados' : 'Mostrar Eliminados'}
      </button>
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

      {mostrarEliminados && (
        <>
          <h2>Empleados Eliminados</h2>
          <table className="empleados-table">
            <thead>
              <tr>
                <th>Nombre</th>
                <th>Apellido</th>
                <th>Legajo</th>
                <th>Puesto</th>
              </tr>
            </thead>
            <tbody className="fondo">
              {empleadosEliminados.map((empleado) => (
                <tr key={empleado.id}>
                  <td>{empleado.nombre}</td>
                  <td>{empleado.apellido}</td>
                  <td>{empleado.legajo}</td>
                  <td>{empleado.puesto}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </>
      )}

      <Snackbar open={snackbar.open} autoHideDuration={6000} onClose={handleCloseSnackbar}>
        <Alert onClose={handleCloseSnackbar} severity={snackbar.severity} sx={{ width: '100%' }}>
          {snackbar.message}
        </Alert>
      </Snackbar>
    </div>
  );
};

export default ListadoEmpleados;