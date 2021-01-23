using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.IO;
using CreativeCoders.Core.Reflection;
using CreativeCoders.Di.MsServiceProvider;
using CreativeCoders.SmartHal.Config.FileSystem.Building;
using CreativeCoders.SmartHal.Kernel.Base;
using CreativeCoders.SmartHal.Kernel.Base.Modules;
using CreativeCoders.SmartHal.System.Boot.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace CreativeCoders.SmartHal.System.Boot
{
    public class BootLoader<T>
        where T : class, IKernelBuilder, new()
    {
        private readonly T _kernelBuilder;
        
        private string _instancePath;
        
        private readonly List<Action<IKernelBuilder>> _configureKernelBuilderActions;

        public BootLoader() : this(new T())
        {
        }
        
        public BootLoader(T kernelBuilder)
        {
            Ensure.IsNotNull(kernelBuilder, nameof(kernelBuilder));
            
            _kernelBuilder = kernelBuilder;
            _configureKernelBuilderActions = new List<Action<IKernelBuilder>>();
        }

        public BootLoader<T> SetInstancePath(string instancePath)
        {
            _instancePath = instancePath;

            return this;
        }

        public async Task<ISmartHalKernel> StartKernelAsync()
        {
            if (!FileSys.Directory.Exists(_instancePath))
            {
                throw new DirectoryNotFoundException("SmartHal instance path not set or not exists");
            }

            await LoadModulesAsync();

            var services = new ServiceCollection();
            
            RegisterServices(services);
            
            var kernelBuilder = _kernelBuilder
                .UseDiContainerBuilder(() => new ServiceProviderDiContainerBuilder(services))
                .SetInstanceConfigPath(_instancePath)
                .UseConfig(new FileConfigurationBuilder(_instancePath, true).Build());
            
            _configureKernelBuilderActions.ForEach(x => x(kernelBuilder));
                
            var kernel = kernelBuilder.Build();

            await kernel.InitAsync().ConfigureAwait(false);

            await kernel.StartAsync().ConfigureAwait(false);

            return kernel;
        }
        
        private static void RegisterServices(IServiceCollection services)
        {
            var moduleInitializerTypes = typeof(IModuleInitializer).GetImplementations();
            
            moduleInitializerTypes.ForEach(x =>
            {
                var initializer = Activator.CreateInstance(x) as IModuleInitializer;
                
                initializer?.RegisterServices(services);
            });
        }
        
        private async Task LoadModulesAsync()
        {
            var modulesLoader = new ModulesLoader(_instancePath);

            await modulesLoader.LoadAllModulesAsync();
        }

        public BootLoader<T> ConfigureKernelBuilder(Action<IKernelBuilder> configureKernelBuilder)
        {
            _configureKernelBuilderActions.Add(configureKernelBuilder);

            return this;
        }
    }
}
