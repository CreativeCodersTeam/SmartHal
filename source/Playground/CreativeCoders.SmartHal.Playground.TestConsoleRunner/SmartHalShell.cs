using System;
using System.Threading.Tasks;
using CreativeCoders.Kernel.Services.ConsoleInterface;
using CreativeCoders.SmartHal.Kernel.Base;

namespace CreativeCoders.SmartHal.Playground.TestConsoleRunner
{
    public class SmartHalShell
    {
        private readonly ISmartHalKernel _kernel;

        public SmartHalShell(ISmartHalKernel kernel)
        {
            _kernel = kernel;
        }

        public async Task RunAsync()
        {
            var consoleService = _kernel.GetService<IConsoleService>();
            
            var consoleInput = consoleService.CreateConsole(new ConsoleOutput());
            
            await CommandLoopAsync(consoleInput).ConfigureAwait(false);
        }
        
        private static async Task CommandLoopAsync(IConsoleInput consoleInput)
        {
            var command = ReadCommand();
            while (command != "exit")
            {
                await consoleInput.ExecuteCommandAsync(command).ConfigureAwait(false);
                command = ReadCommand();
            }
        }
        
        private static string ReadCommand()
        {
            Console.WriteLine();
            Console.Write("SmartHalShell> ");

            var command = Console.ReadLine();

            return command;
        }
    }
}