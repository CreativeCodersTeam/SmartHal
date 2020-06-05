using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.SubSystems.ControlCenter
{
    [UsedImplicitly]
    [InitSystemStep(typeof(IControlCenterSubSystem))]
    public class ControlCenterBootStep : IBootStep
    {
        private readonly IControlCenterSubSystem _controlCenterSubSystem;

        public ControlCenterBootStep(IControlCenterSubSystem controlCenterSubSystem)
        {
            _controlCenterSubSystem = controlCenterSubSystem;
        }

        public async Task ExecuteAsync()
        {
            await _controlCenterSubSystem.StartWebApi().ConfigureAwait(false);
        }
    }
}