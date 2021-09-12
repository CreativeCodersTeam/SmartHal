using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Web.Api.Server;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.SubSystems.WebApi
{
    [UsedImplicitly]
    [SubSystem("WebApi")]
    [DependsOn(typeof(IItemSubSystem), typeof(IThingSubSystem), typeof(ITriggerSubSystem))]
    public class WebApiSubSystem : SubSystemBase, IWebApiSubSystem
    {
        private readonly WebApiHost _webApiHost;

        public WebApiSubSystem(IClassFactory classFactory)
        {
            _webApiHost = classFactory.Create<WebApiHost>();
        }

        public async Task StartWebApiAsync()
        {
            await _webApiHost.StartAsync().ConfigureAwait(false);
        }

        public async Task ShutdownWebApiAsync()
        {
            await _webApiHost.StopAsync().ConfigureAwait(false);
        }
    }
}
