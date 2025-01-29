import React, { useState, useEffect } from 'react';
import { obtenerEmpleados, registrarAsistencia } from '../../Utils/Axios';
import './Asistencia.css';

export const Asistencia = () => {
  const [fecha, setFecha] = useState(new Date().toISOString().split('T')[0]);
  const [empleados, setEmpleados] = useState([]);
  const [asistencias, setAsistencias] = useState({});

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
    setFecha(e.target.value);
  };

  const handleAsistenciaChange = (idEmpleado, presente) => {
    setAsistencias({
      ...asistencias,
      [idEmpleado]: { fecha, presente },
    });
    console.log(`Asistencia actualizada para empleado ${idEmpleado}:`, { fecha, presente });
  };

  const handleSubmit = async () => {
    const asistenciasArray = Object.keys(asistencias).map(idEmpleado => ({
      fecha: asistencias[idEmpleado].fecha,
      presente: asistencias[idEmpleado].presente,
      idEmpleado: parseInt(idEmpleado, 10),
    }));

    console.log('Asistencias a enviar:', asistenciasArray);
    asistenciasArray.forEach((asistencia, index) => {
      console.log(`Asistencia ${index}:`, asistencia);
    });

    try {
      await registrarAsistencia(asistenciasArray);
      alert('Asistencia registrada con éxito');
    } catch (error) {
      console.error('Error al registrar asistencia:', error);
      alert('Error al registrar asistencia');
    }
  };

  return (
    <div className="asistencia-container">
      <h2>Pasar Asistencia</h2>
      <div className="fecha-selector">
        <label>Fecha: </label>
        <input type="date" value={fecha} onChange={handleFechaChange} />
      </div>
      <div className="empleados-list">
        {empleados.map((empleado) => (
          <div key={empleado.id} className="empleado-item">
            <p>{empleado.nombre} ({empleado.puesto})</p>
            <button onClick={() => handleAsistenciaChange(empleado.id, true)}>Presente</button>
            <button onClick={() => handleAsistenciaChange(empleado.id, false)}>Ausente</button>
          </div>
        ))}
      </div>
      <button onClick={handleSubmit}>Registrar Asistencia</button>
    </div>
  );
};

export default Asistencia;