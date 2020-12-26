using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CreativeCoders.Net.WebApi.Building;
using CreativeCoders.Net.WebApi.Serialization.Json;
using CreativeCoders.SmartHal.Web.Api.Client.ControlCenter;
using CreativeCoders.SmartHal.Web.ControlCenter.Client.ViewModels;
using CreativeCoders.SmartHal.Web.ControlCenter.Shared;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CreativeCoders.SmartHal.Web.ControlCenter.Client
{
    public class Program
    {
        private const string ClientConfigUrl = "/api/clientconfig";
        
        [UsedImplicitly]
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

            var configUri = new Uri(new Uri(builder.HostEnvironment.BaseAddress), ClientConfigUrl);
            
            var config = await httpClient.GetFromJsonAsync<ClientConfig>(configUri);

            builder.Services.AddTransient(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddTransient(_ => CreateWebApiClient<IGatewaysApi>(config));

            builder.Services.AddTransient(_ => CreateWebApiClient<IThingsApi>(config));

            builder.Services.AddTransient(_ => CreateWebApiClient<IItemsApi>(config));

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
