using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Playground.DummyTestModule
{
    [UsedImplicitly]
    [Driver(nameof(DummyDriver))]
    public class DummyDriver : IDriver
    {
        public Task InitAsync()
        {
            return Task.CompletedTask;
        }

        public IGatewayHandler CreateGatewayHandler(IGatewaySetupInfo gatewaySetupInfo)
        {
            return new DummyGatewayHandler(gatewaySetupInfo);
        }
    }
}