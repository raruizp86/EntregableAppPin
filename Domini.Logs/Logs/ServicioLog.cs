using Infraestructura.Framework.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicios.Logs
{
    public class ServicioLog : IServicioLog
    {
        private readonly IServicioLogs _servicioLogs;
        public ServicioLog(IServicioLogs servicioLogs)
        {
            _servicioLogs = servicioLogs;
        }
        public void RegistrarExcepcion(string contexto, Exception ex)
        {
            _servicioLogs.RegistrarExcepcion(contexto,ex);
        }

        public void RegistrarExcepcion(Exception ex)
        {
            _servicioLogs.RegistrarExcepcion(ex);
        }

        public void RegistrarAlerta(string contexto,string mensaje)
        {
            _servicioLogs.RegistrarAlerta(contexto,mensaje);
        }

        public void RegistrarInformacion(string contexto, string mensaje)
        {
            _servicioLogs.RegistrarInformacion(contexto, mensaje);
        }
    }
}
