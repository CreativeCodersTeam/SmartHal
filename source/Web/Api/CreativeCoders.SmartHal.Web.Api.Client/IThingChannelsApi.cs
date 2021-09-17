using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Web.Api.Core.Models;
using Refit;

namespace CreativeCoders.SmartHal.Web.Api.Client
{
    public interface IThingChannelsApi
    {
        public const string GetThingChannelsUrl = "/api/thingchannels";

        [Get(GetThingChannelsUrl)]
        Task<IEnumerable<ThingChannelModel>> GetThingChannelsAsync();
    }
}