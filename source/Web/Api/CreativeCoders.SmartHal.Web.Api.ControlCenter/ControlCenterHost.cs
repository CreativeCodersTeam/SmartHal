using System.Collections.Generic;
using System.Reflection;
using CreativeCoders.Config.Base;
using CreativeCoders.Di;
using CreativeCoders.Net;
using CreativeCoders.SmartHal.Config.Base.WebApi;
using CreativeCoders.SmartHal.Web.Api.ServerBase;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CreativeCoders.SmartHal.Web.Api.ControlCenter
{
    public class ControlCenterHost : WebApiHostBase
    {
        private readonly INetworkInfo _networkInfo;

        private readonly IControlCenterConfiguration _controlCenterConfiguration;

        public ControlCenterHost(IDiContainer diContainer, INetworkInfo networkInfo,
            ISetting<IControlCenterConfiguration> controlCenterConfiguration) : base(diContainer)
        {
            _networkInfo = networkInfo;
            _controlCenterConfiguration = controlCenterConfiguration.Value;
        }

        protected override IEnumerable<Assembly> GetControllerAssemblies() =>
            new[] { typeof(ControlCenterHost).Assembly };

        protected override IEnumerable<string> GetUrls()
        {
            if (_controlCenterConfiguration.ListenOnHostName)
            {
                yield return $"http://{_networkInfo.GetHostName()}:{_controlCenterConfiguration.DefaultPort}";
            }

            if (_controlCenterConfiguration.ListenOnLocalhost)
            {
                yield return $"http://localhost:{_controlCenterConfiguration.DefaultPort}";
            }

            foreach (var url in _controlCenterConfiguration.Urls)
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
