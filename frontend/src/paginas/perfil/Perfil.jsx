import { useEffect, useState } from 'react';
import { useAuth0 } from '@auth0/auth0-react';
import { useParams } from 'react-router-dom';
import { obtenerEmpleadoIndividual, obtenerEmpleadoPorEmail, obtenerInasistencias, obtenerllegadasTarde } from '../../Utils/Axios';
import Swal from 'sweetalert2';
import './Perfil.css';
import Boton from '../../componentes/Boton/Boton';
import AgregarEmpleado from '../empleados/AgregarEmpleado';
import defaultAvatar from '../../assets/default-avatar.jpg';

export const Perfil = () => {
    const { id } = useParams();
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();
    const [employeeData, setEmployeeData] = useState(null);
    const [inasistencias, setInasistencias] = useState(0);
    const [llegadasTarde, setLlegadasTarde] = useState(0);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [admin, setAdmin] = useState();

    useEffect(() => {
        const fetchEmployeeData = async () => {
            if (user) {
                try {
                    const token = await getAccessTokenSilently();
                    const adminData = await obtenerEmpleadoPorEmail(token, user.email);
                    const data = await obtenerEmpleadoIndividual(id);
                    setAdmin(adminData);
                    setEmployeeData(data);
                    setLlegadasTarde(await obtenerllegadasTarde(id));
                    setInasistencias(await obtenerInasistencias(id));
                } catch (error) {
                    console.error('Error al obtener los datos del empleado:', error);
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Error al obtener los datos del empleado.',
                        confirmButtonText: 'OK'
                    });
                }
            }
        };
        fetchEmployeeData();
    }, [isAuthenticated, getAccessTokenSilently, id, user]);

    if (!employeeData) {
        return <div className="loading">Cargando...</div>;
    }

    return (
        <div className="perfil-container">
            <div className="perfil-header">
                <img src={employeeData.fotoPerfil ? `data:image/jpeg;base64,${employeeData.fotoPerfil}` : defaultAvatar} alt="Perfil" className="perfil-avatar" />
                <h1>{employeeData.nombre} {employeeData.apellido}</h1>
                <h2>{employeeData.puesto}</h2>
            </div>
            <div className="perfil-info">
                <div className="perfil-column">
                    <div className="perfil-card">
                        <h3>Información Personal</h3>
                        <p><strong>Teléfono:</strong> {employeeData.telefono}</p>
                        <p><strong>Legajo:</strong> {employeeData.legajo}</p>
                        <p><strong>DNI:</strong> {employeeData.dni}</p>
                        <p><strong>Edad:</strong> {employeeData.edad}</p>
                        <p><strong>Correo:</strong> {employeeData.email}</p>
                    </div>
                </div>
                <div className="perfil-column">
                    <div className="perfil-card">
                        <h3>Estadísticas Último Mes</h3>
                        <p><strong>Inasistencias:</strong> {inasistencias}</p>
                        <div className="progress-bar-container">
                            <div className="progress-bar" style={{ width: `${inasistencias * 33}%` }}></div>
                        </div>
                        <p><strong>Llegadas Tarde:</strong> {llegadasTarde}</p>
                        <div className="progress-bar-container">
                            <div className="progress-bar" style={{ width: `${llegadasTarde * 33}%` }}></div>
                        </div>
                        <p><strong>Días de Vacaciones:</strong> {employeeData.diasVacaciones}</p>
                    </div>
                </div>
            </div>
            {admin?.rol === 'admin' && (
                <div className='boton-modificar'>
                    <Boton texto="Modificar Datos" onClick={() => setIsModalOpen(true)} />
                </div>
            )}
            <AgregarEmpleado 
                isOpen={isModalOpen} 
                onRequestClose={() => setIsModalOpen(false)} 
                onEmpleadoAgregado={setEmployeeData} 
                empleado={employeeData} 
                isEditMode={true} 
            />
        </div>
    );
};

export default Perfil;