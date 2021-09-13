using System.Threading;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.Collections;
using CreativeCoders.Core.Threading;
using CreativeCoders.SmartHal.Web.Api.Client;
using CreativeCoders.SmartHal.Web.Api.Core.Models;

namespace CreativeCoders.SmartHal.Web.CommandCenter.Client.ViewModels
{
    public class GatewaysViewModel
    {
        private readonly IGatewaysApi _gatewaysApi;

        public GatewaysViewModel(IGatewaysApi gatewaysApi)
        {
            Ensure.IsNotNull(gatewaysApi, nameof(gatewaysApi));

            _gatewaysApi = gatewaysApi;

            Gateways = new ExtendedObservableCollection<GatewayModel>(new SynchronizationContext(), SynchronizationMethod.Post, () => new NoLockingMechanism());
        }

        public async Task RefreshAsync()
        {
            Gateways = new ExtendedObservableCollection<GatewayModel>(
                new SynchronizationContext(), SynchronizationMethod.Post, () => new NoLockingMechanism(),
                await _gatewaysApi.GetGatewaysAsync());
            
            //using var _ = Gateways.Update();

            //var gateways = await _gatewaysApi.GetGatewaysAsync();

            //Gateways.Clear();

            //Gateways.AddRange(gateways);
        }

        public ExtendedObservableCollection<GatewayModel> Gateways { get; set; }
    }
}