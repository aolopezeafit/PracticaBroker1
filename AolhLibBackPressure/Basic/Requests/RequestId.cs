using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aolh.Lib.BackPressure.Basic.Requests
{

    /// <summary>
    /// Identificador único.
    /// </summary>
    public class RequestId
    {
        public String Id { get; private set; }

        public RequestId(string id)
        {
            Id = id;
        }

        public bool Equals(RequestId requestId)
        {
            return Id.Equals(requestId.Id);
        }
    }
}
