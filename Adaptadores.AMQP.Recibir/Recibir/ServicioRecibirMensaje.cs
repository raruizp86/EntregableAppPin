using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades.Mensajes;
using Adaptadores.AMQP.Conexion;
using Dominio.Servicios.Logs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System.Threading;

namespace Adaptadores.AMQP.Recibir.Recibir
{
    public class ServicioRecibirMensaje : IServicioRecibirMensaje
    {
        private readonly IServicioConexion _servicioConexion;
        private readonly IServicioLog _servicioLog;
        public ServicioRecibirMensaje(IServicioConexion servicioConexion, IServicioLog servicioLog)
        {
            _servicioConexion = servicioConexion;
            _servicioLog = servicioLog;
        }
        public string SuscribirMensaje(Mensaje mensajes)
        {
            try
            {
                string mensaje = string.Empty;
                int hilo = Thread.CurrentThread.ManagedThreadId;
                IConnection connection = _servicioConexion.Conectar();
                using (var channel = connection.CreateModel())
                {
                    // This instructs the channel not to prefetch more than one message
                    channel.BasicQos(0, 1, false);

                    // Create a new, durable exchange
                    channel.ExchangeDeclare("Respuesta", ExchangeType.Direct, true, false, null);
                    // Create a new, durable queue
                    channel.QueueDeclare("ColasRespuesta", true, false, false, null);
                    // Bind the queue to the exchange
                    channel.QueueBind("ColasRespuesta", "Respuesta", "optional-routing-key");

                    using (var subscription = new Subscription(channel, "ColasRespuesta", false))
                    {
                        Console.WriteLine("Waiting for messages...");
                        var encoding = new UTF8Encoding();
                        while (channel.IsOpen)
                        {
                            BasicDeliverEventArgs eventArgs;
                            var success = subscription.Next(2000, out eventArgs);
                            if (success == false) continue;
                            var msgBytes = eventArgs.Body;
                            var message = encoding.GetString(msgBytes);
                            mensaje = message.ToString();
                            
                            channel.BasicAck(eventArgs.DeliveryTag, false);
                            return mensaje;
                           
                        }
                        return mensaje;
                    }
                   
                }
               
            }

            catch (Exception ex)
            {
                string mensanje = "No se pudo recibir mensajes";
                _servicioLog.RegistrarExcepcion("El mensaje con el id:" + mensajes.id + "no puedo ser enviado", ex);
                return mensanje;
            }
        }
}
}
