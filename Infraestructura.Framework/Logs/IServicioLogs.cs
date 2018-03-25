using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Framework.Logs
{
    public interface IServicioLogs
    {
        void RegistrarExcepcion(Exception ex);

        void RegistrarExcepcion(string contexto, Exception ex);

        void RegistrarInformacion(string contexto, string mensaje);

        void RegistrarAlerta(string contexto, string mensaje);
    }
}
