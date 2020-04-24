using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using JetBrains.Annotations;

namespace CreativeCoders.Kernel.Services.ConsoleInterface.Commands
{
    [UsedImplicitly]
    public class ListGatewaysCommand : ConsoleCommandBase
    {
        private readonly IGatewayRepository _gatewayRepository;

        public ListGatewaysCommand(IGatewayRepository gatewayRepository)
        {
            _gatewayRepository = gatewayRepository;
        }
        
        public override Task ExecuteAsync(IReadOnlyCollection<string> arguments)
        {
            var maxNameWidth = _gatewayRepository.Max(x => x.Name.Length) + 2;
            var maxIdWidth = _gatewayRepository.Max(x => x.Id.ToString().Length) + 2;

            Output.WriteLine("List all gateways:");
            Output.WriteLine();
            
            Gui.PrintColumns(
                new ConsoleTextColumn {Text = "Name", Width = maxNameWidth},
                new ConsoleTextColumn {Text = "Id", Width = maxIdWidth},
                new ConsoleTextColumn {Text = "State"});
            
            Gui.PrintColumns(
                new ConsoleTextColumn {Text = "----", Width = maxNameWidth},
                new ConsoleTextColumn {Text = "--", Width = maxIdWidth},
                new ConsoleTextColumn {Text = "-----"});
            
            _gatewayRepository.ForEach(gateway => 
                Gui.PrintColumns(
                    new ConsoleTextColumn {Text = gateway.Name, Width = maxNameWidth},
                    new ConsoleTextColumn {Text = gateway.Id.ToString(), Width = maxIdWidth},
                    new ConsoleTextColumn {Text = gateway.State.ToString()}));
            
            return Task.CompletedTask;
        }

        public override string CommandName => "list-gateways";
    }
}