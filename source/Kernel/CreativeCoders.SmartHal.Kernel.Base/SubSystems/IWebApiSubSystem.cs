using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    public interface IWebApiSubSystem
    {
        Task StartWebApiAsync();

        Task ShutdownWebApiAsync();
    }
}