using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Config.Base;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Config.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items
{
    [UsedImplicitly]
    [InitSystemStep(typeof(IItemSubSystem))]
    public class ItemsBootStep : IBootStep
    {
        private readonly IItemSubSystem _itemSubSystem;
        
        private readonly IEnumerable<IItemConfiguration> _itemConfigurations;

        public ItemsBootStep(IItemSubSystem itemSubSystem, ISettings<IItemConfiguration> itemConfigurations)
        {
            _itemSubSystem = itemSubSystem;
            _itemConfigurations = itemConfigurations.Values;
        }
        
        public Task ExecuteAsync()
        {
            return _itemConfigurations.ForEachAsync(itemConfiguration => _itemSubSystem.AddItemAsync(itemConfiguration));
        }
    }
}