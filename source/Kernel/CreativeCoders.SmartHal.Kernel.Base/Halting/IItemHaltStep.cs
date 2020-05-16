using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.Base.Halting
{
    public interface IItemHaltStep
    {
        Task HaltAsync();
    }
}