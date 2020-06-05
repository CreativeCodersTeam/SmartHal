using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.InitSystem
{
    public interface ISubSystemInitSystem
    {
        Task ExecuteBootStepsAsync();

        Task ExecuteHaltStepsAsync();
    }
}