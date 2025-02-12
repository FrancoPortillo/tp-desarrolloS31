import { useState, useEffect } from 'react';
import { obtenerEmpleados, registrarAsistencia } from '../../Utils/Axios';
import LlegadaTarde from './LlegadaTarde';
import { TextField } from '@mui/material';
import './Asistencia.css';
import Boton from '../../componentes/Boton/Boton';
import { Snackbar, Alert } from '@mui/material';
import { BasicDatePicker } from '../../componentes/BasicDatePicker';
import Inasistencia from './Inasistencia';

export const Asistencia = () => {
  const [fecha, setFecha] = useState(new Date().toISOString().split('T')[0]);
  const [empleados, setEmpleados] = useState([]);
  const [asistencias, setAsistencias] = useState({});
  const [mostrarPopup, setMostrarPopup] = useState(false);
  const [mostrarInasistencia, setMostrarInasistencia] = useState(false);
  const [selectedEmpleado, setSelectedEmpleado] = useState(null);
  const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'success' });

  useEffect(() => {
    const fetchEmpleados = async () => {
      try {
        const data = await obtenerEmpleados();
        setEmpleados(data);
      } catch (error) {
        console.error('Error al obtener empleados:', error);
        setSnackbar({ open: true, message: 'Error al obtener empleados.', severity: 'error' });
      }
    };

    fetchEmpleados();
  }, []);

  const handleFechaChange = (e) => {
    const selectedDate = e.target.value;
    const today = new Date().toISOString().split('T')[0];
    if (selectedDate <= today) {
      setFecha(selectedDate);
    }
  };

  const handleAsistenciaChange = (idEmpleado, presente) => {
    setAsistencias({
      ...asistencias,
      [idEmpleado]: { fecha, presente },
    });
  };

  const handleLlegadaTarde = (idEmpleado) => {
    setAsistencias({
      ...asistencias,
      [idEmpleado]: { fecha, presente: true },
    });
    setSelectedEmpleado(idEmpleado);
    setMostrarPopup(true);
  };

  const handleInasistencia = (idEmpleado) => {
    setAsistencias({
      ...asistencias,
      [idEmpleado]: { fecha, presente: false },
    });
    setSelectedEmpleado(idEmpleado);
    setMostrarInasistencia(true);
  };

  const handleCerrarPopup = () => {
    setMostrarPopup(false);
    setMostrarInasistencia(false);
    setSelectedEmpleado(null);
  };

  const handleSubmit = async () => {
    const asistenciasArray = Object.keys(asistencias).map(idEmpleado => ({
      fecha: new Date(asistencias[idEmpleado].fecha).toISOString(),
      presente: asistencias[idEmpleado].presente,
      idEmpleado: parseInt(idEmpleado, 10)
    }));

    try {
      await registrarAsistencia(asistenciasArray);
      setSnackbar({ open: true, message: 'Asistencia registrada correctamente.', severity: 'success' });
    } catch (error) {
      console.error('Error al registrar asistencia:', error);
      setSnackbar({ open: true, message: 'Error al registrar asistencia.', severity: 'error' });
    }
  };

  const handleCloseSnackbar = () => {
    setSnackbar({ ...snackbar, open: false });
  };

  return (
    <>
      <div className="asistencia-container">
        <div className="fecha-container">
          <BasicDatePicker
            label="Fecha"
            date={fecha}
            onChange={handleFechaChange}
            InputLabelProps={{ shrink: true }}
            inputProps={{ max: new Date().toISOString().split('T')[0] }}
            required
            sx={{ mb: 2 }}
          />
        </div>
        <table className="asistencia-table">
          <thead>
            <tr>
              <th>Nombre</th>
              <th>Apellido</th>
              <th>Legajo</th>
              <th>Puesto</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody className="fondo">
            {empleados.map((empleado) => (
              <tr key={empleado.id}>
                <td>{empleado.nombre}</td>
                <td>{empleado.apellido}</td>
                <td>{empleado.legajo}</td>
                <td>{empleado.puesto}</td>
                <td>
                  <button
                    className={`boton-presente ${asistencias[empleado.id]?.presente === true ? 'activo' : ''}`}
                    onClick={() => handleAsistenciaChange(empleado.id, true)}
                  >
                    Presente
                  </button>
                  <button
                    className={`boton-ausente ${asistencias[empleado.id]?.presente === false ? 'activo' : ''}`}
                    onClick={() => handleInasistencia(empleado.id)}
                  >
                    Ausente
                  </button>
                  <button
                    className={`boton-tarde ${asistencias[empleado.id]?.presente === 'tarde' ? 'activo' : ''}`}
                    onClick={() => handleLlegadaTarde(empleado.id)}
                  >
                    Tarde
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        <Boton texto="Registrar Asistencia" onClick={handleSubmit}/>

        {mostrarPopup && (
          <LlegadaTarde
            isOpen={mostrarPopup}
            onRequestClose={handleCerrarPopup}
            idEmpleado={selectedEmpleado}
            fecha={fecha}
          />
        )}
        {mostrarInasistencia && (
          <Inasistencia
            isOpen={mostrarInasistencia}
            onRequestClose={handleCerrarPopup}
            idEmpleado={selectedEmpleado}
            fecha={fecha}
          />
        )}
        <Snackbar open={snackbar.open} autoHideDuration={6000} onClose={handleCloseSnackbar}>
          <Alert onClose={handleCloseSnackbar} severity={snackbar.severity} sx={{ width: '100%' }}>
            {snackbar.message}
          </Alert>
        </Snackbar>
      </div>
    </>
  );
};

export default Asistencia;