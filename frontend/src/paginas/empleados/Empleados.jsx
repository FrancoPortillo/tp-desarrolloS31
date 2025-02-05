import React, { useState, useEffect } from 'react';
import ListadoEmpleados from './ListadoEmpleados';
import AgregarEmpleado from './AgregarEmpleado';
import { obtenerEmpleados, obtenerEmpleadoPorEmail } from '../../Utils/Axios';
import { useAuth0 } from '@auth0/auth0-react';
import Swal from 'sweetalert2';

export const Empleados = () => {
  const [mostrarPopup, setMostrarPopup] = useState(false);
  const [empleados, setEmpleados] = useState([]);
  const { user, getAccessTokenSilently } = useAuth0();
  const [currentUserId, setCurrentUserId] = useState(null);

  useEffect(() => {
    const fetchEmpleados = async () => {
      try {
        const data = await obtenerEmpleados();
        console.log("data: ", data);
        setEmpleados(data);
      } catch (error) {
        console.error('Error al obtener empleados:', error);
      }
    };

    const fetchCurrentUserId = async () => {
      if (user) {
        try {
          const token = await getAccessTokenSilently();
          const data = await obtenerEmpleadoPorEmail(token, user.email);
          setCurrentUserId(data.id);
        } catch (error) {
          console.error('Error al obtener el ID del empleado:', error);
        }
      }
    };

    fetchEmpleados();
    fetchCurrentUserId();
  }, [user, getAccessTokenSilently]);

  const handleAgregarEmpleado = () => {
    setMostrarPopup(true);
  };

  const handleCerrarPopup = () => {
    setMostrarPopup(false);
  };

  const handleEmpleadoAgregado = (nuevoEmpleado) => {
    setEmpleados([...empleados, nuevoEmpleado]);
    setMostrarPopup(false);
    Swal.fire({
      icon: 'success',
      title: 'Éxito',
      text: 'Empleado agregado correctamente.',
      confirmButtonText: 'OK'
    });
  };

  const handleEliminarEmpleado = (id) => {
    setEmpleados((empleados) => empleados.filter(empleado => empleado.id !== id));
  };

  const handleOrdenarEmpleados = (empleadosOrdenados) => {
    setEmpleados(empleadosOrdenados);
  };

  return (
    <div>
      <ListadoEmpleados
        empleados={empleados}
        onAgregarEmpleado={handleAgregarEmpleado}
        onEliminarEmpleado={handleEliminarEmpleado}
        onOrdenarEmpleados={handleOrdenarEmpleados}
        currentUserId={currentUserId}
      />
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