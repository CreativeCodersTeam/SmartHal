using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Core;
using JetBrains.Annotations;

namespace CreativeCoders.Kernel.Services.ConsoleInterface
{
    [UsedImplicitly]
    public class SmartHalConsole : IConsoleInput
    {
        private readonly IEnumerable<IConsoleCommand> _commands;
        
        private IConsoleOutput _consoleOutput;

        public SmartHalConsole(IEnumerable<IConsoleCommand> commands)
        {
            _commands = commands;
        }
        
        public void SetOutput(IConsoleOutput consoleOutput)
        {
            _consoleOutput = consoleOutput;
            
            _commands.ForEach(x => x.Init(_consoleOutput));
        }
        
        public async Task ExecuteCommandAsync(string command)
        {
            var commandLineCall = new CommandLineCall(command);

            var consoleCommand = _commands.FirstOrDefault(x => x.CommandName == commandLineCall.CommandName);

            if (consoleCommand != null)
            {
                await consoleCommand.ExecuteAsync(commandLineCall.Arguments).ConfigureAwait(false);

                return;
            }
            
            _consoleOutput.WriteLine($"Command '{commandLineCall.CommandName}' not found");
            _consoleOutput.WriteLine();
        }
    }
}