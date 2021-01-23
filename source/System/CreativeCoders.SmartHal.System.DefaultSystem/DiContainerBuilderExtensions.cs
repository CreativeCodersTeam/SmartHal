using CreativeCoders.CodeCompilation;
using CreativeCoders.CodeCompilation.Roslyn;
using CreativeCoders.Di.Building;
using CreativeCoders.Net;
using CreativeCoders.Scripting.CSharp;
using CreativeCoders.SmartHal.Kernel;
using CreativeCoders.SmartHal.Kernel.Base;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.Requests;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Kernel.InitSystem;
using CreativeCoders.SmartHal.Kernel.Messaging;
using CreativeCoders.SmartHal.Kernel.SubSystems.Drivers;
using CreativeCoders.SmartHal.Kernel.SubSystems.Items;
using CreativeCoders.SmartHal.Kernel.SubSystems.Items.Bindings;
using CreativeCoders.SmartHal.Kernel.SubSystems.Items.Building;
using CreativeCoders.SmartHal.Kernel.SubSystems.Scripting;
using CreativeCoders.SmartHal.Kernel.SubSystems.Things;
using CreativeCoders.SmartHal.Kernel.SubSystems.Things.Building;
using CreativeCoders.SmartHal.Kernel.SubSystems.Things.Repositories;
using CreativeCoders.SmartHal.Kernel.SubSystems.Triggers;
using CreativeCoders.SmartHal.Scripting;
using CreativeCoders.SmartHal.Scripting.ActionScripts;
using CreativeCoders.SmartHal.Scripting.ActionScripts.Triggers;
using CreativeCoders.SmartHal.Scripting.Api;
using CreativeCoders.SmartHal.Scripting.Base;
using CreativeCoders.SmartHal.Scripting.Base.ActionScripts.Triggers;
using CreativeCoders.SmartHal.Scripting.Base.Api;
using CreativeCoders.SmartHal.SubSystems.ControlCenter;
using CreativeCoders.SmartHal.SubSystems.RemoteControl;

namespace CreativeCoders.SmartHal.System.DefaultSystem
{
    public static class DiContainerBuilderExtensions
    {
        public static void SetupForKernel(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .SetupCore()
                .SetupDriversSubSystem()
                .SetupThingsSubSystem()
                .SetupItemSubSystem()
                .SetupScriptingSubSystem()
                .SetupTriggerSubSystem()
                .SetupRemoteControlSubSystem()
                .SetupControlCenterSubSystem()
                .SetupBoot()
                .SetupHalt();
        }



        private static IDiContainerBuilder SetupCore(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton<INetworkInfo, NetworkInfo>();

            return containerBuilder;
        }

        private static IDiContainerBuilder SetupControlCenterSubSystem(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton<IControlCenterSubSystem, ControlCenterSubSystem>();

            return containerBuilder;
        }

        private static IDiContainerBuilder SetupRemoteControlSubSystem(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton<IRemoteControlSubSystem, RemoteControlSubSystem>();

            return containerBuilder;
        }
        
        private static IDiContainerBuilder SetupTriggerSubSystem(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton<ITriggerSubSystem, TriggerSubSystem>()
                .AddSingleton<ITriggerRepository, TriggerRepository>();

            return containerBuilder;
        }
        
        private static IDiContainerBuilder SetupScriptingSubSystem(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton<IScriptingSubSystem, ScriptingSubSystem>()
                .AddSingleton<IScriptingCore, ScriptingCore>()
                .AddSingleton<IActionScriptCore, ActionScriptCore>()
                .AddSingleton<ICompilerFactory, RoslynCompilerFactory>()
                .AddSingleton<CSharpScriptRuntime<ActionScriptImplementation>>()
                .AddSingleton<ActionScriptImplementation>()
                .AddSingleton<ActionScriptClassTemplate>()
                .AddTransient<IItemsScriptApi, ItemsScriptApi>()
                .AddTransient<ITriggerApi, TriggerApi>();
            
            return containerBuilder;
        }

        private static IDiContainerBuilder SetupDriversSubSystem(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton<IDriverSubSystem, DriverSubSystem>()
                .AddSingleton<IDriverLoader, DriverLoader>();
            
            return containerBuilder;
        }

        private static IDiContainerBuilder SetupBoot(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton<ISmartHalKernel, SmartHalKernel>()
                .AddSingleton<IMessageHub, MessageHub>()
                .AddSingleton<IKernelRequestDispatcher, KernelRequestDispatcher>()
                .AddSingleton<IKernelBootProcess, KernelBootProcess>()
                .AddSingletonCollectionFor<IBootStep>(true)
                .AddSingletonCollectionFor<ISubSystem>(true)
                .AddSingleton<ISubSystemInitSystem, SubSystemInitSystem>();

            return containerBuilder;
        }

        private static void SetupHalt(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton<IKernelHaltProcess, KernelHaltProcess>()
                .AddSingletonCollectionFor<IHaltStep>();
        }

        private static IDiContainerBuilder SetupThingsSubSystem(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton<IThingSubSystem, ThingSubSystem>()
                .AddSingleton<IGatewayRepository, GatewayRepository>()
                .AddSingleton<IThingRepository, ThingRepository>()
                .AddSingleton<IThingTemplateRepository, ThingTemplateRepository>()
                .AddSingleton<IThingChannelRepository, ThingChannelRepository>()
                .AddSingleton<IGatewayBuilder, GatewayBuilder>()
                .AddSingleton<IThingBuilder, ThingBuilder>()
                .AddSingleton<IThingChannelBuilder, ThingChannelBuilder>();

            return containerBuilder;
        }

        private static IDiContainerBuilder SetupItemSubSystem(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton<IItemSubSystem, ItemSubSystem>()
                .AddSingleton<IItemRepository, ItemRepository>()
                .AddSingletonCollectionFor<IItemType>()
                .AddSingleton<IItemTypeRegistrations, ItemTypeRegistrations>()
                .AddSingleton<IItemBuilder, ItemBuilder>()
                .AddSingleton<IItemBindingBuilder, ItemBindingBuilder>();
            
            return containerBuilder;
        }
    }
}