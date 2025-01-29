import { Navigate, Route, Routes } from "react-router-dom";
import { MenuPrincipal } from "./componentes/MenuPrincipal";
import { Auth0Wrapper } from "./componentes/Auth0Wrapper";
import { Solicitudes } from "./componentes/Solicitudes";
import { withAuthenticationRequired } from "@auth0/auth0-react";
import Empleados from "./paginas/empleados/Empleados";
import Perfil from "./paginas/perfil/Perfil";
import { Asistencia } from "./paginas/Asistencia/Asistencia";

export const App = () => {
  return (
    <Auth0Wrapper>
      <MenuPrincipal />
      <Routes>
        <Route path="/perfil/:id" element={<Perfil />} /> 
        <Route path="/empleados" element={<Empleados />} />
        <Route path="/asistencia" element={<Asistencia/>} />
        <Route path="/solicitudes" element={<Solicitudes />} />
        <Route path="/*" element={<Navigate to="/" />} />
      </Routes>
    </Auth0Wrapper>
  );
};

const AuthenticatedApp = withAuthenticationRequired(App);
export default AuthenticatedApp;