using System.Threading;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.Collections;
using CreativeCoders.Core.Threading;
using CreativeCoders.SmartHal.Web.Api.Client;
using CreativeCoders.SmartHal.Web.Api.Core.Models;

namespace CreativeCoders.SmartHal.Web.CommandCenter.Client.ViewModels
{
    public class ThingsViewModel
    {
        private readonly IThingsApi _thingsApi;

        public ThingsViewModel(IThingsApi thingsApi)
        {
            Ensure.IsNotNull(thingsApi, nameof(thingsApi));

            _thingsApi = thingsApi;

            Things = new ExtendedObservableCollection<ThingModel>(new SynchronizationContext(), SynchronizationMethod.Post, () => new NoLockingMechanism());
        }

        public async Task RefreshAsync()
        {
            Things = new ExtendedObservableCollection<ThingModel>(
                new SynchronizationContext(), SynchronizationMethod.Post, () => new NoLockingMechanism(),
                await _thingsApi.GetThingsAsync());
        }

        public ExtendedObservableCollection<ThingModel> Things { get; set; }
    }
}