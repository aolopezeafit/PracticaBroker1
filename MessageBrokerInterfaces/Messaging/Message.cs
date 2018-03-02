using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aolh.MessageBroker.Interfaces.Messaging
{
    public class Message
    {
        public string Id { get; private set; }
        public byte[] Content {get; private set; }

        public Message(byte[] content) : this(null, content)
        {
        }

        public Message(string id, byte[] content)
        {
            Id = id;
            Content = content;
        }
    }
}
