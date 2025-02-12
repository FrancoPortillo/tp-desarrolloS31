import axios from "axios";

// Configuración base de Axios
const axiosInstance = axios.create({
  baseURL: 'http://localhost:5237', // Cambia esto a la URL de tu API
  headers: {
    'Content-Type': 'application/json',
  },
});
export const subirFotoPerfil = async (id, formData) => {
  try {
      const response = await axios.post(`${import.meta.env.VITE_API_URL}/Empleado/SubirFotoPerfil/${id}`, formData, {
          headers: {
              'Content-Type': 'multipart/form-data'
          }
      });
      return response.data;
  } catch (error) {
      console.error('Error al subir documento:', error);
      throw error;
  }
};

export const getImage = async (idEmpleado) => {
    const response = await axios.get(`${import.meta.env.VITE_API_URL}/Empleado/ObtenerFotoPerfil/${idEmpleado}`,{
        responseType: 'blob'
    });
    return response.data;
};
// Función para agregar un empleado
export const agregarEmpleado = async (empleado) => {
  try {
    const response = await axiosInstance.post('/Empleado/Agregar', empleado);
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error('Error al agregar empleado:', error);
    throw error;
  }
};

// Función para modificar un empleado
export const modificarEmpleado = async (empleado) => {
  try {
    const response = await axiosInstance.put('/Empleado/Modificar', empleado);
    return response.data;
  } catch (error) {
    console.error('Error al modificar empleado:', error);
    throw error;
  }
};

