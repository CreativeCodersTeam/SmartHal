using System;
using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Core.Collections;
using CreativeCoders.SmartHal.Web.Api.Client.ControlCenter;
using CreativeCoders.SmartHal.Web.Api.Core.Models;

namespace CreativeCoders.SmartHal.Web.ControlCenter.Client.ViewModels
{
    public class ThingsViewModel
    {
        private readonly IThingsApi _gatewaysApi;

        public ThingsViewModel(IThingsApi gatewaysApi)
        {
            _gatewaysApi = gatewaysApi;
            ThingModels = new ExtendedObservableCollection<ThingModel>();
        }

        public async Task Refresh()
        {
            var things = (await _gatewaysApi.GetThingsAsync()).ToArray();

            using (ThingModels.Update())
            {
                ThingModels.Clear();
                ThingModels.AddRange(things);
            }

            Console.WriteLine($"{things.Length} gateways loaded.");
        }

        public ExtendedObservableCollection<ThingModel> ThingModels { get; }
    }
}