using Aolh.MessageBroker.Interfaces.Messaging;
using Aolh.MessageBroker.Kafka.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aolh.MessageBroker.Fabric.Messaging
{
    public static class MessageBrokerFabric
    {
        public static IProducer CreateProducer(String identifier, Uri uri)
        {
            return new KafkaProducer(identifier, uri);
        }
        public static IConsumer CreateConsumer(String identifier, Uri uri)
        {
            return new KafkaConsumer(identifier, uri);
        }

        public static string GenerateId()
        {
            return ""+DateTime.Now.Ticks;
        }
    }
}
