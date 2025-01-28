import { Navigate, Route, Routes } from "react-router-dom";
import { MenuPrincipal } from "./componentes/MenuPrincipal";
import { Auth0Wrapper } from "./componentes/Auth0Wrapper";
import { Empleados } from "./componentes/Empleados";
import { Perfil } from "./componentes/Perfil";
import { Solicitudes } from "./componentes/Solicitudes";
import { withAuthenticationRequired } from "@auth0/auth0-react";


// export const App = () => {

//   const login = token => i.post('auth/sign-in', { token }).then(({ data }) => {
//     return data;
//   });


//   return (
//     <Auth0Wrapper>
//       <MenuPrincipal />
//       <Routes>
//         <Route path="/perfil" element={<Perfil />} />
//         <Route path="/empleados" element={<Empleados id={0} />} />
//         <Route path="/solicitudes" element={<Solicitudes />} />
//         <Route path="/*" element={<Navigate to="/" />} />
//       </Routes>
//     </Auth0Wrapper>
//   );
// };

// export default App;

export const App = () => {

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
  )
};

const AuthenticatedApp = withAuthenticationRequired(App);
export default AuthenticatedApp;

