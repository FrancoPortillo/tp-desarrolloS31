import { useAuth0 } from '@auth0/auth0-react';
import { useEffect, useState } from 'react';
import { obtenerEmpleadoPorEmail } from '../Utils/Axios';

export const Perfil = () => {
  const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();
  const [employeeData, setEmployeeData] = useState(null);

  useEffect(() => {
    const fetchEmployeeData = async () => {
      if (isAuthenticated) {
        try {
          console.log('Usuario autenticado:', user);
          console.log("Mail del ususario:", user.email);
          const token = await getAccessTokenSilently();
          console.log('Token obtenido:', token);
          const data = await obtenerEmpleadoPorEmail(token, user.email);
          console.log('Datos del empleado:', data);
          setEmployeeData(data);
        } catch (error) {
          console.error('Error al obtener los datos del empleado:', error);
        }
      }
    };

    fetchEmployeeData();
  }, [isAuthenticated, getAccessTokenSilently, user]);

  if (!employeeData) {
    return <div>Cargando...</div>;
  }

  return (
    <div>
      <h1>Perfil de {employeeData.name}</h1>
      <p>Email: {employeeData.email}</p>
      <p>Posición: {employeeData.position}</p>
      {/* Muestra otros datos del empleado según sea necesario */}
    </div>
  );
};