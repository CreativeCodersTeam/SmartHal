using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.HomeMatic.Core;
using CreativeCoders.HomeMatic.Core.Devices;
using CreativeCoders.HomeMatic.XmlRpc.Client;
using CreativeCoders.Messaging.Core;
using CreativeCoders.SmartHal.Drivers.Base;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Channels;
using CreativeCoders.SmartHal.Kernel.Base.Things;

namespace CreativeCoders.SmartHal.Drivers.HomeMatic
{
    public class HomeMaticThingHandler : ThingHandlerBase
    {
        private readonly IThingSetupInfo _thingSetupInfo;
        
        private readonly ICcuConnection _ccuConnection;
        
        private readonly IHomeMaticXmlRpcApi _xmlRpcApi;
        
        private readonly IMediator _mediator;

        public HomeMaticThingHandler(IThingSetupInfo thingSetupInfo, ICcuConnection ccuConnection,
            IHomeMaticXmlRpcApi xmlRpcApi, IMediator mediator)
        {
            _thingSetupInfo = thingSetupInfo;
            _ccuConnection = ccuConnection;
            _xmlRpcApi = xmlRpcApi;
            _mediator = mediator;
        }

        protected override async Task<ThingState> OnInitAsync()
        {
            var device = await _ccuConnection.GetDeviceAsync(_thingSetupInfo.Address);

            await device.Channels.ForEachAsync(AddDeviceChannel);
            
            return ThingState.Online;
        }

        private async Task AddDeviceChannel(ICcuDeviceChannel channel)
        {
            await channel.ParamSets.ForEachAsync(paramSet => AddParameters(channel, paramSet));
        }

        private async Task AddParameters(ICcuDeviceBase channel, string paramSet)
        {
            var parameters = await _ccuConnection.GetParameterInfoAsync(channel.Address, paramSet);
            
            parameters.ForEach(parameter => AddParameter(channel.Address, parameter.Key));
        }

        private void AddParameter(string channelAddress, string parameterName)
        {
            var channelName = GetChannelName(channelAddress) + "_" + parameterName;

            if (!_thingSetupInfo.Template.IsChannelDefined(channelName))
            {
                return;
            }
            
            var channelHandler = new HomeMaticThingChannelHandler(_thingSetupInfo.Id, channelName, channelAddress, parameterName, _xmlRpcApi, _mediator);
            
            MessageHub.SendMessage(new NewThingChannelMessage(_thingSetupInfo.Id.ToString(), channelHandler));
        }
        
        private static string GetChannelName(string address)
        {
            var index = address?.IndexOf(":");
            if (index >= 0)
            {
                return "CH" + address.Substring(index.Value + 1);
            }
            return address;
        }
    }
}