using System.Collections.Generic;
using System.Reflection;
using CreativeCoders.Config.Base;
using CreativeCoders.Di;
using CreativeCoders.Net;
using CreativeCoders.SmartHal.Config.Base.WebApi;
using CreativeCoders.SmartHal.Web.Api.ServerBase;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CreativeCoders.SmartHal.Web.Api.RemoteControl
{
    public class RemoteControlHost : WebApiHostBase
    {
        private readonly INetworkInfo _networkInfo;

        private readonly IWebApiConfiguration _webApiConfiguration;

        public RemoteControlHost(IDiContainer diContainer, INetworkInfo networkInfo,
            ISetting<IWebApiConfiguration> webApiConfiguration) : base(diContainer)
        {
            _networkInfo = networkInfo;
            _webApiConfiguration = webApiConfiguration.Value;
        }

        protected override IEnumerable<Assembly> GetControllerAssemblies() =>
            new[] {typeof(RemoteControlHost).Assembly};

        protected override IEnumerable<string> GetUrls()
        {
            if (_webApiConfiguration.ListenOnHostName)
            {
                yield return $"http://{_networkInfo.GetHostName()}:{_webApiConfiguration.DefaultPort}";
            }

            if (_webApiConfiguration.ListenOnLocalhost)
            {
                yield return $"http://localhost:{_webApiConfiguration.DefaultPort}";
            }

            foreach (var url in _webApiConfiguration.Urls)
            {
                yield return url;
            }
        }

        protected override void ConfigureHostBuilder(IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureLogging(x => x.AddConsole());
        }
    }
}