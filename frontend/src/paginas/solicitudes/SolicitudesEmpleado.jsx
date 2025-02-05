import { useState, useEffect } from 'react';
import { useAuth0 } from '@auth0/auth0-react';
import { agregarPermisoAusencia, obtenerEmpleadoPorEmail, obtenerPermisosAusenciaPorEmpleado } from '../../Utils/Axios';

const SolicitudesEmpleado = () => {
  const { user, getAccessTokenSilently } = useAuth0();
  const [empleadoID, setEmpleadoID] = useState(null);
  const [solicitudes, setSolicitudes] = useState([]);
  const [nuevaSolicitud, setNuevaSolicitud] = useState({
    fechaInicio: '',
    fechaFin: '',
    detalles: '',
    tipo: '',
    estado: 'Pendiente', // estado inicial
    documentaciones: []
  });
  const [documentacion, setDocumentacion] = useState({
    nombreArchivo: '',
    content: '',
    idEmpleado: ''
  });

  useEffect(() => {
    const fetchEmployeeData = async () => {
      if (user) {
        try {
          const token = await getAccessTokenSilently();
          const data = await obtenerEmpleadoPorEmail(token, user.email);
          setEmpleadoID(data.id); // Asumiendo que el ID del empleado está en data.id
          console.log(data.id);
          const response = await obtenerPermisosAusenciaPorEmpleado(data.id);
          setSolicitudes(response);
        } catch (error) {
          console.error('Error al obtener los datos del empleado:', error);
        }
      }
    };

    fetchEmployeeData();
  }, [user, getAccessTokenSilently]);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNuevaSolicitud({ ...nuevaSolicitud, [name]: value });
  };

  const handleFileChange = (e) => {
    const file = e.target.files[0];
    const reader = new FileReader();
    reader.onload = () => {
      setDocumentacion({ ...documentacion, contenido: reader.result, idEmpleado: empleadoID });
    };
    reader.readAsArrayBuffer(file);
  };

  const handleSolicitarPermiso = async () => {
    try {
      const token = await getAccessTokenSilently();
      const solicitudConDocumentacion = {
        ...nuevaSolicitud,
        idEmpleado: empleadoID,
        documentaciones: documentacion.content ? [documentacion] : []
      };
      await agregarPermisoAusencia(solicitudConDocumentacion, token);
      setNuevaSolicitud({
        fechaInicio: '',
        fechaFin: '',
        detalles: '',
        tipo: '',
        estado: 'Pendiente',
        documentaciones: []
      });
      setDocumentacion({
        nombreArchivo: '',
        content: '',
        idEmpleado: ''
      });
      const response = await obtenerPermisosAusenciaPorEmpleado(empleadoID);
      setSolicitudes(response);
    } catch (error) {
      console.error('Error al solicitar permiso:', error);
    }
  };
  return (
    <div>
      <h2>Mis Solicitudes</h2>
      <ul>
        {solicitudes.map(solicitud => (
          <li key={solicitud.id}>
            Tipo: {solicitud.tipo} - Detalles: {solicitud.detalles} - Fecha Solicitado: {solicitud.fechaSolicitado} - Estado: {solicitud.estado}
          </li>
        ))}
      </ul>
      <div>
        <h3>Solicitar Nuevo Permiso</h3>
        <input
          type="date"
          name="fechaInicio"
          value={nuevaSolicitud.fechaInicio}
          onChange={handleInputChange}
          placeholder="Fecha de Inicio"
        />
        <input
          type="date"
          name="fechaFin"
          value={nuevaSolicitud.fechaFin}
          onChange={handleInputChange}
          placeholder="Fecha de Fin"
        />
        <input
          type="text"
          name="detalles"
          value={nuevaSolicitud.detalles}
          onChange={handleInputChange}
          placeholder="Detalles"
        />
        <input
          type="text"
          name="tipo"
          value={nuevaSolicitud.tipo}
          onChange={handleInputChange}
          placeholder="Tipo"
        />
        <input
          type="file"
          onChange={handleFileChange}
        />
        <button onClick={handleSolicitarPermiso}>Solicitar Permiso</button>
      </div>
    </div>
  );
};

export default SolicitudesEmpleado;