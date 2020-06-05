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

        public Task StartWebApi()
        {
            return _controlCenterHost.StartAsync();
        }

        public Task ShutdownWebApi()
        {
            return _controlCenterHost.StopAsync();
        }
    }
}
