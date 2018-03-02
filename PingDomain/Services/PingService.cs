using Aolh.PingPong.SharedDomain.Messaging;
using System;

namespace Aolh.Ping.Domain.Services
{
    public static class PingService
    {
        public static PingMessage CreatePingMessage()
        {
            return new PingMessage(new PingId(GenerateId().ToString()));
        }

        private static Guid GenerateId()
        {
            return Guid.NewGuid() ;
        }
    }
}
