using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Dominio.Servicios.Logs;

namespace Adaptadores.AMQP.Conexion
{
    public class ServicioConexion : IServicioConexion
    {
        private readonly IServicioLog _servicioLog;
        public ServicioConexion(IServicioLog servicioLog)
        {
            _servicioLog = servicioLog;
        }
        public IConnection Conectar()
        {
            try
            {
                var connectionFactory = new ConnectionFactory
                {
                    HostName = "localhost",
                    Port = 5672,
                    UserName = "guest",
                    Password = "guest",
                    Protocol = Protocols.AMQP_0_9_1,
                    RequestedFrameMax = UInt32.MaxValue,
                    RequestedHeartbeat = UInt16.MaxValue
                };
                var connection = connectionFactory.CreateConnection();
                return connection;
            }
            catch (Exception ex)
            {

                _servicioLog.RegistrarExcepcion(ex);
                IConnection connection = null;
                return connection;
            }
        }
    }
}
