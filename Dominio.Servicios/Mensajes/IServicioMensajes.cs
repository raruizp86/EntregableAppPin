using Dominio.Entidades.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicios.Mensajes
{
    public interface IServicioMensajes
    {
        bool EnviarMensaje(string id, string mensaje);
        Mensaje MensajeEnviadoFormato(string id, string mensaje);
        string RecibirMensaje(string id,string mensaje);

    }
}
