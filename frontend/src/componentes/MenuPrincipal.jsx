import { Outlet } from "react-router-dom";
import AcercaDe from "./Menu/AcercaDe";


export const MenuPrincipal = () => {
return (
  <div className="menu">
    <AcercaDe/>
    <Outlet/>
  </div>
  )
}

export default MenuPrincipal;
