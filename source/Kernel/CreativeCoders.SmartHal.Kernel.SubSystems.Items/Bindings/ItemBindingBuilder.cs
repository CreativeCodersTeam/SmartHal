using System;
using System.Collections.Generic;
using System.Linq;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.Bindings
{
    [UsedImplicitly]
    public class ItemBindingBuilder : IItemBindingBuilder
    {
        private static readonly ILogger Log = LogManager.GetLogger<ItemBindingBuilder>();
        
        private readonly IMessageHub _messageHub;

        public ItemBindingBuilder(IMessageHub messageHub)
        {
            _messageHub = messageHub;
        }
        
        public IItemBinding Build(string dataSource, string itemName, IReadOnlyCollection<string> channelIds)
        {
            if (dataSource?.Equals("InMemory", StringComparison.CurrentCultureIgnoreCase) == true)
            {
                return new InMemoryBinding(itemName, _messageHub);
            }

            if (channelIds.Count > 0)
            {
                return BuildChannelBinding(itemName, channelIds);
            }

            Log.Warn("Unknown data source or no channels for item specified");
                
            return new NullBinding();
        }

        private IItemBinding BuildChannelBinding(string itemName, IReadOnlyCollection<string> channelIds)
        {
            return channelIds.Count switch
            {
                1 => new ItemBinding(itemName, channelIds.First(), _messageHub),
                _ => new ItemMultiBinding(
                    channelIds
                        .Select(channelId => new ItemBinding(itemName, channelId, _messageHub))
                        .ToArray())
            };
        }
    }
}