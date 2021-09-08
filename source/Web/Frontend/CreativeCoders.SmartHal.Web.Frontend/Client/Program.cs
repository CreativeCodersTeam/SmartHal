using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CreativeCoders.Net.WebApi.Building;
using CreativeCoders.Net.WebApi.Serialization.Json;
using CreativeCoders.SmartHal.Web.Api.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CreativeCoders.SmartHal.Web.Frontend.Shared;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Web.Frontend.Client
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
            
            var config = await httpClient.GetFromJsonAsync<ClientConfig>(configUri.ToString());

            builder.Services.AddTransient(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddTransient(_ => CreateWebApiClient(config));

            await builder.Build().RunAsync().ConfigureAwait(false);
        }

        private static ISmartHalWebApi CreateWebApiClient(ClientConfig config)
        {
            Console.WriteLine($"WebApiUrl: {config.WebApiUrl}");

            var apiBuilder = new ApiBuilder(new HttpClient());
            var webApi =
                apiBuilder.BuildApi<ISmartHalWebApi>(config.WebApiUrl, new JsonDataFormatter());

            return webApi;
        }
    }
}