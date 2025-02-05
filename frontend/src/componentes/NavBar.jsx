import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import { NavLink } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';
import { useEffect, useState } from 'react';
import { obtenerEmpleadoPorEmail } from '../Utils/Axios';
import "./NavBar.css";

export const NavBar = ({ setIsAdmin, isAdmin }) => {
  const { loginWithRedirect, logout, user, isLoading, getAccessTokenSilently } = useAuth0();
  const [employeeId, setEmployeeId] = useState(null);

  useEffect(() => {
    const fetchEmployeeData = async () => {
      if (user) {
        try {
          const token = await getAccessTokenSilently();
          const data = await obtenerEmpleadoPorEmail(token, user.email);
          setEmployeeId(data.id); // Asumiendo que el ID del empleado está en data.id
          setIsAdmin(data.rol === "admin"); // Asumiendo que el atributo rol está en data.rol
        } catch (error) {
          console.error('Error al obtener los datos del empleado:', error);
        }
      }
    };

    fetchEmployeeData();
  }, [user, getAccessTokenSilently, setIsAdmin]);

  return (
    <AppBar position="static" color="default">
      <Toolbar>
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          <NavLink to='/' className={({ isActive }) => isActive ? "active-link" : "inactive-link"} style={{ textDecoration: 'none' }}>
            <Button color="inherit" className="nav-button">WorkSense</Button>
          </NavLink>
          <NavLink to={`/perfil/${employeeId}`} className={({ isActive }) => isActive ? "active-link" : "inactive-link"} style={{ textDecoration: 'none' }}>
            <Button color="inherit" className="nav-button">Perfil</Button>
          </NavLink>
          {isAdmin && (
            <>
              <NavLink to='/empleados' className={({ isActive }) => isActive ? "active-link" : "inactive-link"} style={{ textDecoration: 'none' }}>
                <Button color="inherit" className="nav-button">Empleados</Button>
              </NavLink>
              <NavLink to='/asistencia' className={({ isActive }) => isActive ? "active-link" : "inactive-link"} style={{ textDecoration: 'none' }}>
                <Button color="inherit" className="nav-button">Asistencia</Button>
              </NavLink>
            </>
          )}
          <NavLink to='/solicitudes' className={({ isActive }) => isActive ? "active-link" : "inactive-link"} style={{ textDecoration: 'none' }}>
            <Button color="inherit" className="nav-button">Solicitudes</Button>
          </NavLink>
        </Typography>
        {!isLoading && !user && (
          <Button 
            color="inherit"
            sx={{ border: '1px solid', marginLeft: 'auto' }}
            onClick={() => loginWithRedirect()}
          >
            Ingresar
          </Button>
        )}
        {!isLoading && user && (
          <Button 
            color="inherit"
            sx={{ border: '1px solid', marginLeft: 'auto' }}
            onClick={() => logout({ returnTo: window.location.origin })}
          >
            Salir
          </Button>
        )}
      </Toolbar>
    </AppBar>
  );
};

export default NavBar;