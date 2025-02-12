import { useState, useEffect } from 'react';
import { useAuth0 } from '@auth0/auth0-react';
import { modificarEstadoPermiso, modificarPermisoAusencia, modificarVacaciones, obtenerEmpleados, obtenerPermisosAusencia, obtenerVacaciones } from '../../Utils/Axios';
import { Alert, Box, Button, Modal, Snackbar } from '@mui/material';
import { DataGrid, GridToolbar } from "@mui/x-data-grid";
import CheckRoundedIcon from '@mui/icons-material/CheckRounded';
import CloseRoundedIcon from '@mui/icons-material/CloseRounded';
import { ConfirmDialog } from '../../componentes/ConfirmDialog';
import CalendarioVacaciones from '../vacaciones/CalendarioVacaciones';
const SolicitudesAdmin = () => {
  const { getAccessTokenSilently } = useAuth0();
  const [solicitudes, setSolicitudes] = useState([]);
  const [ vacaciones, setVacaciones ] = useState([]);
  const [ empleados, setEmpleados] = useState([]);
  const [ todas, setTodas ] = useState([]);

  const [open, setOpen] = useState(false);
  const [openConfirmDialog, setOpenConfirmDialog] = useState(false);
  const [selectedPermission, setSelectedPermission] = useState(null);
  const [selectedAction, setSelectedAction] = useState(null);
  const [snackbar, setSnackbar] = useState({ open: false, message: "", severity: "" });
  
  const callApi = async () => {
    try {
      const token = await getAccessTokenSilently();
      const respuesta = await obtenerPermisosAusencia(token);
      const resp = await obtenerVacaciones(token);
      const response = await obtenerEmpleados(token);

      const solicitudesConNombre = respuesta.map(solicitud => {
        const empleado = response.find(emp => emp.id === solicitud.idEmpleado);
        return { ...solicitud, nombre: empleado ? `${empleado.apellido} ${empleado.nombre}` : 'Desconocido', tipoSolicitud: 'Permiso' };
      });
      const vacacionesConNombre = resp.map(vacacion => {
        const empleado = response.find(emp => emp.id === vacacion.idEmpleado);
        return { ...vacacion, nombre: empleado ? `${empleado.apellido} ${empleado.nombre}` : 'Desconocido', tipoSolicitud: 'Vacaciones' };
      });
      setSolicitudes(solicitudesConNombre);
      setVacaciones(vacacionesConNombre);
      setEmpleados(response);
      setTodas([...solicitudesConNombre, ...vacacionesConNombre]);
    } catch (error) {
      console.error('Error al obtener datos:', error);
    }
  };
  useEffect(() => {
    callApi();

  }, []);

  const handleOpen = (permission) => {
    setSelectedPermission(permission);
    setOpen(true);
  };
  const handleClose = () => setOpen(false);

  const handleOpenConfirmDialog = (permission, action) => {
    setSelectedPermission(permission);
    setSelectedAction(action);
    setOpenConfirmDialog(true);
  };

  const handleCloseConfirmDialog = () => setOpenConfirmDialog(false);

  const handleConfirmAction = async () => {
    if (!selectedAction) return; 
    try {
        if(selectedPermission.tipoSolicitud === 'Permiso'){
          const { tipoSolicitud, uniqueKey, ...rest } = selectedPermission; 
          await modificarEstadoPermiso(rest.id, selectedAction);
        }else{
          const { tipoSolicitud, uniqueKey, ...rest } = selectedPermission; 
          await modificarVacaciones(rest.id, selectedAction);
        }
        setSnackbar({ open: true, message: `Solicitud ${selectedAction.toLowerCase()} con éxito`, severity: "success" });
        callApi();
    } catch (error) {
        setSnackbar({ open: true, message: `Error al ${selectedAction.toLowerCase()} la solicitud`, severity: "error" });
    } finally {
        setOpenConfirmDialog(false);
    }
};

    const COLUMNS = [
      { field: "nombre", headerName: "Empleado", width: 145 },
      
      { field: "tipoSolicitud", headerName: "Tipo", width: 100 },
      {
        field: "fechaInicio",
        headerName: "Fecha de inicio",
        width: 150,
        type: "date",
        valueFormatter: (params) => new Date(params).toLocaleDateString(),
      },
      {
        field: "fechaFin",
        headerName: "Fecha de finalizacion",
        width: 150,
        type: "date",
        valueFormatter: (params) => new Date(params).toLocaleDateString(),
      },
      { field: "estado", headerName: "Estado", width: 135 },
      {
        field: "approve",
        headerName: "Aprobar",
        width: 70,
        renderCell: (params) => (
          <Button
            size="small"
            onClick={() => handleOpenConfirmDialog(params.row, "Aprobado")}
            sx={{ color: 'green' }}
          >
            <CheckRoundedIcon />
          </Button>
        ),
      },
      {
        field: "reject",
        headerName: "Rechazar",
        width: 80,
        renderCell: (params) => (
          <Button
            size="small"
            onClick={() => handleOpenConfirmDialog(params.row, "Rechazado")}
            sx={{ color: 'red' }}
          >
            <CloseRoundedIcon />
          </Button>
        ),
      },
      // {
      //   field: "documentation",
      //   headerName: "Documentacion",
      //   width: 150,
      //   renderCell: (params) => (
      //     <Button
      //       size="small"
      //       onClick={() => params.row.documentation && downloadFile(params.row.documentation.id)}
      //       sx={{ color: params.row.documentation ? 'blue' : 'gray' }}
      //       disabled={!params.row.documentation}
      //     >
      //     <DownloadRoundedIcon />
      //     </Button>
      //   ),
      // },
      // {
      //   field: "details",
      //   headerName: "Detalles",
      //   width: 150,
      //   renderCell: (params) => (
      //     <Button variant="outlined" size="small" onClick={() => handleOpen(params.row)}>
      //       Ver Detalles
      //     </Button>
      //   ),
      // },
    ];

  return (
    <>
    <div style={{display:'flex', gap: '10px', margin: '20px'}}>
    <CalendarioVacaciones vacaciones={vacaciones} permisos={solicitudes} empleados={empleados} fullWidth={false} />
      <DataGrid
        columns={COLUMNS}
        rows={todas.map((solicitud) => ({ ...solicitud, uniqueKey: `${solicitud.tipoSolicitud}-${solicitud.id}` }))}
        getRowId={(row) => row.uniqueKey}
        disableColumnSelector
        disableDensitySelector
        slots={{ toolbar: GridToolbar }}
        slotProps={{
          toolbar: {
            showQuickFilter: true,
          },
        }}
        initialState={{
          sorting: {
            sortModel: [{ field: "fechaInicio", sort: "desc" }],
          },
          filter: {
            filterModel: {
              items: [
                { field: "estado", operatorValue: "equals", value: "Pendiente" },
              ],
            },
          },
        }}
      />
      {/* <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          {selectedPermission && <PermissionDetailsModal permission={selectedPermission} onClose={handleClose} />}
        </Box>
      </Modal> */}
      <ConfirmDialog
        open={openConfirmDialog}
        handleClose={handleCloseConfirmDialog}
        handleConfirm={handleConfirmAction}
        message={`¿Está seguro de que desea ${selectedAction === "Aprobado" ? "aprobar" : "rechazar"} esta solicitud?`}
      />
      <Snackbar
        open={snackbar.open}
        autoHideDuration={5000}
        onClose={() => setSnackbar({ ...snackbar, open: false })}
        anchorOrigin={{ vertical: "bottom", horizontal: "left" }}
      >
        <Alert
          onClose={() => setSnackbar({ ...snackbar, open: false })}
          severity={snackbar.severity}
          sx={{ width: "100%" }}
        >
          {snackbar.message}
        </Alert>
      </Snackbar>
    </div>
    </>
  );
};

export default SolicitudesAdmin;