import { useState, useEffect } from 'react';
import { obtenerEmpleadoIndividual } from '../Utils/Axios';

export const Empleados = ({ id }) => {
  
  const [empleado, setEmpleado] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchEmpleado = async () => {
      try {
        const data = await obtenerEmpleadoIndividual(id);
        setEmpleado(data);
      } catch (err) {
        setError(err.message);
      }
    };

    fetchEmpleado();
  }, [id]);

  if (error) {
    return <div>Error: {error}</div>;
  }

  if (!empleado) {
    return <div>Cargando...</div>;
  }

  return (
    <div>
      <h1>Detalles del Empleado</h1>
      <p>ID: {empleado.id}</p>
      <p>Nombre: {empleado.nombre}</p>
      <p>Apellido: {empleado.apellido}</p>
      <p>Email: {empleado.email}</p>
      {/* Agrega más campos según tu EmpleadoDTO */}
    </div>
  );
};

export default Empleados;
