using Dominio.Entidades.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptadores.AMQP.Recibir.Recibir
{
    public interface IServicioRecibirMensaje
    {
        string SuscribirMensaje(Mensaje mensajes);
    }
}
