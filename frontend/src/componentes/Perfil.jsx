import { useEffect, useState } from 'react';
import { useAuth0 } from '@auth0/auth0-react';
import { useParams } from 'react-router-dom';
import { obtenerEmpleadoIndividual, obtenerEmpleadoPorId } from '../Utils/Axios';
import './Perfil.css';

export const Perfil = () => {
  const { id } = useParams(); // Obtener el ID del empleado desde los parámetros de la URL
  const { isAuthenticated, getAccessTokenSilently } = useAuth0();
  const [employeeData, setEmployeeData] = useState(null);

  useEffect(() => {
    const fetchEmployeeData = async () => {
      if (isAuthenticated) {
        try {
          const token = await getAccessTokenSilently();
          const data = await obtenerEmpleadoIndividual(id); // Obtener datos del empleado por ID
          setEmployeeData(data);
        } catch (error) {
          console.error('Error al obtener los datos del empleado:', error);
        }
      }
    };

    fetchEmployeeData();
  }, [isAuthenticated, getAccessTokenSilently, id]);

  if (!employeeData) {
    return <div className="loading">Cargando...</div>;
  }

  return (
    <div className="perfil-container full-width">
      <div className="perfil-card">
        <h2>Datos del Empleado</h2>
        <p><strong>Nombre:</strong> {employeeData.nombre}</p>
        <p><strong>Apellido:</strong> {employeeData.apellido}</p>
        <p><strong>Legajo:</strong> {employeeData.legajo}</p>
        <p><strong>DNI:</strong> {employeeData.dni}</p>
        <p><strong>Edad:</strong> {employeeData.edad}</p>
        <p><strong>Puesto:</strong> {employeeData.puesto}</p>
        <p><strong>Correo:</strong> {employeeData.email}</p>
      </div>
      <div className="perfil-card">
        <h2>Estadísticas</h2>
        <p><strong>Asistencias:</strong> {employeeData.asistencias}</p>
        <p><strong>Días de vacaciones:</strong> {employeeData.vacaciones}</p>
      </div>
    </div>
  );
};

export default Perfil;