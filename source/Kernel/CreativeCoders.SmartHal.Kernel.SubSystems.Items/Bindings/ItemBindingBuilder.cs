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
        
        public IItemBinding Build(IReadOnlyCollection<string> channelIds)
        {
            switch (channelIds.Count)
            {
                case 0:
                    Log.Warn("No channels for item specified");
                
                    return new NullBinding();
                case 1:
                    return new ItemBinding(channelIds.First(), _messageHub);
                default:
                    return new ItemMultiBinding(channelIds.Select(channelId => new ItemBinding(channelId, _messageHub)).ToArray());
            }
        }
    }
}