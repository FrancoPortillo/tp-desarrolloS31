import { Outlet } from "react-router-dom";
import { NavBar } from "./NavBar"

export const MenuPrincipal = () => {
return (
  <div>
    <NavBar/>
    <Outlet/>
  </div>
  )
}

export default MenuPrincipal;
