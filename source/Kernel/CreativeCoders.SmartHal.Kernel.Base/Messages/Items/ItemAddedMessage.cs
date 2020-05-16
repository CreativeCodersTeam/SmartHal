using CreativeCoders.SmartHal.Kernel.Base.Items;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Messages.Items
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