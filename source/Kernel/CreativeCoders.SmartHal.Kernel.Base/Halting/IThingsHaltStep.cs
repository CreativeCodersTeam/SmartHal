using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.Base.Halting
{
    public interface IThingsHaltStep
    {
        Task HaltAsync();
    }
}