import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import { NavLink } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';
import { useEffect, useState } from 'react';
import { obtenerEmpleadoPorEmail } from '../Utils/Axios';

export const NavBar = () => {
  const { loginWithRedirect, logout, user, isLoading, getAccessTokenSilently } = useAuth0();
  const [employeeId, setEmployeeId] = useState();

  useEffect(() => {
    const fetchEmployeeId = async () => {
      if (user) {
        try {
          const token = await getAccessTokenSilently();
          const data = await obtenerEmpleadoPorEmail(token, user.email);
          console.log(data);
          setEmployeeId(data); // Asumiendo que el ID del empleado está en data.id
        } catch (error) {
          console.error('Error al obtener el ID del empleado:', error);
        }
      }
    };

    fetchEmployeeId();
  }, [user, getAccessTokenSilently]);

  return (
    <AppBar position="static" color="default">
      <Toolbar>
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          <Button color="inherit" component={NavLink} to='/'>Menu</Button>
          {employeeId && (
            <Button color="inherit" component={NavLink} to={`/perfil/${employeeId.id}`}>Perfil</Button>
          )}
          <Button color="inherit" component={NavLink} to='/empleados'>Empleados</Button>
          <Button color="inherit" component={NavLink} to='/solicitudes'>Solicitudes</Button>
          {!isLoading && !user && (
            <Button 
              color="inherit"
              sx={{ border: '1px solid' }}
              onClick={() => loginWithRedirect()}
            >
              Ingresar
            </Button>
          )}
          {!isLoading && user && (
            <Button 
              color="inherit"
              sx={{ border: '1px solid' }}
              onClick={() => logout({ returnTo: window.location.origin })}
            >
              Salir
            </Button>
          )}
        </Typography>
      </Toolbar>
    </AppBar>
  );
};