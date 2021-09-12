using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.SubSystems.WebApi
{
    [UsedImplicitly]
    [InitSystemStep(typeof(IWebApiSubSystem))]
    public class WebApiHaltStep : IHaltStep
    {
        private readonly IWebApiSubSystem _webApiSubSystem;

        public WebApiHaltStep(IWebApiSubSystem webApiSubSystem)
        {
            _webApiSubSystem = webApiSubSystem;
        }

        public async Task ExecuteAsync()
        {
            await _webApiSubSystem.ShutdownWebApiAsync().ConfigureAwait(false);
        }
    }
}