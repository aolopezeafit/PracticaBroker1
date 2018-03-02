using Aolh.MessageBroker.Interfaces.Messaging;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using System;
using System.Collections.Generic;

namespace Aolh.MessageBroker.Kafka.Messaging
{
    public class KafkaProducer : IProducer
    {
        public Uri Uri { get; private set; }
        public String Network { get; private set; }
        private Producer producer;
        
        public KafkaProducer(string network, Uri uri)
        {
            Network = network;
            Uri = uri;
            var router = new BrokerRouter(new KafkaOptions(uri));
            producer = new Producer(router);
        }

        public void SendMessage(Interfaces.Messaging.Message message)
        {
            string data = Convert.ToBase64String(message.Content);
            KafkaNet.Protocol.Message msg = new KafkaNet.Protocol.Message(data, message.Id);
            producer.SendMessageAsync(Network, new List<KafkaNet.Protocol.Message> { msg }).Wait();
        }
    }
}
