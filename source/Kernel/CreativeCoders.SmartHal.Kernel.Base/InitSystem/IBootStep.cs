using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.Base.InitSystem
{
    public interface IBootStep
    {
        Task ExecuteAsync();
    }
}