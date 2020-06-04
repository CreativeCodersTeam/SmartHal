using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Net.WebApi.Definition;
using CreativeCoders.SmartHal.Web.Api.Core.Models;

namespace CreativeCoders.SmartHal.Web.Api.Client
{
    public interface ISmartHalWebApi
    {
        [Get("items")]
        Task<IEnumerable<ItemModel>> GetItemsAsync();

        // ReSharper disable once StringLiteralTypo
        [Post("items/sendcommand")]
        Task SendCommandAsync([ViaBody] SendCommandModel sendCommandModel);

        [Get("gateways")]
        Task<IEnumerable<GatewayModel>> GetGatewaysAsync();
    }
}
