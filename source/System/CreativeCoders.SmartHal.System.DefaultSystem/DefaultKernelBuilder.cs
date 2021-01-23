using System;
using System.Collections.Generic;
using System.IO;
using CreativeCoders.Core;
using CreativeCoders.Core.IO;
using CreativeCoders.Di;
using CreativeCoders.Di.Building;
using CreativeCoders.SmartHal.Kernel;
using CreativeCoders.SmartHal.Kernel.Base;

namespace CreativeCoders.SmartHal.System.DefaultSystem
{
    public class DefaultKernelBuilder : IKernelBuilder
    {
        private Func<IDiContainerBuilder> _createDiContainerBuilder;
        
        private readonly IList<Action<IDiContainerBuilder>> _configureServicesItems;
        
        private string _configBasePath;

        public DefaultKernelBuilder()
        {
            _configureServicesItems = new List<Action<IDiContainerBuilder>>();
        }

        public ISmartHalKernel Build()
        {
            CheckBuilderRequirements();

            var diContainer = SetupDiContainer();

            var kernel = diContainer.GetInstance<ISmartHalKernel>();

            return kernel;
        }

        private IDiContainer SetupDiContainer()
        {
            var containerBuilder = _createDiContainerBuilder();

            if (containerBuilder == null)
            {
                throw new InvalidOperationException("No DI container builder can be created");
            }

            containerBuilder
                .AddSingleton<ISmartHalEnvironment>(_ => new SmartHalEnvironment {InstanceConfigPath = _configBasePath})
                .SetupForKernel();

            _configureServicesItems.ForEach(configureServices => configureServices(containerBuilder));

            return containerBuilder.Build();
        }

        private void CheckBuilderRequirements()
        {
            if (_createDiContainerBuilder == null)
            {
                throw new InvalidOperationException("No DI container builder creator set.");
            }

            if (_configBasePath == null)
            {
                throw new InvalidOperationException("No instance config path specified");
            }
            
            if (!FileSys.Directory.Exists(_configBasePath))
            {
                throw new DirectoryNotFoundException($"Instance config path '{_configBasePath}' not found");
            }
        }
        
        public IKernelBuilder UseDiContainerBuilder(Func<IDiContainerBuilder> createDiContainerBuilder)
        {
            _createDiContainerBuilder = createDiContainerBuilder;
            
            return this;
        }

        public IKernelBuilder SetInstanceConfigPath(string configBasePath)
        {
            Ensure.IsNotNullOrWhitespace(configBasePath, nameof(configBasePath));
            
            _configBasePath = configBasePath;

            return this;
        }

        public IKernelBuilder ConfigureServices(Action<IDiContainerBuilder> configureServices)
        {
            _configureServicesItems.Add(configureServices);

            return this;
        }
    }
}