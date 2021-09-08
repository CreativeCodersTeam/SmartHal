using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.SubSystems.RemoteControl
{
    [UsedImplicitly]
    [InitSystemStep(typeof(IRemoteControlSubSystem))]
    public class RemoteControlWebApiBootStep : IBootStep
    {
        private readonly IRemoteControlSubSystem _remoteControlSubSystem;

        public RemoteControlWebApiBootStep(IRemoteControlSubSystem remoteControlSubSystem)
        {
            _remoteControlSubSystem = remoteControlSubSystem;
        }
        
        public async Task ExecuteAsync()
        {
            await _remoteControlSubSystem.StartWebApiAsync().ConfigureAwait(false);
        }
    }
}