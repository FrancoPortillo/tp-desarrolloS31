import { useState } from 'react';
import { registrarLlegadaTarde } from '../../Utils/Axios';
import './LlegadaTarde.css';

export const LlegadaTarde = ({ isOpen, onRequestClose, idEmpleado, fecha }) => {
  const [minutosTarde, setMinutosTarde] = useState('');
  const [motivo, setMotivo] = useState('');

  const handleRegistrar = async () => {
    const llegadaTarde = {
      idEmpleado: parseInt(idEmpleado, 10),
      fecha: new Date(fecha).toISOString(), // Convertir la fecha al formato ISO 8601 con hora
      minutosTarde: parseInt(minutosTarde, 10),
      motivo
    };

    try {
      await registrarLlegadaTarde(llegadaTarde);
      alert('Llegada tarde registrada con éxito');
      onRequestClose();
    } catch (error) {
      console.error('Error al registrar llegada tarde:', error);
      alert('Error al registrar llegada tarde');
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

  return (
    <div className={`popup ${isOpen ? 'show' : ''}`} onClick={handleClickOutside}>
      <div className="popup-content">
        <h3>Registrar Llegada Tarde</h3>
        <form className="llegada-tarde-form">
          <div className="form-group">
            <label className="titulo">
              Minutos Tarde
              <input type="number" name="minutosTarde" value={minutosTarde} onChange={handleChange} placeholder="Ej: 15" />
            </label>
          </div>
          <div className="form-group">
            <label className="titulo" >
              Motivo
              <textarea name="motivo" value={motivo} onChange={handleChange} placeholder="Ej: Tráfico" />
            </label>
          </div>
          <div className="botones">
            <button className="boton-registrar" type="button" onClick={handleRegistrar}>Registrar</button>
            <button className="boton-cerrar" type="button" onClick={onRequestClose}>Cerrar</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default LlegadaTarde;