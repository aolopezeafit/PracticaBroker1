using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aolh.Lib.BackPressure.Basic.Requests
{
    internal class RequestUnsubscriber<RequestInfo> : IDisposable
    {
        private List<IObserver<RequestInfo>> _observers;
        private IObserver<RequestInfo> _observer;

        internal RequestUnsubscriber(List<IObserver<RequestInfo>> observers, IObserver<RequestInfo> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
