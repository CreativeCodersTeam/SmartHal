using System.Text;
using System.Threading.Tasks;
using CreativeCoders.Core.SysEnvironment;
using CreativeCoders.HomeMatic.Api;
using CreativeCoders.HomeMatic.Core;
using CreativeCoders.HomeMatic.XmlRpc.Client;
using CreativeCoders.HomeMatic.XmlRpc.Server;
using CreativeCoders.Messaging.Core;
using CreativeCoders.Messaging.DefaultMediator;
using CreativeCoders.Net;
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
        private const int XmlRpcDefaultPort = 12345;

        private readonly IGatewaySetupInfo _gatewaySetupInfo;
        
        private IHomeMaticXmlRpcApi _xmlRpcApi;
        
        private CcuConnection _connection;
        
        private CcuXmlRpcEventServer _eventServer;
        
        private readonly IMediator _mediator;

        private readonly INetworkInfo _networkInfo;

        private AspNetCoreHttpServer _httpServer;

        private string _ccuInterfaceId;


        public HomeMaticGatewayHandler(IGatewaySetupInfo gatewaySetupInfo)
        {
            _gatewaySetupInfo = gatewaySetupInfo;
            _mediator = new Mediator();

            _networkInfo = new NetworkInfo();
        }

        protected override async Task<ThingState> OnInitAsync()
        {
            _ccuInterfaceId = $"GW_{_gatewaySetupInfo.Id.Gateway}_{Env.TickCount}";

            var xmlRpcPort = _gatewaySetupInfo.ReadSetting("XmlRpcEventsPort", XmlRpcDefaultPort);

            var xmlRpcUrl = $"http://{_networkInfo.GetHostName()}:{xmlRpcPort}";

            var ccuPort = _gatewaySetupInfo.GatewayType == "HmIp"
                ? CcuRpcPorts.HomeMaticIp
                : CcuRpcPorts.HomeMatic;
            
            _xmlRpcApi = HomeMaticXmlRpcApiBuilder.Create()
                .ForUrl($"{_gatewaySetupInfo.Address}:{ccuPort}")
                .Build();
            
            _connection = new CcuConnection(_xmlRpcApi);

            await StartEventServer(xmlRpcUrl);

            await _xmlRpcApi.InitAsync(xmlRpcUrl, _ccuInterfaceId);
            
            return ThingState.Online;
        }

        private async Task StartEventServer(string xmlRpcUrl)
        {
            _httpServer = new AspNetCoreHttpServer {AllowSynchronousIO = true};

            var xmlRpcServer = new XmlRpcServer(_httpServer, new XmlRpcServerMethods(),
                Encoding.GetEncoding("iso-8859-1"));
            xmlRpcServer.Urls.Add(xmlRpcUrl);

            _eventServer = new CcuXmlRpcEventServer(xmlRpcServer);

            _eventServer.RegisterEventHandler(
                new HomeMaticGatewayEventHandler(_mediator, $"GW_{_gatewaySetupInfo.Id.Gateway}"));

            await _eventServer.StartAsync();
        }

        public override IThingHandler CreateThingHandler(IThingSetupInfo thingSetupInfo)
        {
            return new HomeMaticThingHandler(thingSetupInfo, _connection, _xmlRpcApi, _mediator);
        }

        protected override async ValueTask OnDisposeAsync()
        {
            await _xmlRpcApi.InitAsync("", _ccuInterfaceId);

            await _eventServer.StopAsync();

            _httpServer.Dispose();
        }
    }
}