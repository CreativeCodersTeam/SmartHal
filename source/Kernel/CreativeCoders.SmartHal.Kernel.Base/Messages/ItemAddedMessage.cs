using CreativeCoders.SmartHal.Kernel.Base.Items;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Messages
{
    [PublicAPI]
    public class ItemAddedMessage : SmartHalMessageBase
    {
        public ItemAddedMessage(IItem item)
        {
            Item = item;
        }
        
        public IItem Item { get; }
    }
}