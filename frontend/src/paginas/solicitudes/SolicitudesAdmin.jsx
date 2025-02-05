import { useState, useEffect } from 'react';
import { useAuth0 } from '@auth0/auth0-react';
import { modificarPermisoAusencia, obtenerPermisosAusencia } from '../../Utils/Axios';

const SolicitudesAdmin = () => {
  const { getAccessTokenSilently } = useAuth0();
  const [solicitudes, setSolicitudes] = useState([]);

  useEffect(() => {
    const fetchSolicitudes = async () => {
      try {
        const token = await getAccessTokenSilently();
        const response = await obtenerPermisosAusencia(token);
        setSolicitudes(response);
      } catch (error) {
        console.error('Error al obtener solicitudes:', error);
      }
    };

    fetchSolicitudes();
  }, [getAccessTokenSilently]);

  const handleAprobar = async (id) => {
    try {
      const token = await getAccessTokenSilently();
      await modificarPermisoAusencia(id, { estado: 'Aprobado' }, token);
      setSolicitudes(solicitudes.map(solicitud => 
        solicitud.id === id ? { ...solicitud, estado: 'Aprobado' } : solicitud
      ));
    } catch (error) {
      console.error('Error al aprobar solicitud:', error);
    }
  };

  const handleRechazar = async (id) => {
    try {
      const token = await getAccessTokenSilently();
      await modificarPermisoAusencia(id, { estado: 'Rechazado' }, token);
      setSolicitudes(solicitudes.map(solicitud => 
        solicitud.id === id ? { ...solicitud, estado: 'Rechazado' } : solicitud
      ));
    } catch (error) {
      console.error('Error al rechazar solicitud:', error);
    }
  };

  return (
    <div>
      <h2>Solicitudes</h2>
      <ul>
        {solicitudes.map(solicitud => (
          <li key={solicitud.id}>
            {solicitud.fechaSolicitado} - {solicitud.estado}
            <button onClick={() => handleAprobar(solicitud.id)}>Aprobar</button>
            <button onClick={() => handleRechazar(solicitud.id)}>Rechazar</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default SolicitudesAdmin;