using CreativeCoders.Di.Building;
using CreativeCoders.SmartHal.System;

namespace CreativeCoders.Kernel.Services.ConsoleInterface
{
    public static class KernelBuilderExtensions
    {
        public static IKernelBuilder AddConsoleSupport(this IKernelBuilder kernelBuilder)
        {
            kernelBuilder.ConfigureServices(x => x.AddScoped<IConsoleService, ConsoleService>());

            kernelBuilder.ConfigureServices(x => x.AddScopedCollectionFor<IConsoleCommand>());
            
            return kernelBuilder;
        }
    }
}