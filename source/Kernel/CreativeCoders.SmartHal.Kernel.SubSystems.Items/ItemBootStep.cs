using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Config.Base;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Config.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Booting;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items
{
    [UsedImplicitly]
    public class ItemBootStep : IItemBootStep
    {
        private readonly IItemSubSystem _itemSubSystem;
        
        private readonly IEnumerable<IItemConfiguration> _itemConfigurations;

        public ItemBootStep(IItemSubSystem itemSubSystem, ISettings<IItemConfiguration> itemConfigurations)
        {
            _itemSubSystem = itemSubSystem;
            _itemConfigurations = itemConfigurations.Values;
        }
        
        public Task InitItemsAsync()
        {
            return _itemConfigurations.ForEachAsync(itemConfiguration => _itemSubSystem.AddItemAsync(itemConfiguration));
        }
    }
}