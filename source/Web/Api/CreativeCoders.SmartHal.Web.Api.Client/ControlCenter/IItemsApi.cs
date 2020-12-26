using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Net.WebApi.Definition;
using CreativeCoders.SmartHal.Web.Api.Core.Models;

namespace CreativeCoders.SmartHal.Web.Api.Client.ControlCenter
{
    public interface IItemsApi
    {
        [Get("items")]
        Task<IEnumerable<ItemModel>> GetItemsAsync();

        [Post("items/sendcommand")]
        Task SendCommandAsync([ViaBody] SendCommandModel sendCommandModel);
    }
}