using System;
using System.Net.Http;
using CreativeCoders.SmartHal.Web.Api.Client;
using CreativeCoders.SmartHal.Web.CommandCenter.Client.Localization;
using CreativeCoders.SmartHal.Web.CommandCenter.Client.ViewModels;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CreativeCoders.SmartHal.Web.CommandCenter.Client
{
    public class Startup
    {
        private readonly IWebAssemblyHostEnvironment _hostEnvironment;

        public Startup(IWebAssemblyHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(_hostEnvironment.BaseAddress) });

            var apiUri = new Uri("https://localhost:13578/");

            services
                .AddApi<IGatewaysApi>(apiUri)
                .AddApi<IThingsApi>(apiUri)
                .AddApi<IThingChannelsApi>(apiUri)
                .AddApi<IItemsApi>(apiUri);


            services.AddSingleton<GatewaysViewModel>();
            services.AddSingleton<ThingsViewModel>();
            services.AddSingleton<ThingChannelsViewModel>();

            services.SetupLocalization("Resources");
        }
    }
}