import { Navigate, Route, Routes } from "react-router-dom";
import { MenuPrincipal } from "./componentes/MenuPrincipal";
import { Auth0Wrapper } from "./componentes/Auth0Wrapper";
import { Empleados } from "./componentes/Empleados";
import { Perfil } from "./componentes/Perfil";
import { Solicitudes } from "./componentes/Solicitudes";
export const App = () => {
  return (
      <Auth0Wrapper>
          <MenuPrincipal/>
          <Routes>
              {/* <Route path="/" element={<MenuPrincipal />} /> */}
              <Route path="/perfil" element={<Perfil />} />
              <Route path="/empleados" element={<Empleados />} />
              <Route path="/solicitudes" element={<Solicitudes />} />
              <Route path="/*" element={<Navigate to="/" />} /> 
          </Routes> 
      </Auth0Wrapper>
  );
};

export default App;