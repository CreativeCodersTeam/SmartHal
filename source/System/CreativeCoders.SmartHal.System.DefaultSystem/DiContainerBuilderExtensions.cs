using CreativeCoders.Di.Building;
using CreativeCoders.SmartHal.Kernel;
using CreativeCoders.SmartHal.Kernel.Base;
using CreativeCoders.SmartHal.Kernel.Base.Booting;
using CreativeCoders.SmartHal.Kernel.Base.Halting;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.Requests;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Kernel.Boot;
using CreativeCoders.SmartHal.Kernel.Halt;
using CreativeCoders.SmartHal.Kernel.Messaging;
using CreativeCoders.SmartHal.Kernel.SubSystems.Assemblies;
using CreativeCoders.SmartHal.Kernel.SubSystems.Drivers;
using CreativeCoders.SmartHal.Kernel.SubSystems.Things;
using CreativeCoders.SmartHal.Kernel.SubSystems.Things.Building;
using CreativeCoders.SmartHal.Kernel.SubSystems.Things.Repositories;

namespace CreativeCoders.SmartHal.System.DefaultSystem
{
    public static class DiContainerBuilderExtensions
    {
        public static void SetupForKernel(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .SetupBoot()
                .SetupAssemblySubSystem()
                .SetupDriversSubSystem()
                .SetupTingsSubSystem()
                .SetupHalt();
        }

        private static IDiContainerBuilder SetupAssemblySubSystem(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddScoped<IAssemblyLoader, AssemblyLoader>()
                .AddScopedCollection<IAssemblyReferenceLoader>(
                    typeof(FileAssemblyReferenceLoader))
                .AddScoped<IAssemblySubSystem, AssemblySubSystem>();
                
            return containerBuilder;
        }

        private static IDiContainerBuilder SetupDriversSubSystem(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddScoped<IDriverSubSystem, DriverSubSystem>()
                .AddScoped<IDriverLoader, DriverLoader>();
            
            return containerBuilder;
        }

        private static IDiContainerBuilder SetupBoot(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddScoped<ISmartHalKernel, SmartHalKernel>()
                .AddScoped<IMessageHub, MessageHub>()
                .AddScoped<IKernelRequestDispatcher, KernelRequestDispatcher>()
                .AddScoped<IKernelBootProcess, KernelBootProcess>()
                .AddScoped<IAssemblyBootStep, AssemblyBootStep>()
                .AddScoped<IDriverBootStep, DriverBootStep>()
                .AddScoped<IThingsBootStep, ThingsBootStep>();

            return containerBuilder;
        }

        private static void SetupHalt(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddScoped<IKernelHaltProcess, KernelHaltProcess>()
                .AddScoped<IThingsHaltStep, ThingsHaltStep>();
        }

        private static IDiContainerBuilder SetupTingsSubSystem(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddScoped<IThingSubSystem, ThingSubSystem>()
                .AddScoped<IGatewayRepository, GatewayRepository>()
                .AddScoped<IThingRepository, ThingRepository>()
                .AddScoped<IThingTemplateRepository, ThingTemplateRepository>()
                .AddScoped<IThingChannelRepository, ThingChannelRepository>()
                .AddScoped<IGatewayBuilder, GatewayBuilder>()
                .AddScoped<IThingBuilder, ThingBuilder>()
                .AddScoped<IThingChannelBuilder, ThingChannelBuilder>();

            return containerBuilder;
        }
    }
}