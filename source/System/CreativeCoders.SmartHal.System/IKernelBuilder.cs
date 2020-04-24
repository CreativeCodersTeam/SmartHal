using System;
using CreativeCoders.Di.Building;
using CreativeCoders.SmartHal.Kernel.Base;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.System
{
    [PublicAPI]
    public interface IKernelBuilder
    {
        ISmartHalKernel Build();

        IKernelBuilder ConfigureServices(Action<IDiContainerBuilder> configureServices);
    }
}