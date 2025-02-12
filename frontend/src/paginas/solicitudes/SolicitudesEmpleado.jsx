import { useAuth0 } from '@auth0/auth0-react';
import React, { useEffect, useState } from 'react';
import CloudUpload from '@mui/icons-material/CloudUpload';
import CloudDownload from '@mui/icons-material/CloudDownload';
import AddIcon from '@mui/icons-material/Add';
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import Button from '@mui/material/Button';
import { eliminarPermisoAusencia, obtenerEmpleadoPorEmail, obtenerPermisosAusenciaPorEmpleado, obtenerVacacionesPorEmpleado } from '../../Utils/Axios';
import Boton from '../../componentes/Boton/Boton';
import { PedidoSolicitud } from './PedidoSolicitud';
import { Box, Modal } from '@mui/material';
import VacacionesSolicitud from '../vacaciones/VacacionesSolicitud';

const style = {
  position: 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  bgcolor: 'background.paper',
  border: '1px solid #000',
  boxShadow: 24,
  p: 4,
  minWidth: '60%',
  maxWidth: '90%',
};

export const SolicitudesEmpleados = () => {
  const { user, getAccessTokenSilently } = useAuth0();
  const [permisos, setPermisos] = useState([]);
  const [idEmpleado, setIdEmpleado] = useState();
  const [open, setOpen] = useState(false);
  const [openVacaciones, setOpenVacaciones] = useState(false); 
  const [openDocumentationModal, setOpenDocumentationModal] = useState(false);
  const [selectedPermissionId, setSelectedPermissionId] = useState(null);
  const [snackbar, setSnackbar] = useState({ open: false, message: "", severity: "" });

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const handleOpenVacaciones = () => setOpenVacaciones(true);
  const handleCloseVacaciones = () => setOpenVacaciones(false);

  const handleSuccess = () => {
    setOpen(false);
    setSnackbar({ open: true, message: "Solicitud creada con éxito", severity: "success" });
    callApi(); // Recargar la tabla
  };

  useEffect(() => {
    callApi();
  }, []);

  const callApi = async () => {
    try {
      const token = await getAccessTokenSilently();
      const empleado = await obtenerEmpleadoPorEmail(token, user.email);
      setIdEmpleado(empleado.id);
      console.log("ID, ", empleado.id);
      const permisos = await obtenerPermisosAusenciaPorEmpleado(empleado.id);
      const vacaciones = await obtenerVacacionesPorEmpleado(empleado.id);
      sessionStorage.setItem('empleadoId', empleado.id);

      const permisosConTipo = permisos.map(permiso => ({ ...permiso, tipoSolicitud: 'Permiso' }));
      const vacacionesConTipo = vacaciones.map(vacacion => ({ ...vacacion, tipoSolicitud: 'Vacaciones' }));

      setPermisos([...permisosConTipo, ...vacacionesConTipo]);
    } catch (error) {
      console.error('Error al obtener datos:', error);
    }
  };

  const deleteRow = async (id) => {
    try {
      await eliminarPermisoAusencia(id);
      setSnackbar({ open: true, message: "Solicitud eliminada con éxito", severity: "success" });
      callApi();
    } catch (error) {
      setSnackbar({ open: true, message: "Error al eliminar la solicitud", severity: "error" });
    }
  };

  const handleVacacionAgregada = (vacacion) => {
    setPermisos([...permisos, { ...vacacion, tipoSolicitud: 'Vacaciones' }]);
    setOpenVacaciones(false);
  };

  return (
    <>
      <div className="empleados-container">
        <table className="empleados-table">
          <thead>
            <tr>
              <th>Tipo de Permiso</th>
              <th>Motivo</th>
              <th>Detalles</th>
              <th>Fecha Solicitado</th>
              <th>Fecha Inicio</th>
              <th>Fecha Fin</th>
              <th>Estado</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody className="fondo">
            {permisos.map((permiso) => (
              <tr key={permiso.id}>
                <td>{permiso.tipoSolicitud}</td>
                <td>{permiso.tipoSolicitud === 'Vacaciones' ? 'xxxxxxxxxxxxxxxx' : permiso.tipo}</td>
                <td>{permiso.tipoSolicitud === 'Vacaciones' ? `${permiso.dias} Días` : permiso.detalles}</td>
                <td>{new Date(permiso.fechaSolicitado).toLocaleDateString()}</td>
                <td>{new Date(permiso.fechaInicio).toLocaleDateString()}</td>
                <td>{new Date(permiso.fechaFin).toLocaleDateString()}</td>
                <td>{permiso.estado}</td>
                <td>
                  {permiso.documentaciones ? (
                    <Button variant="outlined" color="primary">
                      <CloudDownload fontSize="small" />
                    </Button>
                  ) : (
                    <Button variant="outlined" color="primary">
                      <CloudUpload fontSize="small" />
                    </Button>
                  )}
                  {permiso.estado === "Pendiente" && 
                    <Button variant="outlined" color="error" onClick={() => deleteRow(permiso.id)}>
                      <DeleteOutlineIcon fontSize="small" />
                    </Button>
                  }
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        <Button
          startIcon={<AddIcon />}
          variant="contained"
          sx={{
            backgroundColor: "#5bbc5e",
            color: 'white',
            marginTop: "2%",
            marginLeft: "1%",
            '&:hover': {
              backgroundColor: "#4caf50",
            },
          }}
          onClick={handleOpen}
        >
          Solicitar Permiso
        </Button>
        <Button
          startIcon={<AddIcon />}
          variant="contained"
          sx={{
            backgroundColor: "#5bbc5e",
            color: 'white',
            marginTop: "2%",
            marginLeft: "1%",
            '&:hover': {
              backgroundColor: "#4caf50",
            },
          }}
          onClick={handleOpenVacaciones}
        >
          Solicitar Vacaciones
        </Button>
        <Modal
          open={open}
          onClose={handleClose}
          aria-labelledby="modal-modal-title"
          aria-describedby="modal-modal-description"
        >
          <Box sx={style}>
            <PedidoSolicitud idEmpleado={idEmpleado} setPermission={setSelectedPermissionId} handleClose={handleClose} handleSuccess={handleSuccess} />
          </Box>
        </Modal>
        <Modal
          open={openVacaciones}
          onClose={handleCloseVacaciones}
          aria-labelledby="modal-modal-title"
          aria-describedby="modal-modal-description"
        >
          <Box sx={style}>
          <VacacionesSolicitud isOpen={openVacaciones} onRequestClose={handleCloseVacaciones} onVacacionAgregada={handleVacacionAgregada} idEmpleado={idEmpleado}/>
          </Box>
        </Modal>
      </div>
    </>
  );
};

export default SolicitudesEmpleados;