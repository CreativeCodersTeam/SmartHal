using System.Text;
using System.Threading.Tasks;
using CreativeCoders.HomeMatic.Api;
using CreativeCoders.HomeMatic.Core;
using CreativeCoders.HomeMatic.XmlRpc.Client;
using CreativeCoders.HomeMatic.XmlRpc.Server;
using CreativeCoders.Messaging.Core;
using CreativeCoders.Messaging.DefaultMediator;
using CreativeCoders.Net.Servers.Http.AspNetCore;
using CreativeCoders.Net.XmlRpc.Server;
using CreativeCoders.SmartHal.Drivers.Base;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;
using CreativeCoders.SmartHal.Kernel.Base.Things;

namespace CreativeCoders.SmartHal.Drivers.HomeMatic
{
    public class HomeMaticGatewayHandler : GatewayHandlerBase
    {
        private readonly IGatewaySetupInfo _gatewaySetupInfo;
        
        private IHomeMaticXmlRpcApi _xmlRpcApi;
        
        private CcuConnection _connection;
        
        private CcuXmlRpcEventServer _eventServer;
        
        private readonly IMediator _mediator;

        public HomeMaticGatewayHandler(IGatewaySetupInfo gatewaySetupInfo)
        {
            _gatewaySetupInfo = gatewaySetupInfo;
            _mediator = new Mediator();
        }

        protected override async Task<ThingState> OnInitAsync()
        {
            var port = _gatewaySetupInfo.GatewayType == "HmIp"
                ? CcuRpcPorts.HomeMaticIp
                : CcuRpcPorts.HomeMatic;
            
            _xmlRpcApi = HomeMaticXmlRpcApiBuilder.Create()
                .ForUrl($"{_gatewaySetupInfo.Address}:{port}")
                .Build();
            
            _connection = new CcuConnection(_xmlRpcApi);

            var xmlRpcServer = new XmlRpcServer(new AspNetCoreHttpServer(), new XmlRpcServerMethods(),
                Encoding.GetEncoding("iso-8859-1"));
            xmlRpcServer.Urls.Add("http://homie-workstation:12345/");
            
            _eventServer = new CcuXmlRpcEventServer(xmlRpcServer);
            
            _eventServer.RegisterEventHandler(new HomeMaticGatewayEventHandler(_mediator, $"GW_{_gatewaySetupInfo.Id.Gateway}"));
            
            await _eventServer.StartAsync();

            await _xmlRpcApi.InitAsync("http://homie-workstation:12345/", $"GW_{_gatewaySetupInfo.Id.Gateway}");
            
            return ThingState.Online;
        }

        public override IThingHandler CreateThingHandler(IThingSetupInfo thingSetupInfo)
        {
            return new HomeMaticThingHandler(thingSetupInfo, _connection, _xmlRpcApi, _mediator);
        }

        protected override async ValueTask OnDisposeAsync()
        {
            await _xmlRpcApi.InitAsync("", $"GW_{_gatewaySetupInfo.Id.Gateway}");

            await _eventServer.StopAsync();
        }
    }
}