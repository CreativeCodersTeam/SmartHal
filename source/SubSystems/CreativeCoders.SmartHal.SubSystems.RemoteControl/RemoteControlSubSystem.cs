using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Web.Api.RemoteControl;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.SubSystems.RemoteControl
{
    [UsedImplicitly]
    public class RemoteControlSubSystem : IRemoteControlSubSystem
    {
        private readonly RemoteControlHost _host;

        public RemoteControlSubSystem(IClassFactory classFactory)
        {
            _host = classFactory.Create<RemoteControlHost>();
        }
        
        public Task StartWebApi()
        {
            return _host.StartAsync();
        }

        public Task ShutdownWebApi()
        {
            return _host == null
                ? Task.CompletedTask
                : _host.StopAsync();
        }
    }
}