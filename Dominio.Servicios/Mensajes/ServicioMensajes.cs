using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades.Mensajes;
using Adaptadores.AMQP.Enviar.Mensajes;
using Adaptadores.AMQP.Recibir.Recibir;

namespace Dominio.Servicios.Mensajes
{
    public class ServicioMensajes : IServicioMensajes
    {
        public readonly IServicioEnviarMensaje _servicioEnviarMensaje;
        public readonly IServicioRecibirMensaje _servicioRecibirMensaje;
        public ServicioMensajes(IServicioEnviarMensaje servicioEnviarMensaje, IServicioRecibirMensaje servicioRecibirMensaje)
        {
            _servicioEnviarMensaje = servicioEnviarMensaje;
            _servicioRecibirMensaje = servicioRecibirMensaje;
        }
        public bool EnviarMensaje(string id, string mensaje)
        {
            Mensaje obj = new Mensaje();
            List<Mensaje> Respuesta = new List<Mensaje>();
            Mensaje MensajeEnviar = MensajeEnviadoFormato(id, mensaje);
            bool enviado = _servicioEnviarMensaje.PublicarMensaje(MensajeEnviar);
            return enviado;
        }


        public Mensaje MensajeEnviadoFormato(string id, string mensaje)
        {
            Mensaje objMensaje;


            objMensaje = new Mensaje()
            {
                id = int.Parse(id),
                Cuerpo = mensaje,
            };


            return objMensaje;
        }

        public string RecibirMensaje(string id, string mensaje)
        {
            Mensaje obj = new Mensaje();
            List<Mensaje> Respuesta = new List<Mensaje>();
            Mensaje MensajeEnviar = MensajeEnviadoFormato(id, mensaje);
            string respuesta = _servicioRecibirMensaje.SuscribirMensaje(MensajeEnviar);
            return respuesta;
        }
    }
}
