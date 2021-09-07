using System.Threading.Tasks;
using CreativeCoders.Core.Logging;
using CreativeCoders.HomeMatic.XmlRpc;
using CreativeCoders.HomeMatic.XmlRpc.Server;
using CreativeCoders.HomeMatic.XmlRpc.Server.Messages;
using CreativeCoders.Messaging.Core;

namespace CreativeCoders.SmartHal.Drivers.HomeMatic
{
    public class HomeMaticGatewayEventHandler : ICcuEventHandler
    {
        private static readonly ILogger Log = LogManager.GetLogger<HomeMaticGatewayEventHandler>();
        
        private readonly IMediator _mediator;
        
        private readonly string _interfaceId;

        public HomeMaticGatewayEventHandler(IMediator mediator, string interfaceId)
        {
            _mediator = mediator;
            _interfaceId = interfaceId;
        }
        
        public async Task Event(string address, string valueKey, object value)
        {
            Log.Debug($"CCU event Address = {address}, ValueKey = {valueKey}, Value = {value}");
            
            await _mediator.SendAsync(new HomeMaticEventMessage(_interfaceId, address, valueKey, value))
                .ConfigureAwait(false);
        }

        public Task NewDevices(DeviceDescription[] deviceDescriptions)
        {
            return Task.CompletedTask;
        }

        public Task DeleteDevices(DeviceDescription[] deviceDescriptions)
        {
            return Task.CompletedTask;
        }

        public Task UpdateDevice(string address, int hint)
        {
            return Task.CompletedTask;
        }
    }
}