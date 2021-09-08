using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Web.Api.ControlCenter;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.SubSystems.ControlCenter
{
    [UsedImplicitly]
    [SubSystem("ControlCenter")]
    [DependsOn(typeof(IItemSubSystem), typeof(IThingSubSystem), typeof(ITriggerSubSystem))]
    public class ControlCenterSubSystem : SubSystemBase, IControlCenterSubSystem
    {
        private readonly ControlCenterHost _controlCenterHost;

        public ControlCenterSubSystem(IClassFactory classFactory)
        {
            _controlCenterHost = classFactory.Create<ControlCenterHost>();
        }

        public async Task StartWebApiAsync()
        {
            await _controlCenterHost.StartAsync().ConfigureAwait(false);
        }

        public async Task ShutdownWebApiAsync()
        {
            await _controlCenterHost.StopAsync().ConfigureAwait(false);
        }
    }
}
