import { useEffect, useState } from 'react';
import { agregarVacaciones, obtenerEmpleados, obtenerPermisosAusencia, obtenerVacaciones } from '../../Utils/Axios';
import Boton from '../../componentes/Boton/Boton';
import Swal from 'sweetalert2';
import './SolicitudVacaciones.css';
import CalendarioVacaciones from './CalendarioVacaciones';
import { useAuth0 } from '@auth0/auth0-react';
import dayjs from 'dayjs';
import utc from 'dayjs/plugin/utc';
import { BasicDatePicker } from '../../componentes/BasicDatePicker';
import { Button } from '@mui/material';

dayjs.extend(utc);

export const VacacionesSolicitud = ({ isOpen, onRequestClose, onVacacionAgregada, idEmpleado }) => {
  const { getAccessTokenSilently } = useAuth0();
  const [vacaciones, setVacaciones] = useState([]);
  const [solicitudes, setSolicitudes] = useState([]);
  const [empleados, setEmpleados] = useState([]);

  const [nuevaVacacion, setNuevaVacacion] = useState({
    fechaSolicitado: '',
    fechaInicio: '',
    fechaFin: '',
    dias: 0,
    estado: 'Pendiente',
    idEmpleado: idEmpleado
  });
  const [errors, setErrors] = useState({});

  const validateVacacion = () => {
    const newErrors = {};

    if (!nuevaVacacion.fechaSolicitado) {
      newErrors.fechaSolicitado = "La fecha de solicitud es obligatoria.";
    }
    if (!nuevaVacacion.fechaInicio) {
      newErrors.fechaInicio = "La fecha de inicio es obligatoria.";
    }
    if (!nuevaVacacion.fechaFin) {
      newErrors.fechaFin = "La fecha de fin es obligatoria.";
    }
    if (nuevaVacacion.dias <= 0) {
      newErrors.dias = "Los días deben ser un número positivo.";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleGuardar = async (e) => {
    e.preventDefault();
    if (!validateVacacion()) {
      return;
    }

    try {
      const vacacionData = {
        ...nuevaVacacion,
        fechaSolicitado: dayjs(nuevaVacacion.fechaSolicitado).utc().format(),
        fechaInicio: dayjs(nuevaVacacion.fechaInicio).utc().format(),
        fechaFin: dayjs(nuevaVacacion.fechaFin).utc().format(),
      };

      const response = await agregarVacaciones(vacacionData);
      setNuevaVacacion(response);
      onVacacionAgregada(response);
      onRequestClose();
      Swal.fire({
        icon: 'success',
        title: 'Éxito',
        text: 'Solicitud de vacaciones guardada correctamente.',
        confirmButtonText: 'OK'
      });
    } catch (error) {
      console.error('Error al guardar solicitud de vacaciones:', error);
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: 'Error al guardar la solicitud de vacaciones.',
        confirmButtonText: 'OK'
      });
    }
  };

  const handleChange = (name, value) => {
    setNuevaVacacion({ ...nuevaVacacion, [name]: value });
  };

  const handleClickOutside = (e) => {
    if (e.target.classList.contains('popup')) {
      onRequestClose();
    }
  };

  const callApi = async () => {
    try {
      const token = await getAccessTokenSilently();
      const respuesta = await obtenerPermisosAusencia(token);
      const resp = await obtenerVacaciones(token);
      const response = await obtenerEmpleados(token);

      const solicitudesConNombre = respuesta.map(solicitud => {
        const empleado = response.find(emp => emp.id === solicitud.idEmpleado);
        return { ...solicitud, nombre: empleado ? empleado.nombre : 'Desconocido', tipoSolicitud: 'Permiso' };
      });
      const vacacionesConNombre = resp.map(vacacion => {
        const empleado = response.find(emp => emp.id === vacacion.idEmpleado);
        return { ...vacacion, nombre: empleado ? empleado.nombre : 'Desconocido', tipoSolicitud: 'Vacaciones' };
      });
      setSolicitudes(solicitudesConNombre);
      setVacaciones(vacacionesConNombre);
      setEmpleados(response);
    } catch (error) {
      console.error('Error al obtener datos:', error);
    }
  };

  useEffect(() => {
    callApi();
  }, []);

  return (
    <div className={`popup ${isOpen ? 'show' : ''}`} onClick={handleClickOutside}>
      <div className="popup-content">
        <h4>Solicitar Vacaciones</h4>
        <div>
          <CalendarioVacaciones vacaciones={vacaciones} permisos={solicitudes} empleados={empleados} fullWidth={true} />
        </div>
        <form className="vacaciones-solicitud-form" onSubmit={handleGuardar}>
          <div className="form-group">
            <div className="field">
              <BasicDatePicker
                label="Fecha Solicitado"
                date={nuevaVacacion.fechaSolicitado}
                onChange={(date) => handleChange('fechaSolicitado', date)}
              />
              {errors.fechaSolicitado && <span className="error">{errors.fechaSolicitado}</span>}
            </div>
            <div className="field">
              <BasicDatePicker
                label="Fecha Inicio"
                date={nuevaVacacion.fechaInicio}
                onChange={(date) => handleChange('fechaInicio', date)}
              />
              {errors.fechaInicio && <span className="error">{errors.fechaInicio}</span>}
            </div>
          </div>
          <div className="form-group">
            <div className="field">
              <BasicDatePicker
                label="Fecha Fin"
                date={nuevaVacacion.fechaFin}
                onChange={(date) => handleChange('fechaFin', date)}
              />
              {errors.fechaFin && <span className="error">{errors.fechaFin}</span>}
            </div>
            <div className="field-small">
              <label>
                Días
                <input type="number" name="dias" value={nuevaVacacion.dias} onChange={(e) => handleChange('dias', e.target.value)} />
                {errors.dias && <span className="error">{errors.dias}</span>}
              </label>
            </div>
          </div>
          <div className="botones">
            <Boton texto="Guardar" type="submit" />
            <Button
              variant="contained"
              sx={{
                backgroundColor: "#f44336", // Rojo
                color: 'white',
                '&:hover': {
                  backgroundColor: "#d32f2f", // Rojo más oscuro al pasar el mouse
                },
              }}
              onClick={onRequestClose}
            >
              Cerrar
            </Button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default VacacionesSolicitud;