using CreativeCoders.SmartHal.Config.Base.Items;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.Building
{
    public interface IItemBuilder
    {
        Item Build(IItemConfiguration itemConfiguration);
    }
}