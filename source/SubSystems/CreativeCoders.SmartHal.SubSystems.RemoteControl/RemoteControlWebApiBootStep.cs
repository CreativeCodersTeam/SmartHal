using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Booting;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.SubSystems.RemoteControl
{
    [UsedImplicitly]
    public class RemoteControlWebApiBootStep : IRemoteControlWebApiBootStep
    {
        private readonly IRemoteControlSubSystem _remoteControlSubSystem;

        public RemoteControlWebApiBootStep(IRemoteControlSubSystem remoteControlSubSystem)
        {
            _remoteControlSubSystem = remoteControlSubSystem;
        }
        
        public Task InitAsync()
        {
            return _remoteControlSubSystem.StartWebApi();
        }
    }
}