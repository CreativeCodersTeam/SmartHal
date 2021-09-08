using System;
using System.Threading.Tasks;
using CreativeCoders.Kernel.Services.ConsoleInterface;
using CreativeCoders.SmartHal.System.Boot;
using CreativeCoders.SmartHal.System.DefaultSystem;

namespace CreativeCoders.SmartHal.Playground.TestConsoleRunner
{
    public static class Program
    {
        public static async Task Main()
        {
            const string basePath = @"c:\temp\SmartHal\hm";
            
            Logging.InitNlog(@"c:\temp\SmartHal\hm\logs");

            var kernel = await new BootLoader<DefaultKernelBuilder>()
                .SetInstancePath(basePath)
                .ConfigureKernelBuilder(x => x.AddConsoleSupport())
                .StartKernelAsync()
                .ConfigureAwait(false);
            
            await new SmartHalShell(kernel).RunAsync().ConfigureAwait(false);
            
            await kernel.ShutdownAsync().ConfigureAwait(false);

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        
    }
}