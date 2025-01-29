import React, { useState } from 'react';
import ListadoEmpleados from './ListadoEmpleados';
import AgregarEmpleado from './AgregarEmpleado';

export const Empleados = () => {
  const [mostrarPopup, setMostrarPopup] = useState(false);
  const [empleados, setEmpleados] = useState([]);

  const handleAgregarEmpleado = () => {
    setMostrarPopup(true);
  };

  const handleCerrarPopup = () => {
    setMostrarPopup(false);
  };

  const handleEmpleadoAgregado = (nuevoEmpleado) => {
    setEmpleados([...empleados, nuevoEmpleado]);
  };

  return (
    <div>
      <ListadoEmpleados onAgregarEmpleado={handleAgregarEmpleado} />
      {mostrarPopup && (
        <AgregarEmpleado
          isOpen={mostrarPopup}
          onRequestClose={handleCerrarPopup}
          onEmpleadoAgregado={handleEmpleadoAgregado}
        />
      )}
    </div>
  );
};

export default Empleados;