import CalendarioVacaciones from './CalendarioVacaciones';
import { SolicitudVacaciones } from './SolicitudVacaciones';

export const Vacaciones = ({ isAdmin }) => {
  return (
    <div>
      {isAdmin ? <CalendarioVacaciones /> : <SolicitudVacaciones />}
    </div>
  );
};

export default Vacaciones;