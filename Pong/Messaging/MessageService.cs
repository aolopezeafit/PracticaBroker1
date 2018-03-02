using Aolh.Library.Serialization.Binary;
using Aolh.MessageBroker.Fabric.Messaging;
using Aolh.MessageBroker.Interfaces.Messaging;
using Aolh.PingPong.SharedDomain.Messaging;
using Aolh.Pong.Domain.Entities;
using Aolh.Pong.Domain.Services;
using System;
using System.Threading;

namespace Pong.Messaging
{
    public static class MessageService
    {
        private static IProducer producer;
        private static IConsumer consumer;

        static MessageService()
        {
            producer = MessageBrokerFabric.CreateProducer(Properties.Settings.Default.MessageBrokerNetwork, new Uri(Properties.Settings.Default.MessageBrokerUri));
            consumer = MessageBrokerFabric.CreateConsumer(Properties.Settings.Default.MessageBrokerNetwork, new Uri(Properties.Settings.Default.MessageBrokerUri));
            consumer.MessageReceived += consumer_MessageReceived;
            //producer.SendMessage(new Message(Serializer.Serialize(new PingMessage(new PingId(DateTime.Now.Ticks.ToString()))))); //test envío
        }

        public static void Init()
        {
        }

        private static void consumer_MessageReceived(object sender, Message message)
        {
            try
            {
                object obj = Serializer.Deserialize(message.Content);
                if (obj.GetType().Equals(typeof(PingMessage)))
                {
                    PingMessage pingMessage = (PingMessage)obj;
                    Console.WriteLine(string.Format("Ping recibido: {0}", pingMessage.PingId.Id));
                    PongMessage pongMessage = PongService.ProcessPing(pingMessage);
                    Console.WriteLine(string.Format("Enviando Pong: {0}...", pongMessage.PingId.Id));
                    producer.SendMessage(new Message(Serializer.Serialize(pongMessage)));
                    Console.WriteLine(string.Format("Pong enviado: {0}", pongMessage.PingId.Id));
                    PongService.ProcessPong(pongMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static Statistic GetStatistic(object sender, Message message)
        {
            return PongService.GetStatistic();
        }
    }
}
