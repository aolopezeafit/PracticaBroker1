using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aolh.Lib.BackPressure.Basic.Requests
{

    /// <summary>
    /// Petición con identificador único para procesamiento del back pressure.
    /// </summary>
    public class Request
    {
        public RequestId RequestId { get; private set; }
        public object Tag { get; private set; }
        public DateTime CreationDate { get; private set; }

        public Request(RequestId requestId, object tag)
        {
            RequestId = requestId;
            Tag = tag;
            CreationDate = DateTime.Now;
        }
    }
}
