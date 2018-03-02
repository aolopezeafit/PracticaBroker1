using System.ServiceModel;
using System.ServiceModel.Activation;
using Aolh.Pong.Domain.Entities;
using Aolh.Pong.Domain.Services;

namespace Pong.Controllers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PongController : IPongController
    {
        Statistic IPongController.Get()
        {
            return PongService.GetStatistic();
        }
    }
}
