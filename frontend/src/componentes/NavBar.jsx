import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import { NavLink } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';
import { useEffect, useState } from 'react';
import { obtenerEmpleadoPorEmail } from '../Utils/Axios';
import "./NavBar.css";
import LogoutButton from './LogoutButton';
import worksense  from "../assets/worksense.png"

export const NavBar = ({ setIsAdmin, isAdmin }) => {
  const { loginWithRedirect, logout, user, isLoading, getAccessTokenSilently } = useAuth0();
  const [employeeId, setEmployeeId] = useState(null);

  useEffect(() => {
    const fetchEmployeeData = async () => {
      if (user) {
        try {
          const token = await getAccessTokenSilently();
          console.log("TOKEN", token);
          console.log("USER", user.email);
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
    <>
    <header style={{ 
        height: '10vh', 
        backgroundColor: 'rgb(59, 59, 59)',
        display: 'flex', 
        alignItems: 'center', 
        justifyContent: 'space-between',
        padding: '0 2vh'
        }}>
        <img src={worksense} alt="Miksa Logo" style={{ height: '40%' }} className="clickable-logo" />
        <LogoutButton />
      </header>
    <AppBar position="static" color="#c0cbff">
      <Toolbar>
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          <NavLink to='/' className={({ isActive }) => isActive ? "active-link" : "inactive-link"} style={{ textDecoration: 'none' }}>
            <Button color="inherit" className="nav-button">Menú</Button>
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
      </Toolbar>
    </AppBar>
    </>
  );
};

export default NavBar;