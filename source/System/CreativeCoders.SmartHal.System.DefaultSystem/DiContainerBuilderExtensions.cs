using CreativeCoders.CodeCompilation;
using CreativeCoders.CodeCompilation.Roslyn;
using CreativeCoders.Di.Building;
using CreativeCoders.Net;
using CreativeCoders.Scripting.CSharp;
using CreativeCoders.SmartHal.Kernel;
using CreativeCoders.SmartHal.Kernel.Base;
using CreativeCoders.SmartHal.Kernel.Base.Booting;
using CreativeCoders.SmartHal.Kernel.Base.Halting;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.Requests;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Kernel.Boot;
using CreativeCoders.SmartHal.Kernel.Halt;
using CreativeCoders.SmartHal.Kernel.Messaging;
using CreativeCoders.SmartHal.Kernel.SubSystems.Assemblies;
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
using CreativeCoders.SmartHal.SubSystems.RemoteControl;

namespace CreativeCoders.SmartHal.System.DefaultSystem
{
    public static class DiContainerBuilderExtensions
    {
        public static void SetupForKernel(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .SetupCore()
                .SetupBoot()
                .SetupAssemblySubSystem()
                .SetupDriversSubSystem()
                .SetupThingsSubSystem()
                .SetupItemSubSystem()
                .SetupScriptingSubSystem()
                .SetupTriggerSubSystem()
                .SetupRemoteControlWebApiSubSystem()
                //.SetupControlCenterWebApiSubSystem()
                .SetupHalt();
        }



        private static IDiContainerBuilder SetupCore(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddScoped<INetworkInfo, NetworkInfo>();

            return containerBuilder;
        }

        private static IDiContainerBuilder SetupRemoteControlWebApiSubSystem(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddScoped<IRemoteControlSubSystem, RemoteControlSubSystem>();

            return containerBuilder;
        }
        
        private static IDiContainerBuilder SetupTriggerSubSystem(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddScoped<ITriggerSubSystem, TriggerSubSystem>()
                .AddScoped<ITriggerRepository, TriggerRepository>();

            return containerBuilder;
        }
        
        private static IDiContainerBuilder SetupScriptingSubSystem(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddScoped<IScriptingSubSystem, ScriptingSubSystem>()
                .AddScoped<IScriptingCore, ScriptingCore>()
                .AddScoped<IActionScriptCore, ActionScriptCore>()
                .AddScoped<ICompilerFactory, RoslynCompilerFactory>()
                .AddScoped<CSharpScriptRuntime<ActionScriptImplementation>>()
                .AddScoped<ActionScriptImplementation>()
                .AddScoped<ActionScriptClassTemplate>()
                .AddTransient<IItemsScriptApi, ItemsScriptApi>()
                .AddTransient<ITriggerApi, TriggerApi>();
            
            return containerBuilder;
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
                .AddScoped<IThingsBootStep, ThingsBootStep>()
                .AddScoped<IItemBootStep, ItemBootStep>()
                .AddScoped<IScriptingBootStep, ScriptingBootStep>()
                .AddScoped<IRemoteControlWebApiBootStep, RemoteControlWebApiBootStep>();

            return containerBuilder;
        }

        private static void SetupHalt(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddScoped<IKernelHaltProcess, KernelHaltProcess>()
                .AddScoped<IThingsHaltStep, ThingsHaltStep>()
                .AddScoped<IItemHaltStep, ItemHaltStep>()
                .AddScoped<ITriggersHaltStep, TriggersHaltStep>()
                .AddScoped<IRemoteControlWebApiHaltStep, RemoteControlWebApiHaltStep>();
        }

        private static IDiContainerBuilder SetupThingsSubSystem(this IDiContainerBuilder containerBuilder)
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

        private static IDiContainerBuilder SetupItemSubSystem(this IDiContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddScoped<IItemSubSystem, ItemSubSystem>()
                .AddScoped<IItemRepository, ItemRepository>()
                .AddScopedCollectionFor<IItemType>()
                .AddScoped<IItemTypeRegistrations, ItemTypeRegistrations>()
                .AddScoped<IItemBuilder, ItemBuilder>()
                .AddScoped<IItemBindingBuilder, ItemBindingBuilder>();
            
            return containerBuilder;
        }
    }
}