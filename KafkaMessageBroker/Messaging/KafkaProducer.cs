using Aolh.MessageBroker.Interfaces.Messaging;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aolh.MessageBroker.Kafka.Messaging
{
    public class KafkaProducer : IProducer
    {
        public Uri Uri { get; private set; }
        public String Network { get; private set; }
        private Producer<Null, string> producer;
        
        public KafkaProducer(string network, Uri uri)
        {
            Network = network;
            Uri = uri;
            string srv = uri.ToString();

            string tmp = "localhost";
            var config = new Dictionary<string, object>
            {
                { "bootstrap.servers", srv },
                { "client.id", tmp  },
                { "default.topic.config", new Dictionary<string, object>
                    {
                        { "acks", "all" }
                    }
                }
            };

            producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8));
        }

        public void SendMessage(Interfaces.Messaging.Message message)
        {
            string data = Convert.ToBase64String(message.Content);
            DateTime dt1 = DateTime.Now;
            producer.ProduceAsync(Network, null, data);
            DateTime dt2 = DateTime.Now;
            double dif = (dt2 - dt1).TotalMilliseconds/1000;
            Console.WriteLine("Kafka " + dif);
        }
    }
}
