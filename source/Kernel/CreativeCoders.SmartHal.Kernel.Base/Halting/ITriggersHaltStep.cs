using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.Base.Halting
{
    public interface ITriggersHaltStep
    {
        Task HaltAsync();
    }
}