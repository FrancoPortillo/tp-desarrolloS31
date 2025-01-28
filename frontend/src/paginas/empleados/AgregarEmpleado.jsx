import React, { useState } from 'react';
import { agregarEmpleado } from '../../Utils/Axios';
import './Empleados.css';

export const AgregarEmpleado = ({ isOpen, onRequestClose, onEmpleadoAgregado }) => {
  const [nuevoEmpleado, setNuevoEmpleado] = useState({
    nombre: '',
    apellido: '',
    legajo: '',
    edad: '',
    dni: '',
    email: '',
    puesto: '',
    rol: '',
    telefono: '' // Añadir el campo telefono
  });

  const handleAgregar = async () => {
    try {
      const nuevoEmpleadoData = await agregarEmpleado(nuevoEmpleado);
      onEmpleadoAgregado(nuevoEmpleadoData);
      setNuevoEmpleado({
        nombre: '',
        apellido: '',
        legajo: '',
        edad: '',
        dni: '',
        email: '',
        puesto: '',
        rol: '',
        telefono: '' // Resetear el campo telefono
      });
      onRequestClose();
    } catch (error) {
      console.error('Error al agregar empleado:', error);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setNuevoEmpleado({ ...nuevoEmpleado, [name]: value });
  };

  return (
    <div className={`popup ${isOpen ? 'show' : ''}`}>
      <div className="popup-content">
        <h3>Agregar Nuevo Empleado</h3>
        <input type="text" name="nombre" value={nuevoEmpleado.nombre} onChange={handleChange} placeholder="Nombre" />
        <input type="text" name="apellido" value={nuevoEmpleado.apellido} onChange={handleChange} placeholder="Apellido" />
        <input type="number" name="legajo" value={nuevoEmpleado.legajo} onChange={handleChange} placeholder="Legajo" />
        <input type="number" name="edad" value={nuevoEmpleado.edad} onChange={handleChange} placeholder="Edad" />
        <input type="text" name="dni" value={nuevoEmpleado.dni} onChange={handleChange} placeholder="DNI" />
        <input type="email" name="email" value={nuevoEmpleado.email} onChange={handleChange} placeholder="Email" />
        <input type="text" name="puesto" value={nuevoEmpleado.puesto} onChange={handleChange} placeholder="Puesto" />
        <input type="text" name="rol" value={nuevoEmpleado.rol} onChange={handleChange} placeholder="Rol" />
        <input type="text" name="telefono" value={nuevoEmpleado.telefono} onChange={handleChange} placeholder="Teléfono" /> {/* Añadir el campo telefono */}
        <button className="boton-agregar" onClick={handleAgregar}>Agregar Empleado</button>
        <button className="boton-cerrar" onClick={onRequestClose}>Cerrar</button>
      </div>
    </div>
  );
};

export default AgregarEmpleado;