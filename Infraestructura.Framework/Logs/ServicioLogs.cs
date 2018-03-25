using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Framework.Logs
{
    public class ServicioLogs : IServicioLogs
    {
        static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public void RegistrarExcepcion(Exception ex)
        {
            var listErrors = ex.Messages();
            var message = string.Join("; ", listErrors);

            Log(LogLevel.Error, string.Format(
                "{0}\n\r{1}"
                , message
                , ex.StackTrace));

        }

        public void RegistrarExcepcion(string contexto, Exception ex)
        {
            var listErrors = ex.Messages();
            var message = string.Join("; ", listErrors);
            Log(LogLevel.Error, string.Format(
                "{2}->{0}\n\r{1}"
                , message
                , ex.StackTrace
                , contexto));
        }

        private void Log(LogLevel level, string mensaje)
        {
            logger.Log(level, mensaje);
        }

      

        public void RegistrarInformacion(string contexto, string mensaje)
        {
            if (logger.IsInfoEnabled)
            {
                Log(LogLevel.Info, string.Format(
                   "{1}->{0}"
                   , mensaje
                   , contexto));
            }
        }

        public void RegistrarAlerta(string contexto, string mensaje)
        {
            if (logger.IsWarnEnabled)
            {
                Log(LogLevel.Warn, string.Format(
                   "{1}->{0}"
                   , mensaje
                   , contexto));
            }
        }
    }
}
