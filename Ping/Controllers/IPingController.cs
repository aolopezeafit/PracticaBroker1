using Ping.Routes;
using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Ping.Controllers
{
    [ServiceContract(Name = "Inicio")]
    public interface IPingController
    {
        [OperationContract(AsyncPattern =true)]
        [WebInvoke(UriTemplate = Routing.PingGetAsync, Method = "GET")]
        IAsyncResult BeginGetAsync(AsyncCallback callback, object asyncState);

        string EndGetAsync(IAsyncResult result);
    }
}
