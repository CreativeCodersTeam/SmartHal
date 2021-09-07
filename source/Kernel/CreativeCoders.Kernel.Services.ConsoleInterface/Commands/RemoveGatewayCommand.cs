using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using JetBrains.Annotations;

namespace CreativeCoders.Kernel.Services.ConsoleInterface.Commands
{
    [UsedImplicitly]
    public class RemoveGatewayCommand : ConsoleCommandBase
    {
        private readonly IGatewayRepository _gatewayRepository;

        public RemoveGatewayCommand(IGatewayRepository gatewayRepository)
        {
            _gatewayRepository = gatewayRepository;
        }

        public override async Task ExecuteAsync(string[] arguments)
        {
            var gatewayId = arguments.FirstOrDefault();

            if (gatewayId.IsNullOrWhiteSpace())
            {
                Output.WriteLine("No gateway id given");
                return;
            }

            var gateway = _gatewayRepository.FirstOrDefault(x => x.Id.Equals(gatewayId));

            if (gateway == null)
            {
                Output.WriteLine($"Gateway '{gatewayId}' not found");
                return;
            }

            await _gatewayRepository.RemoveAsync(gateway).ConfigureAwait(false);
        }

        public override string CommandName => "remove-gateway";
    }
}