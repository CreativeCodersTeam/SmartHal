using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CreativeCoders.Net.Http;
using CreativeCoders.Net.WebApi.Building;
using CreativeCoders.Net.WebApi.Serialization.Json;
using CreativeCoders.SmartHal.Web.Api.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CreativeCoders.SmartHal.Web.Frontend.Shared;

namespace CreativeCoders.SmartHal.Web.Frontend.Client
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

            builder.Services.AddTransient(sp => CreateWebApiClient(config));

            await builder.Build().RunAsync();
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