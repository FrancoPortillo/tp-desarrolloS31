import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useAuth0 } from '@auth0/auth0-react';
import { Calendar, momentLocalizer, Views } from 'react-big-calendar';
import moment from 'moment';
import 'moment/locale/es';
import 'react-big-calendar/lib/css/react-big-calendar.css';
import './CalendarioVacaciones.css'; // Importa tu archivo CSS personalizado
import { obtenerEmpleados, obtenerVacaciones } from '../../Utils/Axios';

moment.locale('es', {
    months: 'Enero_Febrero_Marzo_Abril_Mayo_Junio_Julio_Agosto_Septiembre_Octubre_Noviembre_Diciembre'.split('_'),
    monthsShort: 'ene_feb_mar_abr_may_jun_jul_ago_sep_oct_nov_dic'.split('_'),
    weekdays: 'domingo_lunes_martes_miércoles_jueves_viernes_sábado'.split('_'),
    weekdaysShort: 'dom._lun._mar._mié._jue._vie._sáb.'.split('_'),
    weekdaysMin: 'do_lu_ma_mi_ju_vi_sá'.split('_'),
    Today: 'Hoy',
    Next: 'Siguiente',
    Back: 'Anterior',
    longDateFormat: {
        LT: 'H:mm',
        LTS: 'H:mm:ss',
        L: 'DD/MM/YYYY',
        LL: 'D [de] MMMM [de] YYYY',
        LLL: 'D [de] MMMM [de] YYYY H:mm',
        LLLL: 'dddd, D [de] MMMM [de] YYYY H:mm'
    },
    calendar: {
        sameDay: '[Hoy a las] LT',
        nextDay: '[Mañana a las] LT',
        nextWeek: 'dddd [a las] LT',
        lastDay: '[Ayer a las] LT',
        lastWeek: '[El] dddd [pasado a las] LT',
        sameElse: 'L'
    },
    relativeTime: {
        future: 'en %s',
        past: 'hace %s',
        s: 'unos segundos',
        ss: '%d segundos',
        m: 'un minuto',
        mm: '%d minutos',
        h: 'una hora',
        hh: '%d horas',
        d: 'un día',
        dd: '%d días',
        M: 'un mes',
        MM: '%d meses',
        y: 'un año',
        yy: '%d años'
    },
    week: {
        dow: 1, 
        doy: 4  
    }
});
const messages = {
    previous: 'Anterior',
    next: 'Siguiente',
    today: 'Hoy',
    noEventsInRange: 'No hay eventos en este rango',
    showMore: total => `+ Ver más (${total})`
};

const CalendarioVacaciones = ({ vacaciones, permisos, empleados, fullWidth }) => {
    const [eventos, setEventos] = useState([]);
    const localizer = momentLocalizer(moment);
    const [vac, setVac] = useState([]);
    const [per, setPer] = useState([]);
    const [emp, setEmp] = useState([]);
  
    useEffect(() => {
      setVac(vacaciones);
      setPer(permisos);
      setEmp(empleados);
    }, [vacaciones, permisos, empleados]);
  
    useEffect(() => {
      if (vac.length > 0 && per.length > 0 && emp.length > 0) {
        callApi();
      }
    }, [vac, per, emp]);
  
    const callApi = async () => {
      try {
        const vacacionesConPuesto = vac.map(vacacion => {
          const empleado = emp.find(emp => emp.id === vacacion.idEmpleado);
          return { ...vacacion, puesto: empleado ? empleado.puesto : 'Desconocido' };
        });
  
        const eventosVacaciones = vacacionesConPuesto.map(vacacion => ({
          title: `Vacaciones de ${vacacion.nombre} (${vacacion.puesto})`,
          start: new Date(vacacion.fechaInicio),
          end: new Date(vacacion.fechaFin)
        }));
  
        const permisosConPuesto = per.map(permiso => {
          const empleado = emp.find(emp => emp.id === permiso.idEmpleado);
          return { ...permiso, puesto: empleado ? empleado.puesto : 'Desconocido' };
        });
  
        const eventosPermisos = permisosConPuesto.map(permiso => ({
          title: `Permiso de ${permiso.nombre} (${permiso.puesto})`,
          start: new Date(permiso.fechaInicio),
          end: new Date(permiso.fechaFin)
        }));
  
        setEventos([...eventosVacaciones, ...eventosPermisos]);
      } catch (error) {
        console.error('Error al obtener el calendario de vacaciones:', error);
      }
    };
  
    return (
      <Calendar
        localizer={localizer}
        events={eventos}
        startAccessor="start"
        endAccessor="end"
        style={{ height: 500, width: fullWidth ? '100%' : '45%' }}
        defaultView={Views.MONTH}
        toolbar={true}
        messages={messages}
      />
    );
  };
  
  export default CalendarioVacaciones;