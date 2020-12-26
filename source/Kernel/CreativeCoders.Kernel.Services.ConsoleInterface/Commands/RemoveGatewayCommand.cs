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

        public override Task ExecuteAsync(string[] arguments)
        {
            var gatewayId = arguments.FirstOrDefault();

            if (gatewayId.IsNullOrWhiteSpace())
            {
                Output.WriteLine("No gateway id given");
                return Task.CompletedTask;
            }

            var gateway = _gatewayRepository.FirstOrDefault(x => x.Id.Equals(gatewayId));

            if (gateway == null)
            {
                Output.WriteLine($"Gateway '{gatewayId}' not found");
                return Task.CompletedTask;
            }

            return _gatewayRepository.RemoveAsync(gateway);
        }

        public override string CommandName => "remove-gateway";
    }
}