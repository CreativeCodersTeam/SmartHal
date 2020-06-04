using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    public interface IRemoteControlSubSystem
    {
        Task StartWebApi();

        Task ShutdownWebApi();
    }
}