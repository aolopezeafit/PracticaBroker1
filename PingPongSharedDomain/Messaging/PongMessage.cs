using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aolh.PingPong.SharedDomain.Messaging
{
    [Serializable]
    public class PongMessage
    {
        public PingId PingId { get; private set; }
        public PongMessage(PingId pingId)
        {
            PingId = pingId;
        }
    }
}
