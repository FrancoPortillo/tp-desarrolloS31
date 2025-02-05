import SolicitudesEmpleado from './SolicitudesEmpleado';
import SolicitudesAdmin from './SolicitudesAdmin';

export const Solicitudes = ({ isAdmin }) => {
  return (
    <div>
      {isAdmin ? <SolicitudesAdmin /> : <SolicitudesEmpleado />}
    </div>
  );
};

export default Solicitudes;