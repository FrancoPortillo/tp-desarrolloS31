import React, { useEffect, useState } from 'react';
import { obtenerEmpleados } from '../../Utils/Axios';
import './Empleados.css';
import Perfil from '../../componentes/Perfil';
import { NavLink } from 'react-router-dom';
import { Button } from 'bootstrap';

export const Empleados = () => {
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

  // Función para ordenar los empleados por columna
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
            <tr key={empleado.legajo}>
              <td>{empleado.nombre}</td>
              <td>{empleado.apellido}</td>
              <td>{empleado.legajo}</td>
              <td>{empleado.puesto}</td>
              <td>
                <button className="boton-ver-perfil">Ver Perfil</button>
                <button className="boton-eliminar">Eliminar</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <button className="boton-agregar">Agregar Empleado</button>
    </div>
  );
};

export default Empleados;
