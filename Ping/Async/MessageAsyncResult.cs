

using Aolh.Lib.BackPressure.Basic.Requests;
using Aolh.Library.Serialization.Binary;
using Aolh.MessageBroker.Interfaces.Messaging;
using Aolh.Ping.Domain.Services;
using Aolh.PingPong.SharedDomain.Messaging;
using Ping.BackPressure;
using Ping.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ping.Async
{
    /// <summary>
    /// Tarea asíncrona para procesar solicitud del cliente.
    /// Implementa el observer del back pressure para saber si se procesa o no.
    /// </summary>
    public class MessageAsyncResult : AsyncResult, IBasicBackPressureObserver
    {
        public delegate void MessageReceivedHandler(object sender, bool cancel);
        public event MessageReceivedHandler MessageReceived;
        protected void RaiseMessageReceived(bool cancel)
        {
            if (MessageReceived == null) return;
            var thread = new Thread(e => MessageReceived.Invoke(this, cancel));
            thread.Start();
        }

        public string Data { get; private set; }
        public Exception Exception { get; private set; }
        Request request;

        public MessageAsyncResult( AsyncCallback callback, object state): base(callback, state)
        {
            Task<string> task = ProcessAsync();
            //task.Wait(60000);
            Data = task.Result;
            BackPressureService.backPressure.RemoveObserver(this);
        }

        /// <summary>
        /// Tarea asíncrona que solicita al back pressure ser procesada y espera la respuesta de este para continuar o salir.
        /// </summary>
        private Task<string> ProcessAsync()
        {
            var tcs = new TaskCompletionSource<string>();
            MessageReceivedHandler delegateBackPressure = delegate (object sender, bool cancel) {
                string str;
                if (cancel)
                {
                    str = string.Format("Rechazado por back pressure." );
                }
                else
                {
                    DateTime startDate = DateTime.Now;
                    PingMessage pingMessage = MessageService.SendPing();
                    DateTime endDate = DateTime.Now;
                    str = string.Format("Ping {0}: {1} segundos.", pingMessage.PingId.Id, (endDate - startDate).TotalSeconds);
                }
                BackPressureService.backPressure.RemoveRequest(request);
                tcs.TrySetResult(str);
            };
            MessageReceived += delegateBackPressure;
            Task<string> ret = tcs.Task;
            BackPressureService.backPressure.AddObserver(this);

            request = BackPressureService.backPressure.CreateRequest(this);
            BackPressureService.backPressure.AddRequest(request);
            return ret;
        }

        /// <summary>
        /// Es llamado cuando el back pressure acepta una nueva petición.
        /// </summary>
        public void OnNext(Request request)
        {
            if (this.request!=null)
            {
                if (this.request.RequestId.Equals(request.RequestId))
                {
                    RaiseMessageReceived(false);
                }
            }
        }

        /// <summary>
        /// Es llamado cuando el back pressure rechaza una petición.
        /// </summary>
        public void OnError(Exception ex, Request request)
        {
            if (this.request != null)
            {
                if (this.request.RequestId.Equals(request.RequestId))
                {
                    this.Exception = ex;
                    RaiseMessageReceived(true);
                }
            }
        }

        public void OnError(Exception error)
        {
        }

        public void OnCompleted()
        {
        }
    }
}
