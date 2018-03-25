using Adaptadores.AMQP.Enviar.Mensajes;
using Dominio.Servicios.Mensajes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AppPing.Controllers
{
    public class PingController : AsyncController
    {
        private IServicioEnviarMensaje _mensajesEnviarServicios;
        private IServicioMensajes _mensajesServicios;
        public PingController(IServicioEnviarMensaje mensajesEnviarServicios, IServicioMensajes mensajesServicios)
        {

            _mensajesEnviarServicios = mensajesEnviarServicios;
            _mensajesServicios = mensajesServicios;
        }
        public ActionResult EnviarMensaje(string Id, string mensajeEnviado)
        {


            int hilo = Thread.CurrentThread.ManagedThreadId;
            var msg = string.Format(@"He Enviado un mensaje desde Ping! Hora:{0} : {1} : {2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            ViewBag.Inicial = msg;
            bool procesar = true;
            //DelegadoProcesar enviar = new DelegadoProcesar(procesarMensaje);
            var tasks = new List<Task<int>>();
            Task t = Task.Factory.StartNew(() => (procesar = _mensajesServicios.EnviarMensaje(Id, mensajeEnviado)));
            Task.WaitAll(t);
            ResponderMensaje(Id,mensajeEnviado);
            
            return View();
        }

        public void procesarMensaje(string Id, string mensajeEnviado)
        {
            int hilo = Thread.CurrentThread.ManagedThreadId;
            bool procesar = _mensajesServicios.EnviarMensaje(Id, mensajeEnviado);

        }


        public ActionResult ResponderMensaje(string Id, string mensajeEnviado)
        {
            
            string respuesta = string.Empty;
            //DelegadoRecibir X = (DelegadoRecibir)((AsyncResult)result).AsyncDelegate;
            respuesta = _mensajesServicios.RecibirMensaje(Id, mensajeEnviado);
            if (respuesta != "")
                ViewBag.Respuesta = respuesta;
            
            return View();

        }


    }
}