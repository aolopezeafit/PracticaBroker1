using Aolh.MessageBroker.Interfaces.Messaging;
using KafkaNet;
using KafkaNet.Model;
using System;
using System.Text;
using System.Threading;

namespace Aolh.MessageBroker.Kafka.Messaging
{
    public class KafkaConsumer : IConsumer
    {
        public Uri Uri { get; private set; }
        public String Network { get; private set; }
        private Consumer consumer;
        private Thread threadReadMessages;

        public KafkaConsumer(string network, Uri uri)
        {
            Network = network;
            Uri = uri;
            var options = new KafkaOptions(Uri);
            var router = new BrokerRouter(options);
            consumer = new Consumer(new ConsumerOptions(network, router));
            threadReadMessages = new Thread(ReadMessages);
            threadReadMessages.IsBackground = true;
            threadReadMessages.Priority = ThreadPriority.Lowest;
            threadReadMessages.Start();
        }

        private void ReadMessages()
        {
            try
            {
                string id = null;
                byte[] value = null;
                string base64 = null;
                foreach (var item in consumer.Consume())
                {
                    if (item.Key != null)
                    {
                        id = Encoding.UTF8.GetString(item.Key);
                    }
                    if (item.Value != null)
                    {
                        base64=Encoding.UTF8.GetString(item.Value);
                        value = Convert.FromBase64String(base64);
                    }
                    RaiseMessageReceived(new Message(id, value));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
