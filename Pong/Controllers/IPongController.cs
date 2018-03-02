using Aolh.Pong.Domain.Entities;
using Pong.Routes;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Pong.Controllers
{
    [ServiceContract]
    public interface IPongController
    {
        [OperationContract]
        [WebInvoke(UriTemplate = Routing.PongGet, Method = "GET", ResponseFormat = WebMessageFormat.Json )]
        Statistic Get();
    }
}
