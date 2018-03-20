using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aolh.Lib.BackPressure.Basic.Requests
{

    /// <summary>
    /// Interfaz del observer.
    /// </summary>
    public interface IBasicBackPressureObserver : IObserver<Request>
    {
        void OnError(  Exception ex, Request request);
    }
}
