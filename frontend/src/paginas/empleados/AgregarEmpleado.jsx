import { useState, useEffect } from 'react';
import { agregarEmpleado, modificarEmpleado, subirFotoPerfil } from '../../Utils/Axios';
import './AgregarEmpleado.css';
import Boton from '../../componentes/Boton/Boton';
import Swal from 'sweetalert2';

export const AgregarEmpleado = ({ isOpen, onRequestClose, onEmpleadoAgregado, empleado, isEditMode }) => {
  const [nuevoEmpleado, setNuevoEmpleado] = useState({
    id: 0,
    nombre: '',
    apellido: '',
    legajo: 0,
    edad: 0,
    dni: '',
    email: '',
    puesto: '',
    rol: 'user',
    telefono: '',
    empresa: 0,
  });
  const [errors, setErrors] = useState({});
  const [file, setFile] = useState(null);

  useEffect(() => {
    if (isEditMode && empleado) {
      setNuevoEmpleado({
        id: empleado.id || 0,
        nombre: empleado.nombre || '',
        apellido: empleado.apellido || '',
        legajo: empleado.legajo || 0,
        edad: empleado.edad || 0,
        dni: empleado.dni || '',
        email: empleado.email || '',
        puesto: empleado.puesto || '',
        telefono: empleado.telefono || '',
        empresa: empleado.empresa || 0,
      });
    }
  }, [isEditMode, empleado]);

  const validateEmpleado = () => {
    const newErrors = {};

    if (!nuevoEmpleado.nombre) {
      newErrors.nombre = "El nombre es obligatorio.";
    }
    if (!nuevoEmpleado.apellido) {
      newErrors.apellido = "El apellido es obligatorio.";
    }
    if (nuevoEmpleado.legajo <= 0) {
      newErrors.legajo = "El legajo debe ser un número positivo.";
    }
    if (nuevoEmpleado.edad < 18 || nuevoEmpleado.edad > 65) {
      newErrors.edad = "La edad debe estar entre 18 y 65 años.";
    }
    if (!nuevoEmpleado.dni.match(/^\d{8}$/)) {
      newErrors.dni = "El DNI debe tener 8 dígitos.";
    }
    if (!nuevoEmpleado.email) {
      newErrors.email = "El email es obligatorio.";
    } else if (!nuevoEmpleado.email.match(/^[^\s@]+@[^\s@]+\.[^\s@]+$/)) {
      newErrors.email = "El email no tiene un formato válido.";
    }
    if (!nuevoEmpleado.puesto) {
      newErrors.puesto = "El puesto es obligatorio.";
    }
    if (!nuevoEmpleado.telefono.match(/^\+?\d{1,3}?[-.\s]?\(?\d{1,4}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,9}$/)) {
      newErrors.telefono = "El teléfono debe ser un número válido.";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleGuardar = async (e) => {
    e.preventDefault();
    if (!validateEmpleado()) {
        return;
    }

    try {
        let empleadoData;
        if (isEditMode) {
            empleadoData = await modificarEmpleado(nuevoEmpleado);
            onEmpleadoAgregado(nuevoEmpleado);
          //   if (file) {
          //     const formData = new FormData();
          //     formData.append("fotoPerfil", file);
          //     await subirFotoPerfil(empleadoData.id, formData);
          // }
        } else {
            empleadoData = await agregarEmpleado(nuevoEmpleado);
            setNuevoEmpleado(empleadoData);
            onEmpleadoAgregado(empleadoData);
        }
        onRequestClose();
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Empleado guardado correctamente.',
            confirmButtonText: 'OK'
        });
    } catch (error) {
        console.error('Error al guardar empleado:', error);
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Error al guardar el empleado.',
            confirmButtonText: 'OK'
        });
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setNuevoEmpleado({ ...nuevoEmpleado, [name]: name === 'legajo' || name === 'edad' ? parseInt(value, 10) : value });
  };

  const handleFileChange = (event) => {
    const file = event.target.files[0];
    if (file) {
        if (file.size > 5 * 1024 * 1024) {
            setErrors((prevErrors) => ({
                ...prevErrors,
                file: 'El archivo no debe pesar más de 5MB',
            }));
            setFile(null);
        } else {
            setFile(file);
            setErrors((prevErrors) => ({
                ...prevErrors,
                file: '',
            }));
        }
    }
  };

  const handleClickOutside = (e) => {
    if (e.target.classList.contains('popup')) {
      onRequestClose();
    }
  };

  return (
    <div className={`popup ${isOpen ? 'show' : ''}`} onClick={handleClickOutside}>
      <div className="popup-content">
        <h3>{isEditMode ? 'Modificar Empleado' : 'Alta de Empleado'}</h3>
        <form className="agregar-empleado-form" onSubmit={handleGuardar}>
          <div className="form-group">
            <label>
              Nombre
              <input type="text" name="nombre" value={nuevoEmpleado.nombre} onChange={handleChange} placeholder="Ej: Juan" />
              {errors.nombre && <span className="error">{errors.nombre}</span>}
            </label>
            <label>
              Apellido
              <input type="text" name="apellido" value={nuevoEmpleado.apellido} onChange={handleChange} placeholder="Ej: Pérez" />
              {errors.apellido && <span className="error">{errors.apellido}</span>}
            </label>
          </div>
          <div className="form-group">
            <label>
              Legajo
              <input type="number" name="legajo" value={nuevoEmpleado.legajo} onChange={handleChange} placeholder="Ej: 12345" />
              {errors.legajo && <span className="error">{errors.legajo}</span>}
            </label>
            <label>
              Edad
              <input type="number" name="edad" value={nuevoEmpleado.edad} onChange={handleChange} placeholder="Ej: 30" />
              {errors.edad && <span className="error">{errors.edad}</span>}
            </label>
          </div>
          <div className="form-group">
            <label>
              DNI
              <input type="text" name="dni" value={nuevoEmpleado.dni} onChange={handleChange} placeholder="Ej: 12345678" />
              {errors.dni && <span className="error">{errors.dni}</span>}
            </label>
            <label>
              Email
              <input type="email" name="email" value={nuevoEmpleado.email} onChange={handleChange} placeholder="Ej: juan.perez@correo.com" />
              {errors.email && <span className="error">{errors.email}</span>}
            </label>
          </div>
          <div className="form-group">
            <label>
              Puesto
              <input type="text" name="puesto" value={nuevoEmpleado.puesto} onChange={handleChange} placeholder="Ej: Desarrollador" />
              {errors.puesto && <span className="error">{errors.puesto}</span>}
            </label>
            <label>
              Rol
              <input type="text" name="rol" value={nuevoEmpleado.rol} onChange={handleChange} placeholder="Ej: Admin" />
              {errors.rol && <span className="error">{errors.rol}</span>}
            </label>
          </div>
          <label>
            Teléfono
            <input type="text" name="telefono" value={nuevoEmpleado.telefono} onChange={handleChange} placeholder="Ej: +54 221 1234567" />
            {errors.telefono && <span className="error">{errors.telefono}</span>}
          </label>
          <label>
            Foto de Perfil
            <input type="file" name="fotoPerfil" onChange={handleFileChange} />
          </label>
          <div className="botones">
            <Boton texto={isEditMode ? 'Modificar' : 'Guardar'} type="submit" />
            <Boton texto="Cerrar" onClick={onRequestClose} />
          </div>
        </form>
      </div>
    </div>
  );
};

export default AgregarEmpleado;