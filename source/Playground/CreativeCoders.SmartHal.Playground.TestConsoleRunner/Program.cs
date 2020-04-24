using System;
using System.Threading.Tasks;
using CreativeCoders.Di.SimpleInjector;
using CreativeCoders.Kernel.Services.ConsoleInterface;
using CreativeCoders.SmartHal.Config.FileSystem.Building;
using CreativeCoders.SmartHal.System;
using CreativeCoders.SmartHal.System.DefaultSystem;
using SimpleInjector;

namespace CreativeCoders.SmartHal.Playground.TestConsoleRunner
{
    public static class Program
    {
        public static async Task Main()
        {
            const string basePath = @"c:\temp\SmartHal\hm";
            
            Logging.InitNlog(@"c:\temp\SmartHal\hm\logs");
            
            var kernel = new DefaultKernelBuilder()
                .UseDiContainerBuilder(() => new SimpleInjectorDiContainerBuilder(new Container()))
                .UseConfig(new FileConfigurationBuilder(basePath, true).Build())
                .AddConsoleSupport()
                .Build();
            
            await kernel.InitAsync().ConfigureAwait(false);
            
            await kernel.StartAsync().ConfigureAwait(false);

            await new SmartHalShell(kernel).RunAsync().ConfigureAwait(false);
            
            await kernel.ShutdownAsync().ConfigureAwait(false);

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        
    }
}