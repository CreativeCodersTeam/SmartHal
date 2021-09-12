using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Web.Api.Core.Models;
using Refit;

namespace CreativeCoders.SmartHal.Web.Api.Client
{
    public interface IGatewaysApi
    {
        [Get("/api/gateways")]
        Task<IEnumerable<GatewayModel>> GetGatewaysAsync();
    }
}