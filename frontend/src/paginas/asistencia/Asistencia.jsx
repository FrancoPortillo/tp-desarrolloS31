import  { useState, useEffect } from 'react';
import { obtenerEmpleados, registrarAsistencia, registrarLlegadaTarde } from '../../Utils/Axios';
// import './Asistencia.css';

export const Asistencia = () => {
  const [fecha, setFecha] = useState(new Date().toISOString().split('T')[0]);
  const [empleados, setEmpleados] = useState([]);
  const [asistencias, setAsistencias] = useState({});
  const [motivos, setMotivos] = useState({});
  const [minutosTarde, setMinutosTarde] = useState({});

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

  const handleAsistenciaChange = (idEmpleado, estado) => {
    setAsistencias({
      ...asistencias,
      [idEmpleado]: { fecha, presente: estado !== 'ausente', estado },
    });
  };

  const handleMotivoChange = (idEmpleado, motivo) => {
    setMotivos({
      ...motivos,
      [idEmpleado]: motivo,
    });
  };

  const handleMinutosTardeChange = (idEmpleado, minutos) => {
    setMinutosTarde({
      ...minutosTarde,
      [idEmpleado]: minutos,
    });
  };

  const handleSubmit = async () => {
    const asistenciasArray = Object.keys(asistencias).map(idEmpleado => ({
      ...asistencias[idEmpleado],
      idEmpleado: parseInt(idEmpleado, 10),
    }));

    try {
      await registrarAsistencia(asistenciasArray);

      // Registrar llegadas tarde
      const llegadasTarde = asistenciasArray.filter(a => a.estado === 'tarde');
      for (const llegadaTarde of llegadasTarde) {
        await registrarLlegadaTarde({
          fecha: llegadaTarde.fecha,
          idEmpleado: llegadaTarde.idEmpleado,
          motivo: motivos[llegadaTarde.idEmpleado] || '',
          minutosTarde: minutosTarde[llegadaTarde.idEmpleado] || 0
        });
      }

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
            <button onClick={() => handleAsistenciaChange(empleado.id, 'presente')}>Presente</button>
            <button onClick={() => handleAsistenciaChange(empleado.id, 'ausente')}>Ausente</button>
            <button onClick={() => handleAsistenciaChange(empleado.id, 'tarde')}>Tarde</button>
            {asistencias[empleado.id]?.estado === 'tarde' && (
              <>
                <input
                  type="text"
                  placeholder="Motivo de la llegada tarde"
                  value={motivos[empleado.id] || ''}
                  onChange={(e) => handleMotivoChange(empleado.id, e.target.value)}
                />
                <input
                  type="number"
                  placeholder="Minutos tarde"
                  value={minutosTarde[empleado.id] || ''}
                  onChange={(e) => handleMinutosTardeChange(empleado.id, e.target.value)}
                />
              </>
            )}
          </div>
        ))}
      </div>
      <button onClick={handleSubmit}>Registrar Asistencia</button>
    </div>
  );
};

export default Asistencia;