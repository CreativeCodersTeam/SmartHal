using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using JetBrains.Annotations;

namespace CreativeCoders.Kernel.Services.ConsoleInterface.Commands
{
    [UsedImplicitly]
    public class SendCommandToItemCommand : ConsoleCommandBase
    {
        private readonly IMessageHub _messageHub;

        public SendCommandToItemCommand(IMessageHub messageHub)
        {
            _messageHub = messageHub;
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

            _messageHub.SendMessage(new SendCommandToItemMessage(itemName, commandValue));
            
            return Task.CompletedTask;
        }

        public override string CommandName => "send-command";
    }
}