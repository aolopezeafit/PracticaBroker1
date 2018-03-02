using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aolh.PingPong.SharedDomain.Messaging
{
    [Serializable]
    public class PingMessage
    {
        public PingId PingId { get; private set; }

        public PingMessage(PingId pingId)
        {
            PingId = pingId;
        }
    }
}
