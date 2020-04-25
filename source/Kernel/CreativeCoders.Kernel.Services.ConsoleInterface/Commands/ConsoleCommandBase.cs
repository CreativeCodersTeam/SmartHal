using System.Threading.Tasks;

namespace CreativeCoders.Kernel.Services.ConsoleInterface.Commands
{
    public abstract class ConsoleCommandBase : IConsoleCommand
    {
        public void Init(IConsoleOutput consoleOutput)
        {
            Output = consoleOutput;
            Gui = new ConsoleGui(Output);
        }

        public abstract Task ExecuteAsync(string[] arguments);
        
        public abstract string CommandName { get; }

        protected IConsoleOutput Output { get; private set; }
        
        protected ConsoleGui Gui { get; private set; }
    }
}