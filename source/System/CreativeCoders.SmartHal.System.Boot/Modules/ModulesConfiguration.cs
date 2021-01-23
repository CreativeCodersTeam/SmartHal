using System.Collections.Generic;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.System.Boot.Modules
{
    [PublicAPI]
    public class ModulesConfiguration
    {
        public string ModulesBasePath { get; set; } = string.Empty;

        public IEnumerable<ModuleAssemblyReference> Modules { get; set; } = new List<ModuleAssemblyReference>();
    }
}