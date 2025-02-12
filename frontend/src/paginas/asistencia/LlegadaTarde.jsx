import { useState } from 'react';
import { registrarLlegadaTarde } from '../../Utils/Axios';
import './LlegadaTarde.css';
import { Snackbar, Alert } from '@mui/material';

export const LlegadaTarde = ({ isOpen, onRequestClose, idEmpleado, fecha }) => {
  const [minutosTarde, setMinutosTarde] = useState('');
  const [motivo, setMotivo] = useState('');
  const [errors, setErrors] = useState({});
  const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'success' });

  const validateLlegadaTarde = () => {
    const newErrors = {};

    if (!minutosTarde) {
      newErrors.minutosTarde = "Los minutos tarde son obligatorios.";
    } else if (parseInt(minutosTarde, 10) <= 0) {
      newErrors.minutosTarde = "Los minutos tarde deben ser un número positivo.";
    }
    if (!motivo) {
      newErrors.motivo = "El motivo es obligatorio.";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleRegistrar = async () => {
    if (!validateLlegadaTarde()) {
      return;
    }

    const llegadaTarde = {
      idEmpleado: parseInt(idEmpleado, 10),
      fecha: new Date(fecha).toISOString(), // Convertir la fecha al formato ISO 8601 con hora
      minutosTarde: parseInt(minutosTarde, 10),
      motivo
    };

    try {
      await registrarLlegadaTarde(llegadaTarde);
      setSnackbar({ open: true, message: 'Llegada tarde registrada con éxito.', severity: 'success' });
      onRequestClose();
    } catch (error) {
      console.error('Error al registrar llegada tarde:', error);
      setSnackbar({ open: true, message: 'Error al registrar llegada tarde.', severity: 'error' });
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name === 'minutosTarde') {
      setMinutosTarde(value);
    } else if (name === 'motivo') {
      setMotivo(value);
    }
  };

  const handleClickOutside = (e) => {
    if (e.target.classList.contains('popup')) {
      onRequestClose();
    }
  };

  const handleCloseSnackbar = () => {
    setSnackbar({ ...snackbar, open: false });
  };

  return (
    <div className={`popup ${isOpen ? 'show' : ''}`} onClick={handleClickOutside}>
      <div className="popup-content">
        <h3>Registrar Llegada Tarde</h3>
        <form className="llegada-tarde-form">
          <div className="form-group">
            <label className="titulo">
              Minutos Tarde
              <input type="number" name="minutosTarde" value={minutosTarde} onChange={handleChange} placeholder="Ej: 15" />
              {errors.minutosTarde && <span className="error">{errors.minutosTarde}</span>}
            </label>
          </div>
          <div className="form-group">
            <label className="titulo">
              Motivo
              <textarea name="motivo" value={motivo} onChange={handleChange} placeholder="Ej: Tráfico" />
              {errors.motivo && <span className="error">{errors.motivo}</span>}
            </label>
          </div>
          <div className="botones">
            <button className="boton-registrar" type="button" onClick={handleRegistrar}>Registrar</button>
            <button className="boton-cerrar" type="button" onClick={onRequestClose}>Cerrar</button>
          </div>
        </form>
        <Snackbar open={snackbar.open} autoHideDuration={6000} onClose={handleCloseSnackbar}>
          <Alert onClose={handleCloseSnackbar} severity={snackbar.severity} sx={{ width: '100%' }}>
            {snackbar.message}
          </Alert>
        </Snackbar>
      </div>
    </div>
  );
};

export default LlegadaTarde;