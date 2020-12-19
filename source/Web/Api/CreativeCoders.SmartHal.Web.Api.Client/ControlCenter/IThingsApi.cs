using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Net.WebApi.Definition;
using CreativeCoders.SmartHal.Web.Api.Core.Models;

namespace CreativeCoders.SmartHal.Web.Api.Client.ControlCenter
{
    public interface IThingsApi
    {
        [Get("things")]
        Task<IEnumerable<ThingModel>> GetThingsAsync();
    }
}