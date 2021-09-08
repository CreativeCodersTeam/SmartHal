using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Web.Api.RemoteControl;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.SubSystems.RemoteControl
{
    [UsedImplicitly]
    [SubSystem("RemoteControl")]
    [DependsOn(typeof(IItemSubSystem))]
    public class RemoteControlSubSystem : SubSystemBase, IRemoteControlSubSystem
    {
        private readonly RemoteControlHost _host;

        public RemoteControlSubSystem(IClassFactory classFactory)
        {
            _host = classFactory.Create<RemoteControlHost>();
        }
        
        public async Task StartWebApiAsync()
        {
            await _host.StartAsync().ConfigureAwait(false);
        }

        public async Task ShutdownWebApiAsync()
        {
            if (_host != null)
            {
                await _host.StopAsync().ConfigureAwait(false);
            }
        }
    }
}