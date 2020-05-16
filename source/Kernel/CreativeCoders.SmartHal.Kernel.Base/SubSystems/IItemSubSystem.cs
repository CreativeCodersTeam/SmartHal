using System.Threading.Tasks;
using CreativeCoders.SmartHal.Config.Base.Items;

namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    public interface IItemSubSystem
    {
        Task AddItemAsync(IItemConfiguration itemConfiguration);

        void SendCommand(string itemName, object commandValue);
    }
}