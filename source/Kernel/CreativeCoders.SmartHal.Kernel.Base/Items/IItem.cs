using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Items
{
    [PublicAPI]
    public interface IItem
    {
        string Name { get; }
        
        IItemType ItemType { get; }
    }
}