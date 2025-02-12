import { useState, useEffect } from 'react';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import { Button, MenuItem } from '@mui/material';
import dayjs from 'dayjs';
import SendIcon from '@mui/icons-material/Send';
import { agregarPermisoAusencia } from '../../Utils/Axios';
import { BasicDatePicker } from '../../componentes/BasicDatePicker';

export const PedidoSolicitud = ({ idEmpleado, setPermission, handleClose, handleSuccess }) => {
  const [data, setData] = useState({
    idEmpleado: idEmpleado,
    tipo: '',
    detalles: '',
    fechaInicio: null,
    fechaFin: null,
    fechaSolicitado: dayjs(),
    estado: "Pendiente",
  });

  const [errors, setErrors] = useState({});
  const [isMounted, setIsMounted] = useState(false);
  const [hasSubmitted, setHasSubmitted] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setData((prevData) => ({
      ...prevData,
      [name]: value
    }));
    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: '',
    }));
  };

  const handleDateChange = (name, date) => {
    const dateValue = date && date.target ? date.target.value : date;
    const parsedDate = dayjs(dateValue);
    if (!parsedDate.isValid()) {
      console.error('Fecha invalida', date);
      return;
    }
    setData((prevData) => ({
      ...prevData,
      [name]: parsedDate.toISOString(),
    }));
  };

  const validate = () => {
    let tempErrors = {};
    if (!data.tipo) tempErrors.tipo = '*El campo es obligatorio';
    if (!data.detalles) tempErrors.detalles = '*El campo es obligatorio';

    setErrors(tempErrors);
    return Object.keys(tempErrors).length === 0;
  };

  useEffect(() => {
    setData((prevData) => ({
      ...prevData,
      fechaInicio: dayjs().toISOString(),
      fechaFin: dayjs().toISOString(),
      fechaSolicitado: dayjs(),
    }));
    setIsMounted(true);
  }, []);

  useEffect(() => {
    if (hasSubmitted) {
      validate();
    }
  }, [data]);

  const isFormComplete = () => {
    return data.tipo && data.detalles && data.fechaInicio && data.fechaFin && data.fechaSolicitado;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setHasSubmitted(true);
    if (!validate()) return;
    try {
      const response = await agregarPermisoAusencia(data);
      console.log("respuesta: ", response);
      if (response) {
        setPermission(response.data);
        handleSuccess(); // Llama a handleSuccess para cerrar el modal y mostrar la alerta de éxito
      } else {
        console.error('Error en la respuesta del servidor:', response);
      }
    } catch (error) {
      console.error('Error al enviar la solicitud:', error);
    }
  };

  return (
    <Box
      component="form"
      onSubmit={handleSubmit}
      sx={{
        display: 'flex',
        flexDirection: 'column',
        gap: 2,
        margin: '1%',
      }}
      noValidate
      autoComplete="off"
    >
      <TextField
        label="Tipo"
        select
        name="tipo"
        value={data.tipo || ''}
        variant="standard"
        onChange={handleChange}
        error={!!errors.tipo}
        helperText={errors.tipo}
      >
        <MenuItem value="Familiar">Familiar</MenuItem>
        <MenuItem value="Medico">Médica</MenuItem>
        <MenuItem value="Otro">Otro</MenuItem>
      </TextField>
      <TextField
        label="Detalles"
        variant="standard"
        name="detalles"
        value={data.detalles || ''}
        onChange={handleChange}
        error={!!errors.detalles}
        helperText={errors.detalles}
      />

      <BasicDatePicker
        label="Fecha de inicio"
        date={data.fechaInicio}
        onChange={(date) => handleDateChange('fechaInicio', date)}
      />
      <BasicDatePicker
        label="Fecha de finalización"
        date={data.fechaFin}
        onChange={(date) => handleDateChange('fechaFin', date)}
      />
      {errors.dateTime && (
        <span style={{ color: '#d32f2f', fontSize: '12px', padding: '0', margin: '0 0 20px 0', width: '100%', textAlign: 'center' }}>{errors.dateTime}</span>
      )}

      <Button sx={{ backgroundColor: "#5bbc5e", color: 'white', '&:hover': { backgroundColor: "#4caf50", } }} startIcon={<SendIcon />} type="submit" variant="contained" disabled={!isFormComplete()}>
        Enviar
      </Button>
    </Box>
  );
};

export default PedidoSolicitud;