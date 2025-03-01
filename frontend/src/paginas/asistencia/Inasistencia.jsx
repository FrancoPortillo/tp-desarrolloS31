import { useState, useEffect } from 'react';
import { registrarInasistencia } from '../../Utils/Axios';
import './LlegadaTarde.css';
import { TextField, MenuItem, Button, Box, Snackbar, Alert, Select, InputLabel, FormControl } from '@mui/material';

export const Inasistencia = ({ isOpen, onRequestClose, idEmpleado, fecha }) => {
  const [detalles, setDetalles] = useState('');
  const [tipo, setTipo] = useState(''); // Estado para el tipo de inasistencia
  const [errors, setErrors] = useState({});
  const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'success' });

  useEffect(() => {
    if (!isOpen) {
      setDetalles('');
      setTipo('');
      setErrors({});
    }
  }, [isOpen]);

  const validateInasistencia = () => {
    const newErrors = {};
    if (!detalles) newErrors.detalles = 'El detalle es obligatorio';
    if (!tipo) newErrors.tipo = 'El tipo es obligatorio';

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleRegistrar = async () => {
    if (!validateInasistencia()) {
      return;
    }

    const inasistencia = {
      idEmpleado: idEmpleado,
      fecha: new Date(fecha).toISOString(),
      detalles,
      tipo,
    };

    try {
      await registrarInasistencia(inasistencia);
      setSnackbar({ open: true, message: 'Inasistencia registrada con éxito.', severity: 'success' });
      onRequestClose();
    } catch (error) {
      console.error('Error al registrar la inasistencia:', error);
      setSnackbar({ open: true, message: 'Error al registrar la inasistencia.', severity: 'error' });
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name === 'detalles') {
      setDetalles(value);
    } else if (name === 'tipo') {
      setTipo(value);
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
            <h3>Registrar Inasistencia</h3>
            <form className="llegada-tarde-form">
                <div className="form-group">
                    <label className="titulo">
                        Detalles
                        <textarea className="detalle" name="detalles" value={detalles} onChange={handleChange} placeholder="Ej: Urgencia médica." />
                        {errors.detalles && <span className="error">{errors.detalles}</span>}
                    </label>
                </div>
                <div className="form-group">
                    <FormControl fullWidth variant="outlined" error={!!errors.tipo}>
                        <InputLabel>Tipo</InputLabel>
                        <Select
                            label="Tipo"
                            name="tipo"
                            value={tipo}
                            onChange={handleChange}
                        >
                            <MenuItem value="Familiar">Familiar</MenuItem>
                            <MenuItem value="Medico">Médica</MenuItem>
                            <MenuItem value="Otro">Otro</MenuItem>
                        </Select>
                        {errors.tipo && <span className="error">{errors.tipo}</span>}
                    </FormControl>
                </div>
                <div className="botones">
                    <Button variant="contained" color="primary" onClick={handleRegistrar}>
                        Registrar
                    </Button>
                    <Button variant="outlined" color="secondary" onClick={onRequestClose}>
                        Cerrar
                    </Button>
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

export default Inasistencia;