using System.Threading.Tasks;

namespace CreativeCoders.Kernel.Services.ConsoleInterface
{
    public interface IConsoleCommand
    {
        void Init(IConsoleOutput consoleOutput);
        
        Task ExecuteAsync(string[] arguments);
        
        string CommandName { get; }
    }
}