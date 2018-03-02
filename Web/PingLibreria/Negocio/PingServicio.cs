using Aolh.MessageBroker.Fabrica.Mensajeria;
using Aolh.MessageBroker.Interfaces.Mensajeria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aolh.Ping.Libreria.Negocio
{
    public static class PingServicio
    {
        private static IProductor productor = MessageBrokerFabrica.CrearProductor();

        public static void EnviarPing()
        {
            productor.EnviarMensaje(new Mensaje("PING_MESSAGE"));
        }
    }
}