// Función para eliminar un empleado
export const eliminarEmpleado = async (id) => {
  try {
    const response = await axiosInstance.delete(`/Empleado/Eliminar/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error al eliminar empleado:', error);
    throw error;
  }
};

// Función para obtener todos los empleados
export const obtenerEmpleados = async () => {
  try {
    const response = await axiosInstance.get('/Empleado/Obtener');
    return response.data;
  } catch (error) {
    console.error('Error al obtener empleados:', error);
    throw error;
  }
};
export const obtenerEmpleadosEliminados = async () => {
  try {
    const response = await axiosInstance.get('/Empleado/ObtenerEliminados');
    return response.data;
  } catch (error) {
    console.error('Error al obtener empleados:', error);
    throw error;
  }
};

// Función para obtener un empleado por ID
export const obtenerEmpleadoPorId = async (token, id) => {
  try {
    const response = await axiosInstance.get(`/Empleado/ObtenerIndividual/${id}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error al obtener empleado por ID:', error);
    throw error;
  }
};

// Función para obtener un empleado individual
export const obtenerEmpleadoIndividual = async (id) => {
  try {
    const response = await axiosInstance.get(`/Empleado/ObtenerIndividual/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error al obtener empleado individual:', error);
    throw error;
  }
};

export const obtenerEmpleadoPorEmail = async (token, email) => {
  try {
    const response = await axiosInstance.get(`/Empleado/ObtenerPorEmail/${email}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error al obtener empleado por correo electrónico:', error);
    throw error;
  }
};

// Función para obtener las inasistencias de un empleado por ID
export const obtenerInasistencias = async (idEmpleado) => {
  try {
    const response = await axiosInstance.get(`/Inasistencia/ObtenerInasistencias/${idEmpleado}`);
    return response.data;
  } catch (error) {
    console.error('Error al obtener inasistencias:', error);
    throw error;
  }
};

// Función para registrar asistencia
export const registrarAsistencia = async (asistencias) => {
  try {
    const response = await axiosInstance.post('/Asistencia/Registrar', asistencias);
    return response.data;
  } catch (error) {
    console.error('Error al registrar asistencia:', error);
    throw error;
  }
};

export const registrarInasistencia = async (inasistencia) => {
  try {
    const response = await axiosInstance.post('/Inasistencia/Agregar', inasistencia);
    return response.data;
  } catch (error) {
    console.error('Error al registrar la inasistencia:', error);
    throw error;
  }
};
// Función para registrar llegada tarde
export const registrarLlegadaTarde = async (llegadaTarde) => {
  try {
    const response = await axiosInstance.post('/LlegadaTarde/Agregar', llegadaTarde);
    return response.data;
  } catch (error) {
    console.error('Error al registrar llegada tarde:', error);
    throw error;
  }
};

export const obtenerllegadasTarde = async (idEmpleado) => {
  try {
    const response = await axiosInstance.get(`/LlegadaTarde/ObtenerLlegadasTarde/${idEmpleado}`);
    return response.data;
  } catch (error) {
    console.error('Error al obtener las llegadas tarde:', error);
    throw error;
  }
};

// Función para obtener todas las solicitudes de permisos de ausencia
export const obtenerPermisosAusencia = async () => {
  try {
    const response = await axiosInstance.get('/PermisoAusencia/Obtener');
    return response.data;
  } catch (error) {
    console.error('Error al obtener permisos de ausencia:', error);
    throw error;
  }
};

// Función para obtener las solicitudes de permisos de ausencia de un empleado específico
export const obtenerPermisosAusenciaPorEmpleado = async (idEmpleado) => {
  try {
    const response = await axiosInstance.get(`/PermisoAusencia/ObtenerPorEmpleado/${idEmpleado}`);
    return response.data;
  } catch (error) {
    console.error('Error al obtener permisos de ausencia por empleado:', error);
    throw error;
  }
};

// Función para agregar una nueva solicitud de permiso de ausencia
export const agregarPermisoAusencia = async (permiso) => {
  try {
    const response = await axiosInstance.post('/PermisoAusencia/Agregar', permiso);
    return response.data;
  } catch (error) {
    console.error('Error al agregar permiso de ausencia:', error);
    throw error;
  }
};

// Función para modificar una solicitud de permiso de ausencia
export const modificarPermisoAusencia = async (permiso) => {
  try {
    const response = await axiosInstance.put(`/PermisoAusencia/Modificar`, permiso);
    return response.data;
  } catch (error) {
    console.error('Error al modificar permiso de ausencia:', error);
    throw error;
  }
};
export const modificarEstadoPermiso = async (id, estado) => {
  try {
    const response = await axiosInstance.patch(`/PermisoAusencia/Modificar/${estado}/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error al modificar permiso de ausencia:', error);
    throw error;
  }
};
// Función para eliminar una solicitud de permiso de ausencia
export const eliminarPermisoAusencia = async (id) => {
  try {
    const response = await axiosInstance.delete(`/PermisoAusencia/Eliminar/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error al eliminar permiso de ausencia:', error);
    throw error;
  }
};
export const eliminarVacaciones = async (id) => {
  try {
    const response = await axiosInstance.delete(`/Vacaciones/Eliminar/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error al eliminar permiso de ausencia:', error);
    throw error;
  }
};

export const obtenerVacaciones = async () => {
  try {
    const response = await axiosInstance.get('/Vacaciones/calendario');
    return response.data;
  } catch (error) {
    console.error('Error al obtener vacaciones:', error);
    throw error;
  }
};

export const modificarVacaciones = async (id, estado) => {
  try {
    const response = await axiosInstance.patch(`/Vacaciones/Modificar/${estado}/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error al modificar vacaciones:', error);
    throw error;
  }
};
export const obtenerVacacionesPorEmpleado = async (idEmpleado) => {
  try {
    const response = await axiosInstance.get(`/Vacaciones/ObtenerPorEmpleado/${idEmpleado}`);
    return response.data;
  } catch (error) {
    console.error('Error al obtener vacaciones por empleado:', error);
    throw error;
  }
};
export const agregarVacaciones = async (vacacion) => {
  try {
    const response = await axiosInstance.post('/Vacaciones/Agregar', vacacion);
    return response.data;
  } catch (error) {
    console.error('Error al agregar vacaciones:', error);
    throw error;
  }
};
export const agregarDocumentacion = async (documento) => {
  try {
    const response = await axiosInstance.post('/Documentacion/Agregar', documento);
    return response.data;
  } catch (error) {
    console.error('Error al agregar documento:', error);
    throw error;
  }
};
export const subirDocumentacion = async (idDocumento, archivo) => {
  try {
    const response = await axiosInstance.post('/Documentacion/SubirArchivo', idDocumento, archivo);
    return response.data;
  } catch (error) {
    console.error('Error al subir documento:', error);
    throw error;
  }
};
export const descargarDocumentacion = async (id) => {
  try {
    const response = await axiosInstance.get(`/Documentacion/Descargar/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error al descargar documento:', error);
    throw error;
  }
};
