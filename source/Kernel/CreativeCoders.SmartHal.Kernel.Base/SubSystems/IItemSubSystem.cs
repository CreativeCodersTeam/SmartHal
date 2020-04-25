using CreativeCoders.SmartHal.Config.Base.Items;

namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    public interface IItemSubSystem
    {
        void AddItem(IItemConfiguration itemConfiguration);
    }
}