import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import { NavLink } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';

export const NavBar = () => {
  const { loginWithRedirect, logout, user, isLoading } = useAuth0();

  console.log('isLoading:', isLoading);
  console.log('user:', user);

  return (
    <AppBar position="static" color="default">
      <Toolbar>
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          <Button color="inherit" component={NavLink} to='/'>Menu</Button>
          <Button color="inherit" component={NavLink} to='/perfil'>Perfil</Button>
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
// import AppBar from '@mui/material/AppBar';
// import Toolbar from '@mui/material/Toolbar';
// import Typography from '@mui/material/Typography';
// import Button from '@mui/material/Button';
// import { NavLink } from 'react-router-dom';
// import { useAuth0 } from '@auth0/auth0-react';
// // import LogoutButton from './LogoutButton';
// export const NavBar = () => {
//     const { loginWithRedirect, logout, user, isLoading } = useAuth0();
    
// return (
//     <AppBar position="static" color="default">
//         <Toolbar>
//             <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
//                 <Button color="inherit" component={NavLink} to='/'>Menu</Button>
//                 <Button color="inherit" component={NavLink} to='/perfil'>Perfil</Button>
//                 <Button color="inherit" component={NavLink} to='/empleados'>Empleados</Button>
//                 <Button color="inherit" component={NavLink} to='/solicitudes'>Solicitudes</Button>
//                 {!isLoading && !user && (
//                     <Button 
//                         color="inherit"
//                         sx={{ border: '1px solid' }}
//                         onClick={() => loginWithRedirect()}
//                     >
//                         Ingresar
//                     </Button>
//                 )}
//                 {!isLoading && user && (
//                     <Button 
//                         color="inherit"
//                         sx={{ border: '1px solid' }}
//                         onClick={() => logout({ returnTo: window.location.origin })}
//                     >
//                         Salir
//                     </Button>
//                 )}
//             </Typography>
//         </Toolbar>
//     </AppBar>
// )
// }
