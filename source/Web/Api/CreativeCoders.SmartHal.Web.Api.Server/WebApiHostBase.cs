using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Di;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CreativeCoders.SmartHal.Web.Api.Server
{
    public abstract class WebApiHostBase
    {
        private readonly IDiContainer _diContainer;

        private IHost _webHost;

        protected WebApiHostBase(IDiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public async Task StartAsync()
        {
            var hostBuilder = CreateHostBuilder();

            ConfigureHostBuilder(hostBuilder);

            _webHost = hostBuilder.Build();

            await _webHost.StartAsync().ConfigureAwait(false);
        }

        public async Task StopAsync()
        {
            await _webHost.StopAsync().ConfigureAwait(false);

            _webHost.Dispose();
        }

        private IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    webBuilder.UseUrls(GetUrls().ToArray());

                    webBuilder.ConfigureServices(x =>
                        x.AddSingleton<IControllerFactory>(_ => new DiContainerControllerFactory(_diContainer)));

                    webBuilder.ConfigureServices(ConfigureServices);
                });

        protected virtual void ConfigureHostBuilder(IHostBuilder hostBuilder)
        {
        }

        protected abstract IEnumerable<string> GetUrls();

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }
    }
}
