using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Aolh.MessageBroker.Interfaces.Messaging
{
    public abstract class IConsumer
    {
        public delegate void MessageReceivedHandler(object sender, Message message);
        public event MessageReceivedHandler MessageReceived;

        protected void RaiseMessageReceived(Message message)
        {
            if (MessageReceived == null) return;
            var thread = new Thread(e => MessageReceived.Invoke(this, message));
            thread.Start();
        }
    }
}
