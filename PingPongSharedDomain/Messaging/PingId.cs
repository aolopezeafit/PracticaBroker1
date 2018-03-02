using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aolh.PingPong.SharedDomain.Messaging
{
    [Serializable]
    public class PingId
    {
        public String Id { get; private set; }

        public PingId(string id)
        {
            Id = id;
        }

        public bool Equals(PingId pingId)
        {
            return Id.Equals(pingId.Id);
        }
    }
}
