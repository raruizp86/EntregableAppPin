using Dominio.Entidades.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Adaptadores.AMQP.Enviar.Mensajes
{
    public interface IServicioEnviarMensaje
    {
        bool PublicarMensaje(Mensaje mensajes);
       
    }
}
