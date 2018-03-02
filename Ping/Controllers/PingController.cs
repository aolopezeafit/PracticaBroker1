using Aolh.Library.Serialization.Binary;
using Aolh.MessageBroker.Fabric.Messaging;
using Aolh.MessageBroker.Interfaces.Messaging;
using Aolh.Ping.Domain.Services;
using Aolh.PingPong.SharedDomain.Messaging;
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
        private static Object thisLock = new Object();
        public string Get(string id)
        {
            return "Ping " + id;
        }
        public IAsyncResult BeginCommunication(string id, AsyncCallback callback, object state)
        {
            Console.WriteLine("Nueva solicitud http " + DateTime.Now.ToString());
                if (BackPressureService.backPressure.isFull())
                {
                    throw new WebFaultException(System.Net.HttpStatusCode.Forbidden);
                }
                else
                {
                    var messageAsyncResult = new MessageAsyncResult(id, callback, state);
                    ThreadPool.QueueUserWorkItem(CompleteProcess, messageAsyncResult);
                    return messageAsyncResult;
                } 
        }

        public string EndCommunication(IAsyncResult asyncResult)
        {
            var messageAsyncResult = asyncResult as MessageAsyncResult;
            messageAsyncResult.AsyncWait.WaitOne();
            Console.WriteLine("Enviando respuesta http al cliente " + messageAsyncResult.Data);
            return messageAsyncResult.Data;
        }

        private void CompleteProcess(object state)
        {
            var messageAsyncResult = state as MessageAsyncResult;
            messageAsyncResult.Completed();
        }
    }
}
