using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    public interface IControlCenterSubSystem
    {
        Task StartWebApiAsync();

        Task ShutdownWebApiAsync();
    }
}