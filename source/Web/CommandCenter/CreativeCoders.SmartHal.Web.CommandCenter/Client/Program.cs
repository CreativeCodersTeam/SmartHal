using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Web.CommandCenter.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var startup = new Startup(builder.HostEnvironment);

            startup.ConfigureServices(builder.Services);

            //var cultureInfo = new CultureInfo("de-DE");
            var cultureInfo = new CultureInfo("en-US");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            await builder.Build().RunAsync();
        }
    }
}
