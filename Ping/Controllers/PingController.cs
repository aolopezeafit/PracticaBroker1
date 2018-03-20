using Aolh.Lib.BackPressure.Basic.Requests;
using Ping.Async;
using Ping.BackPressure;
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Threading;

namespace Ping.Controllers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PingController : IPingController
    {
        public PingController()
        {
        }
        /// <summary>
        /// Inicio de tarea asíncrona para una nueva petición.
        /// </summary>
        public IAsyncResult BeginGetAsync(AsyncCallback callback, object state)
        {
            Console.WriteLine("Nueva solicitud http: " + DateTime.Now.ToString());
            var messageAsyncResult = new MessageAsyncResult(  callback, state);
            ThreadPool.QueueUserWorkItem(CompleteProcess, messageAsyncResult);
            return messageAsyncResult;
        }

        /// <summary>
        /// Fin de la tarea asíncrona de la petición.
        /// </summary>
        public string EndGetAsync(IAsyncResult asyncResult)
        {
            var messageAsyncResult = asyncResult as MessageAsyncResult;
            messageAsyncResult.AsyncWait.WaitOne();
            Console.WriteLine("Enviando respuesta http al cliente: " + messageAsyncResult.Data);
            if (messageAsyncResult.Exception==null)
            {
                return messageAsyncResult.Data;
            }
            else
            {
                throw new WebFaultException(System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }

        private void CompleteProcess(object state)
        {
            var messageAsyncResult = state as MessageAsyncResult;
            messageAsyncResult.Completed();
        }
    }
}
