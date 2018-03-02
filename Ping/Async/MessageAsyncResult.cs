

using Aolh.Library.Serialization.Binary;
using Aolh.MessageBroker.Interfaces.Messaging;
using Aolh.Ping.Domain.Services;
using Aolh.PingPong.SharedDomain.Messaging;
using Ping.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ping.Async
{
    public class MessageAsyncResult : AsyncResult
    {
        public string Data { get; private set; }
        public MessageAsyncResult(string message, AsyncCallback callback, object state): base(callback, state)
        {
            DateTime startDate = DateTime.Now;
            PingMessage pingMessage= MessageService.SendPing();
            DateTime endDate = DateTime.Now;
            Data = string.Format("Ping {0}: {1} segundos.", pingMessage.PingId.Id, (endDate - startDate).TotalSeconds) ;
        }
    }
}
