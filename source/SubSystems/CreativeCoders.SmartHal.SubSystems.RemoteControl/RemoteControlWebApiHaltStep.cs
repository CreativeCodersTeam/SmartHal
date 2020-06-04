using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Halting;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;

namespace CreativeCoders.SmartHal.SubSystems.RemoteControl
{
    public class RemoteControlWebApiHaltStep : IRemoteControlWebApiHaltStep
    {
        private readonly IRemoteControlSubSystem _remoteControlSubSystem;

        public RemoteControlWebApiHaltStep(IRemoteControlSubSystem remoteControlSubSystem)
        {
            _remoteControlSubSystem = remoteControlSubSystem;
        }

        public Task HaltAsync()
        {
            return _remoteControlSubSystem.ShutdownWebApi();
        }
    }
}