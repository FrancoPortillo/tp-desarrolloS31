import axios from "axios";


// Configuración base de Axios
const axiosInstance = axios.create({
    baseURL: 'http://localhost:5237', // Cambia esto a la URL de tu API
    headers: {
      'Content-Type': 'application/json',
    },
  });
  
  // Función para agregar un empleado
  export const agregarEmpleado = async (empleado) => {
    try {
      const response = await axiosInstance.post('/Empleado/Agregar', empleado);
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