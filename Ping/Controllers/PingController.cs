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
            try
            {
                Console.WriteLine("Nueva solicitud http: " + DateTime.Now.ToString());
                MessageAsyncResult mar = new MessageAsyncResult();
                AsyncResult<String> result = new AsyncResult<String>(callback, state);
                ThreadPool.QueueUserWorkItem(mar.GetRandomIntHelper, result);
                return result;
            }
            catch (Exception ex)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }

        /// <summary>
        /// Fin de la tarea asíncrona de la petición.
        /// </summary>
        public string EndGetAsync(IAsyncResult asyncResult)
        {
            try
            {
                AsyncResult<String> r = asyncResult as AsyncResult<String>;
                string result = "" + r.EndInvoke();
                Console.WriteLine("Enviando respuesta http al cliente: " + result);
                return result;
            }
            catch (Exception ex)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }
    }
}
