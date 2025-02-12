import { Outlet } from "react-router-dom";
import Cards from "./Menu/Cards";


export const MenuPrincipal = () => {
return (
  <div className="menu">
    <Cards/>
    <Outlet/>
  </div>
  )
}

export default MenuPrincipal;
