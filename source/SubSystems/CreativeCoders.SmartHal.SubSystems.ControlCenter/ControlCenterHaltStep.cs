using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.SubSystems.ControlCenter
{
    [UsedImplicitly]
    [InitSystemStep(typeof(IControlCenterSubSystem))]
    public class ControlCenterHaltStep : IHaltStep
    {
        private readonly IControlCenterSubSystem _controlCenterSubSystem;

        public ControlCenterHaltStep(IControlCenterSubSystem controlCenterSubSystem)
        {
            _controlCenterSubSystem = controlCenterSubSystem;
        }

        public Task ExecuteAsync()
        {
            return _controlCenterSubSystem.ShutdownWebApi();
        }
    }
}