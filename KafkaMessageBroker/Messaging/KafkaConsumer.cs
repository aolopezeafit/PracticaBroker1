using Aolh.MessageBroker.Interfaces.Messaging;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Aolh.MessageBroker.Kafka.Messaging
{
    public class KafkaConsumer : IConsumer
    {
        public Uri Uri { get; private set; }
        public String Network { get; private set; }
        private Consumer<Null, string> consumer;
        private Thread threadReadMessages;

        public KafkaConsumer(string network, Uri uri)
        {
            Network = network;
            Uri = uri;
            string srv = uri.ToString();

            string tmp = "localhost";

            var config = new Dictionary<string, object>
            {
                { "bootstrap.servers", srv },
                { "group.id", tmp },
                { "default.topic.config", new Dictionary<string, object>
                    {
                        { "auto.offset.reset", "smallest" }
                    }
                }
            };

            consumer = new Consumer<Null, string>(config, null, new StringDeserializer(Encoding.UTF8));

            consumer.OnMessage += (_, msg) =>
            {
                try
                {
                    ProcessMessage(msg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };

            consumer.Subscribe(Network);

            threadReadMessages = new Thread(ReadMessages);
            threadReadMessages.IsBackground = true;
            threadReadMessages.Priority = ThreadPriority.Lowest;
            threadReadMessages.Start();
        }

        private void ProcessMessage(Message<Null, string> item)
        {
            string id = null;
            byte[] value = null;
            if (item.Key != null)
            {
                //id = Encoding.UTF8.GetString(item.Key);
            }
            if (item.Value != null)
            {
                value = Convert.FromBase64String(item.Value);
            }
            RaiseMessageReceived(new Interfaces.Messaging.Message(id, value));
        }

        private void ReadMessages()
        {
            try
            {
                bool cancelled = false;
                while (!cancelled)
                {
                    consumer.Poll(100);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
