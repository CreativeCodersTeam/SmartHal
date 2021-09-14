using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Web.Api.Core.Models;
using Refit;

namespace CreativeCoders.SmartHal.Web.Api.Client
{
    public interface IThingsApi
    {
        [Get("/api/things")]
        Task<IEnumerable<ThingModel>> GetThingsAsync();
    }
}