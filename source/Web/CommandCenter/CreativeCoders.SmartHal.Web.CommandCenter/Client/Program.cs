using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Web.Api.Client;
using CreativeCoders.SmartHal.Web.CommandCenter.Client.ViewModels;
using CreativeCoders.SmartHal.Web.CommandCenter.Shared;
using Refit;

namespace CreativeCoders.SmartHal.Web.CommandCenter.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //var cultureInfo = new CultureInfo("de-DE");
            var cultureInfo = new CultureInfo("en-US");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            ConfigureServices(builder.Services, builder.HostEnvironment);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services, IWebAssemblyHostEnvironment hostEnvironment)
        {
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(hostEnvironment.BaseAddress) });

            var apiUri = new Uri("https://localhost:13578/");

            services.AddRefitClient<IGatewaysApi>()
                .ConfigureHttpClient((sp, client) => client.BaseAddress = apiUri);

            services.AddRefitClient<IThingsApi>()
                .ConfigureHttpClient((sp, client) => client.BaseAddress = apiUri);

            services.AddSingleton<GatewaysViewModel>();
            services.AddSingleton<ThingsViewModel>();

            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
        }

        //private async Task<ClientConfigModel> GetClientConfigAsync()
        //{

        //}
    }
}
