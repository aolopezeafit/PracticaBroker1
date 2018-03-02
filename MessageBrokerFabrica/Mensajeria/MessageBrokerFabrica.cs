using Aolh.MessageBroker.Interfaces.Mensajeria;
using Aolh.MessageBroker.Kafka.Mensajeria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aolh.MessageBroker.Fabrica.Mensajeria
{
    public static class MessageBrokerFabrica
    {
        public static IProductor CrearProductor()
        {
            return new KafkaProductor();
        }
        public static IConsumidor CrearConsumidor()
        {
            return new KafkaConsumidor();
        }

        public static string CrearIdentificador()
        {
            return ""+DateTime.Now.Ticks;
        }
    }
}
