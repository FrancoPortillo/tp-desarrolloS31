import axios from "axios";

const BASE_URL = "http://localhost:5237/api";

export const ObtenerEmpleados = async () => {
    
    const config = {
        method: `get`,
        url: BASE_URL + `/Empleado/Obtener`,
        headers: { 
            'Access-Control-Allow-Origin': '*', 
            Authorization: `Bearer ${sessionStorage.getItem('jwt')}`
         }
    } 
    return await axios(config);
}