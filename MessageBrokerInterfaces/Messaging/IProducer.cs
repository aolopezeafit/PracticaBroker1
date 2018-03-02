using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aolh.MessageBroker.Interfaces.Messaging
{
    public interface IProducer
    {
        void SendMessage(Message message);
    }
}
