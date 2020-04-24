using System;
using System.Collections.Generic;
using CreativeCoders.Core;
using CreativeCoders.Di;
using CreativeCoders.Di.Building;
using CreativeCoders.SmartHal.Kernel.Base;

namespace CreativeCoders.SmartHal.System.DefaultSystem
{
    public class DefaultKernelBuilder : IKernelBuilder
    {
        private Func<IDiContainerBuilder> _createDiContainerBuilder;
        
        private readonly IList<Action<IDiContainerBuilder>> _configureServicesItems;

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
        }
        
        public IKernelBuilder UseDiContainerBuilder(Func<IDiContainerBuilder> createDiContainerBuilder)
        {
            _createDiContainerBuilder = createDiContainerBuilder;
            
            return this;
        }

        public IKernelBuilder ConfigureServices(Action<IDiContainerBuilder> configureServices)
        {
            _configureServicesItems.Add(configureServices);

            return this;
        }
    }
}