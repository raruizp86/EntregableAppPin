using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicios.Logs
{
    public interface IServicioLog
    {
        void RegistrarAlerta(string contexto, string mensaje);
        void RegistrarInformacion(string contexto, string mensaje);
        void RegistrarExcepcion(string contexto, Exception ex);
        void RegistrarExcepcion(Exception ex);
    }

}
