using Ping.Routes;
using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Ping.Controllers
{
    [ServiceContract(Name = "Inicio")]
    public interface IPingController
    {
        [OperationContract]
        [WebGet(UriTemplate = Routing.PingGet, BodyStyle = WebMessageBodyStyle.Bare)]
        string Get(string id);

        [OperationContract(AsyncPattern =true)]
        [WebInvoke(UriTemplate = Routing.PingGetAsync, Method = "GET")]
        IAsyncResult BeginCommunication(string id, AsyncCallback callback, object asyncState);

        string EndCommunication(IAsyncResult result);
    }
}
