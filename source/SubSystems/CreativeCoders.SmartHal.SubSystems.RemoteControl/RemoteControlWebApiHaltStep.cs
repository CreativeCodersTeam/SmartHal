using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.SubSystems.RemoteControl
{
    [UsedImplicitly]
    [InitSystemStep(typeof(IRemoteControlSubSystem))]
    public class RemoteControlWebApiHaltStep : IHaltStep
    {
        private readonly IRemoteControlSubSystem _remoteControlSubSystem;

        public RemoteControlWebApiHaltStep(IRemoteControlSubSystem remoteControlSubSystem)
        {
            _remoteControlSubSystem = remoteControlSubSystem;
        }

        public async Task ExecuteAsync()
        {
            await _remoteControlSubSystem.ShutdownWebApiAsync().ConfigureAwait(false);
        }
    }
}