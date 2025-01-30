import { useState, useEffect } from 'react';
import { obtenerEmpleados, registrarAsistencia } from '../../Utils/Axios';
import LlegadaTarde from './LlegadaTarde';
import './Asistencia.css';

export const Asistencia = () => {
  const [fecha, setFecha] = useState(new Date().toISOString().split('T')[0]);
  const [empleados, setEmpleados] = useState([]);
  const [asistencias, setAsistencias] = useState({});
  const [mostrarPopup, setMostrarPopup] = useState(false);
  const [selectedEmpleado, setSelectedEmpleado] = useState(null);

  useEffect(() => {
    const fetchEmpleados = async () => {
      try {
        const data = await obtenerEmpleados();
        setEmpleados(data);
      } catch (error) {
        console.error('Error al obtener empleados:', error);
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

  const handleCerrarPopup = () => {
    setMostrarPopup(false);
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
    } catch (error) {
      console.error('Error al registrar asistencia:', error);
    }
  };

  return (
    <>
      <div className="asistencia-container">
        <div className="fecha-container">
          <label className="fecha-label">Fecha:</label>
            <TextField
              type="date"
              value={fecha}
              onChange={handleFechaChange}
              InputLabelProps={{ shrink: true }}
              inputProps={{ max: new Date().toISOString().split('T')[0] }}
              className="fecha-selector"
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
                    onClick={() => handleAsistenciaChange(empleado.id, false)}
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

        <button className="boton-agregar-asistencia" onClick={handleSubmit}>Registrar Asistencia</button>

        {mostrarPopup && (
          <LlegadaTarde
            isOpen={mostrarPopup}
            onRequestClose={handleCerrarPopup}
            idEmpleado={selectedEmpleado}
            fecha={fecha}
          />
        )}
      </div>
    </>
  );
};

export default Asistencia;