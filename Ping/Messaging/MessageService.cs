using Aolh.Library.Serialization.Binary;
using Aolh.MessageBroker.Fabric.Messaging;
using Aolh.MessageBroker.Interfaces.Messaging;
using Aolh.Ping.Domain.Services;
using Aolh.PingPong.SharedDomain.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ping.Messaging
{
    public static class MessageService
    {
        private static IProducer producer;
        private static IConsumer consumer;
        private static Dictionary<Task, IConsumer.MessageReceivedHandler> delegates;

        static MessageService()
        {
            producer = MessageBrokerFabric.CreateProducer(Properties.Settings.Default.MessageBrokerNetwork, new Uri(Properties.Settings.Default.MessageBrokerUri));
            consumer = MessageBrokerFabric.CreateConsumer(Properties.Settings.Default.MessageBrokerNetwork, new Uri(Properties.Settings.Default.MessageBrokerUri));
            delegates = new Dictionary<Task, IConsumer.MessageReceivedHandler>();
        }

        public  static PingMessage SendPing()
        {
            PingMessage pingMessage = PingService.CreatePingMessage();
            Task<PongMessage> task = SendPingAsync(pingMessage);
            task.Wait(60000);
            PongMessage pongMessage = task.Result;
            consumer.MessageReceived -= delegates[task];
            return pingMessage;
        }

        private static void consumer_MessageReceived(object sender, Message message)
        {
            int i = 0;
        }
         
        private static Task<PongMessage> SendPingAsync(PingMessage pingMessage)
        {
            var tcs = new TaskCompletionSource<PongMessage>();
            IConsumer.MessageReceivedHandler del = delegate (object sender, Message message) {
                object obj = Serializer.Deserialize(message.Content);
                if (obj.GetType().Equals(typeof(PongMessage)))
                {
                    PongMessage pongMessage = (PongMessage)obj;
                    if (pongMessage.PingId.Equals(pingMessage.PingId))
                    {
                        Console.WriteLine(string.Format("Pong recibido: {0}", pongMessage.PingId.Id));
                        tcs.TrySetResult(pongMessage);
                    }
                }
            };
            consumer.MessageReceived += del;
            Task<PongMessage> ret= tcs.Task;
            delegates.Add(ret, del);
            Console.WriteLine(string.Format("Enviando Ping: {0}...", pingMessage.PingId.Id));
            producer.SendMessage(new Message(Serializer.Serialize(pingMessage)));
            Console.WriteLine(string.Format("Ping enviado: {0}", pingMessage.PingId.Id));
            return ret;
        }
    }
}
