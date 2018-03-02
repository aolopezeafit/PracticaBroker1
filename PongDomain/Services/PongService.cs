using Aolh.PingPong.SharedDomain.Messaging;
using Aolh.Pong.Domain.Entities;
using System;
using System.Threading;

namespace Aolh.Pong.Domain.Services
{
    public class PongService
    {
        private static Statistic statistic = new Statistic();
        public static Statistic GetStatistic()
        {
            return statistic;
        }

        public static PongMessage ProcessPing(PingMessage pingMessage)
        {
            statistic = new Statistic(statistic.Requests + 1, statistic.Responses);
            Console.WriteLine("Esperando 2 segundos para responder Pong " + pingMessage.PingId.Id);
            Thread.Sleep(2000);
            return new PongMessage(pingMessage.PingId);
        }

        public static void ProcessPong(PongMessage pongMessage)
        {
            statistic = new Statistic(statistic.Requests, statistic.Responses + 1);
        }
    }
}
