using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text;
using CreativeCoders.Net.Http;
using CreativeCoders.Net.WebApi.Building;
using CreativeCoders.Net.WebApi.Serialization.Json;
using CreativeCoders.SmartHal.Web.Api.Client.ControlCenter;
using CreativeCoders.SmartHal.Web.ControlCenter.Client.ViewModels;
using CreativeCoders.SmartHal.Web.ControlCenter.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CreativeCoders.SmartHal.Web.ControlCenter.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

            var configUri = new Uri(new Uri(builder.HostEnvironment.BaseAddress), "/api/clientconfig");
            
            var config = await httpClient.GetFromJsonAsync<ClientConfig>(configUri.ToString());

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddTransient(sp => CreateWebApiClient<IGatewaysApi>(config));

            builder.Services.AddTransient(sp => CreateWebApiClient<IThingsApi>(config));

            builder.Services.AddTransient(sp => CreateWebApiClient<IItemsApi>(config));

            builder.Services.AddSingleton<GatewaysViewModel>();
            builder.Services.AddSingleton<ThingsViewModel>();
            builder.Services.AddSingleton<ItemsViewModel>();

            await builder.Build().RunAsync();
        }

        private static T CreateWebApiClient<T>(ClientConfig config)
            where T : class
        {
            Console.WriteLine($"WebApiUrl: {config.WebApiUrl}");

            var apiBuilder = new ApiBuilder(new HttpClient());
            var webApi =
                apiBuilder.BuildApi<T>(config.WebApiUrl, new JsonDataFormatter());

            return webApi;
        }
    }
}
