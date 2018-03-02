using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KafkaLib
{
    public class Productor
    {
        public void Inicializar()
        {
            string payload = "Welcome to Kafka!";
            string topic = "IDGTestTopic";
            Message msg = new Message(payload);
            Uri uri = new Uri("http://localhost:9092");
            var router = new BrokerRouter(new KafkaOptions(uri));
            var client = new Producer(router);
            client.SendMessageAsync(topic, new List<Message> { msg }).Wait();
        }
    }
}
