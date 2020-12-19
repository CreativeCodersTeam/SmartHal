using System;
using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Core.Collections;
using CreativeCoders.SmartHal.Web.Api.Client.ControlCenter;
using CreativeCoders.SmartHal.Web.Api.Core.Models;

namespace CreativeCoders.SmartHal.Web.ControlCenter.Client.ViewModels
{
    public class GatewaysViewModel
    {
        private readonly IGatewaysApi _gatewaysApi;

        public GatewaysViewModel(IGatewaysApi gatewaysApi)
        {
            _gatewaysApi = gatewaysApi;
            GatewayModels = new ExtendedObservableCollection<GatewayModel>();
        }

        public async Task Refresh()
        {
            var gateways = (await _gatewaysApi.GetGatewaysAsync()).ToArray();

            using (GatewayModels.Update())
            {
                GatewayModels.Clear();
                GatewayModels.AddRange(gateways);
            }
            
            Console.WriteLine($"{gateways.Count()} gateways loaded.");
        }

        public ExtendedObservableCollection<GatewayModel> GatewayModels { get; }
    }
}