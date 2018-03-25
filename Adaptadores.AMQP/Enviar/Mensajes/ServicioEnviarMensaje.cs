using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades.Mensajes;
using RabbitMQ.Client;
using Adaptadores.AMQP.Conexion;
using Dominio.Servicios.Logs;
using System.Threading;

namespace Adaptadores.AMQP.Enviar.Mensajes
{
    public class ServicioEnviarMensaje : IServicioEnviarMensaje
    {

        private readonly IServicioConexion _servicioConexion;
        private readonly IServicioLog _servicioLog;
        public ServicioEnviarMensaje(IServicioConexion servicioConexion, IServicioLog servicioLog)
        {
            _servicioConexion = servicioConexion;
            _servicioLog = servicioLog;
        }
        public bool PublicarMensaje(Mensaje mensajes)
        {
            try
            {
              int  hilo = Thread.CurrentThread.ManagedThreadId;
                IConnection connection = _servicioConexion.Conectar();

                if (connection.IsOpen)
                {

                    using (var channel = connection.CreateModel())
                    {

                        channel.ExchangeDeclare("Servicios", ExchangeType.Direct, true, false, null);

                        channel.QueueDeclare("Colas", true, false, false, null);

                        channel.QueueBind("Colas", "Servicios", "optional-routing-key");


                        var properties = channel.CreateBasicProperties();
                        properties.DeliveryMode = 1;
                        properties.ClearMessageId();
                        properties.ReplyTo = "Servicios";
                        var encoding = new UTF8Encoding();
                        var hora = DateTime.Now;
                        var msg = string.Format(@"El Id:{1} a Enviando un mensaje desde Ping! a las {0} que dice {2}", hora,mensajes.id,mensajes.Cuerpo);
                        var msgBytes = encoding.GetBytes(msg);

                        channel.BasicPublish("Servicios", "optional-routing-key", false, properties, msgBytes);

                        channel.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _servicioLog.RegistrarExcepcion("El mensaje con el id:"+mensajes.id + "no puedo ser enviado",ex);
                return false;
            }
          
        }

       
    }
}
