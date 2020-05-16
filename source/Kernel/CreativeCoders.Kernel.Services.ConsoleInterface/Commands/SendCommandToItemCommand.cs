using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.Kernel.Services.ConsoleInterface.Commands
{
    [UsedImplicitly]
    public class SendCommandToItemCommand : ConsoleCommandBase
    {
        private readonly IItemSubSystem _itemSubSystem;

        public SendCommandToItemCommand(IItemSubSystem itemSubSystem)
        {
            _itemSubSystem = itemSubSystem;
        }
        
        public override Task ExecuteAsync(string[] arguments)
        {
            if (arguments.Length != 2)
            {
                Output.WriteLine("Argument count mismatch");
                
                return Task.CompletedTask;
            }

            var itemName = arguments[0];
            var commandValue = arguments[1];

            _itemSubSystem.SendCommand(itemName, commandValue);
            
            return Task.CompletedTask;
        }

        public override string CommandName => "send-command";
    }
}