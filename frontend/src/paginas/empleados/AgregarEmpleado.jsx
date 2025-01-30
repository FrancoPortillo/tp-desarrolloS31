import { useState } from 'react';
import { agregarEmpleado } from '../../Utils/Axios';
import './AgregarEmpleado.css';

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
    telefono: '' 
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
        telefono: '' 
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

  const handleClickOutside = (e) => {
    if (e.target.classList.contains('popup')) {
      onRequestClose();
    }
  };

  return (
    <div className={`popup ${isOpen ? 'show' : ''}`} onClick={handleClickOutside}>
      <div className="popup-content">
        <h3>Alta de Empleado</h3>
        <form className="agregar-empleado-form">
          <div className="form-group">
            <label>
              Nombre
              <input type="text" name="nombre" value={nuevoEmpleado.nombre} onChange={handleChange} placeholder="Ej: Juan" />
            </label>
            <label>
              Apellido
              <input type="text" name="apellido" value={nuevoEmpleado.apellido} onChange={handleChange} placeholder="Ej: Pérez" />
            </label>
          </div>
          <div className="form-group">
            <label>
              Legajo
              <input type="number" name="legajo" value={nuevoEmpleado.legajo} onChange={handleChange} placeholder="Ej: 12345" />
            </label>
            <label>
              Edad
              <input type="number" name="edad" value={nuevoEmpleado.edad} onChange={handleChange} placeholder="Ej: 30" />
            </label>
          </div>
          <div className="form-group">
            <label>
              DNI
              <input type="text" name="dni" value={nuevoEmpleado.dni} onChange={handleChange} placeholder="Ej: 12345678" />
            </label>
            <label>
              Email
              <input type="email" name="email" value={nuevoEmpleado.email} onChange={handleChange} placeholder="Ej: juan.perez@correo.com" />
            </label>
          </div>
          <div className="form-group">
            <label>
              Puesto
              <input type="text" name="puesto" value={nuevoEmpleado.puesto} onChange={handleChange} placeholder="Ej: Desarrollador" />
            </label>
            <label>
              Rol
              <input type="text" name="rol" value={nuevoEmpleado.rol} onChange={handleChange} placeholder="Ej: Admin" />
            </label>
          </div>
          <label>
            Teléfono
            <input type="text" name="telefono" value={nuevoEmpleado.telefono} onChange={handleChange} placeholder="Ej: +54 221 1234567" />
          </label>
          <div className="botones">
            <button className="boton-agregar" type="button" onClick={handleAgregar}>Agregar Empleado</button>
            <button className="boton-cerrar" type="button" onClick={onRequestClose}>Cerrar</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default AgregarEmpleado;