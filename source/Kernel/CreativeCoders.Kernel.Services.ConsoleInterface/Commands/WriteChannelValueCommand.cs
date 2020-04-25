using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using JetBrains.Annotations;

namespace CreativeCoders.Kernel.Services.ConsoleInterface.Commands
{
    [UsedImplicitly]
    public class WriteChannelValueCommand : ConsoleCommandBase
    {
        private readonly IMessageHub _messageHub;

        public WriteChannelValueCommand(IMessageHub messageHub)
        {
            _messageHub = messageHub;
        }
        
        public override Task ExecuteAsync(string[] arguments)
        {
            var channelId = arguments.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(channelId))
            {
                Output.WriteLine("No channel id given");
                
                return Task.CompletedTask;
            }

            var value = arguments.Skip(1).FirstOrDefault();

            if (value == null)
            {
                Output.WriteLine("No value is given");
                
                return Task.CompletedTask;
            }
            
            _messageHub.SendMessage(new WriteChannelValueMessage(channelId, value));
            
            return Task.CompletedTask;
        }

        public override string CommandName => "write-channel-value";
    }
}