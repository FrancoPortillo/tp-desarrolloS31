import { Navigate, Route, Routes } from "react-router-dom";
import { MenuPrincipal } from "./componentes/MenuPrincipal";
import { Auth0Wrapper } from "./componentes/Auth0Wrapper";
import { Empleados } from "./componentes/Empleados";
import { Perfil } from "./componentes/Perfil";
import { Solicitudes } from "./componentes/Solicitudes";
import { useAuth0 } from '@auth0/auth0-react';
import { useEffect } from 'react';
import axios from 'axios';

export const App = () => {

  const fetchEmpleado = async (id) => {
    try {
      const response = await fetch(`http://localhost:5237/Empleado/ObtenerIndividual/${id}`);
      const data = await response.json();
      console.log(data);
    } catch (error) {
      console.error('Error al obtener empleado individual:', error);
    }
  };
  
  fetchEmpleado(1); // Cambia el ID según sea necesario
  const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();

  useEffect(() => {
    const associateEmployeeId = async () => {
      if (isAuthenticated) {
        try {
          const token = await getAccessTokenSilently();
          const response = await axios.post(
            'https://your-api-endpoint/associate-employee',
            {
              auth0UserId: user.sub,
              email: user.email, // Utiliza el correo electrónico para buscar el ID de empleado
            },
            {
              headers: {
                Authorization: `Bearer ${token}`,
              },
            }
          );
          console.log('Asociación exitosa:', response.data);
        } catch (error) {
          console.error('Error al asociar el ID de empleado:', error);
        }
      }
    };

    associateEmployeeId();
  }, [isAuthenticated, getAccessTokenSilently, user]);

  return (
    <Auth0Wrapper>
      <MenuPrincipal />
      <Routes>
        <Route path="/perfil" element={<Perfil />} />
        <Route path="/empleados" element={<Empleados id={0} />} />
        <Route path="/solicitudes" element={<Solicitudes />} />
        <Route path="/*" element={<Navigate to="/" />} />
      </Routes>
    </Auth0Wrapper>
  );
};

export default App;

