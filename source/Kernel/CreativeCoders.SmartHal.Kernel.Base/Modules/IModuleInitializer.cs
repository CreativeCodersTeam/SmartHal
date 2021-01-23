using Microsoft.Extensions.DependencyInjection;

namespace CreativeCoders.SmartHal.Kernel.Base.Modules
{
    public interface IModuleInitializer
    {
        void RegisterServices(IServiceCollection services);
    }
}