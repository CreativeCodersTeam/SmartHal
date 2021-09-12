using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Web.Api.Core.Models;

namespace CreativeCoders.SmartHal.Web.Api.Client
{
    public interface IThingsApi
    {
        Task<IEnumerable<ThingModel>> GetThingsAsync();
    }
}