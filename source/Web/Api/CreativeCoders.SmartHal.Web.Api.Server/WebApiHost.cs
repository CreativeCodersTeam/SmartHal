using System.Collections.Generic;
using CreativeCoders.Config.Base;
using CreativeCoders.Core;
using CreativeCoders.Di;
using CreativeCoders.Net;
using CreativeCoders.SmartHal.Config.Base.WebApi;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CreativeCoders.SmartHal.Web.Api.Server
{
    public class WebApiHost : WebApiHostBase
    {
        private readonly INetworkInfo _networkInfo;

        private readonly IWebApiConfiguration _webApiConfiguration;

        public WebApiHost(IDiContainer diContainer, INetworkInfo networkInfo,
            ISetting<IWebApiConfiguration> webApiConfiguration) : base(diContainer)
        {
            Ensure.IsNotNull(diContainer, nameof(diContainer));
            Ensure.IsNotNull(networkInfo, nameof(networkInfo));
            Ensure.IsNotNull(webApiConfiguration, nameof(webApiConfiguration));

            _networkInfo = networkInfo;
            _webApiConfiguration = webApiConfiguration.Value;
        }

        protected override IEnumerable<string> GetUrls()
        {
            if (_webApiConfiguration.ListenOnHostName)
            {
                yield return $"https://{_networkInfo.GetHostName()}:{_webApiConfiguration.DefaultPort}";
            }

            if (_webApiConfiguration.ListenOnLocalhost)
            {
                yield return $"https://localhost:{_webApiConfiguration.DefaultPort}";
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