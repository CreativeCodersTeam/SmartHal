using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Config.Base.Things;

namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    public interface IThingSubSystem
    {
        Task InitThingTemplatesAsync(IEnumerable<IThingTemplateDefinition> thingTemplateDefinitions);
        
        Task InitGatewayConfigurationPackagesAsync(
            IEnumerable<GatewayConfigurationPackage> gatewayConfigurationPackages);
    }
}