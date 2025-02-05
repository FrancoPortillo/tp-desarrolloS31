import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';
import { obtenerEmpleadoPorEmail } from '../Utils/Axios';

export const ProtectedRoute = ({ children }) => {
  const { user, getAccessTokenSilently } = useAuth0();
  const [isAdmin, setIsAdmin] = React.useState(false);
  const [loading, setLoading] = React.useState(true);

  React.useEffect(() => {
    const fetchEmployeeData = async () => {
      if (user) {
        try {
          const token = await getAccessTokenSilently();
          const data = await obtenerEmpleadoPorEmail(token, user.email);
          setIsAdmin(data.rol); // Asumiendo que el atributo admin está en data.admin
        } catch (error) {
          console.error('Error al obtener los datos del empleado:', error);
        } finally {
          setLoading(false);
        }
      }
    };

    fetchEmployeeData();
  }, [user, getAccessTokenSilently]);

  if (loading) {
    return <div>Loading...</div>;
  }

  return isAdmin ? <Outlet /> : <Navigate to="/" />;
};

export default ProtectedRoute;