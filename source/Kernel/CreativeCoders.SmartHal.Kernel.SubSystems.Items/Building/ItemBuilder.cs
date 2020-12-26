using System;
using System.Linq;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Config.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.SubSystems.Items.Bindings;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.Building
{
    [UsedImplicitly]
    public class ItemBuilder : IItemBuilder
    {
        private static readonly ILogger Log = LogManager.GetLogger<ItemBuilder>();
        
        private readonly IItemTypeRegistrations _itemTypeRegistrations;
        
        private readonly IMessageHub _messageHub;
        
        private readonly IItemBindingBuilder _itemBindingBuilder;
        
        public ItemBuilder(IItemTypeRegistrations itemTypeRegistrations, IMessageHub messageHub, IItemBindingBuilder itemBindingBuilder)
        {
            _itemTypeRegistrations = itemTypeRegistrations;
            _messageHub = messageHub;
            _itemBindingBuilder = itemBindingBuilder;
        }
        
        public Item Build(IItemConfiguration itemConfiguration)
        {
            var itemType = _itemTypeRegistrations.ItemTypes.FirstOrDefault(x =>
                x.Name.Equals(itemConfiguration.ItemType, StringComparison.InvariantCulture));

            if (itemType == null)
            {
                Log.Warn($"Item type '{itemConfiguration.ItemType}' not found");
                
                return null;
            }

            var binding = _itemBindingBuilder.Build(itemConfiguration.DataSource, itemConfiguration.Name, itemConfiguration.ChannelIds); 
            
            var item = new Item(itemConfiguration.Name, itemType, binding, _messageHub);
            
            Log.Info($"Item '{item.Name}' created");
            
            return item;
        }
    }
}