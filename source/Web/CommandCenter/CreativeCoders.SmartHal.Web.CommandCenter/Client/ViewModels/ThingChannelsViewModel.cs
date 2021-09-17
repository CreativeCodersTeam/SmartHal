using System.Threading;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.Collections;
using CreativeCoders.Core.Threading;
using CreativeCoders.SmartHal.Web.Api.Client;
using CreativeCoders.SmartHal.Web.Api.Core.Models;

namespace CreativeCoders.SmartHal.Web.CommandCenter.Client.ViewModels
{
    public class ThingChannelsViewModel
    {
        private readonly IThingChannelsApi _thingChannelsApi;

        public ThingChannelsViewModel(IThingChannelsApi thingChannelsApi)
        {
            Ensure.IsNotNull(thingChannelsApi, nameof(thingChannelsApi));

            _thingChannelsApi = thingChannelsApi;

            ThingChannels = new ExtendedObservableCollection<ThingChannelModel>(new SynchronizationContext(), SynchronizationMethod.Post, () => new NoLockingMechanism());
        }

        public async Task RefreshAsync()
        {
            ThingChannels = new ExtendedObservableCollection<ThingChannelModel>(
                new SynchronizationContext(), SynchronizationMethod.Post, () => new NoLockingMechanism(),
                await _thingChannelsApi.GetThingChannelsAsync());
        }

        public ExtendedObservableCollection<ThingChannelModel> ThingChannels { get; set; }
    }
}