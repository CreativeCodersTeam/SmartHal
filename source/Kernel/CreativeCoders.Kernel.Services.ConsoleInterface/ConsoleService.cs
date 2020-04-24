using CreativeCoders.Core;
using JetBrains.Annotations;

namespace CreativeCoders.Kernel.Services.ConsoleInterface
{
    [UsedImplicitly]
    public class ConsoleService : IConsoleService
    {
        private readonly IClassFactory _classFactory;

        public ConsoleService(IClassFactory classFactory)
        {
            _classFactory = classFactory;
        }
        
        public IConsoleInput CreateConsole(IConsoleOutput consoleOutput)
        {
            var console = _classFactory.Create<SmartHalConsole>();
            
            console.SetOutput(consoleOutput);

            return console;
        }
    }
}