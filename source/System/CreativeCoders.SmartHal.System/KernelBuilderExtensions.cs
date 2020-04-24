using CreativeCoders.Config;
using CreativeCoders.Config.Base;

namespace CreativeCoders.SmartHal.System
{
    public static class KernelBuilderExtensions
    {
        public static IKernelBuilder UseConfig(this IKernelBuilder kernelBuilder, IConfiguration configuration)
        {
            kernelBuilder.ConfigureServices(containerBuilder => containerBuilder.Configure(configuration));
            
            return kernelBuilder;
        }
    }
}