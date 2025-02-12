import { Navigate, Route, Routes } from "react-router-dom";
import { MenuPrincipal } from "./componentes/MenuPrincipal";
import { Auth0Wrapper } from "./componentes/Auth0Wrapper";
import { Solicitudes } from "./paginas/solicitudes/Solicitudes";
import Empleados from "./paginas/empleados/Empleados";
import Perfil from "./paginas/perfil/Perfil";
import { Asistencia } from "./paginas/Asistencia/Asistencia";
import NavBar from "./componentes/NavBar";
import ProtectedRoute from "./componentes/ProtectedRoute";
import "./App.css"
import { useState } from 'react';
import { withAuthenticationRequired } from "@auth0/auth0-react";
import Vacaciones from "./paginas/vacaciones/Vacaciones";

export const App = () => {
  const [isAdmin, setIsAdmin] = useState(false);

  return (
    <Auth0Wrapper>
      <NavBar setIsAdmin={setIsAdmin} isAdmin={isAdmin} />
      <Routes>
        <Route path="/" element={<MenuPrincipal />} />
        <Route path="/perfil/:id" element={<Perfil />} /> 
        <Route element={<ProtectedRoute />}>  
          <Route path="/empleados" element={<Empleados />} />
          <Route path="/asistencia" element={<Asistencia />} />
          <Route path="/solicitudes" element={<Solicitudes isAdmin={isAdmin} />} />
          <Route path="/vacaciones" element={<Vacaciones isAdmin={isAdmin} />} />
        </Route>
        <Route path="/*" element={<Navigate to="/" />} />
      </Routes>
    </Auth0Wrapper>
  );
};

const AuthenticatedApp = withAuthenticationRequired(App);
export default AuthenticatedApp;