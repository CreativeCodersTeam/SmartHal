using System.Threading.Tasks;

namespace CreativeCoders.Kernel.Services.ConsoleInterface
{
    public interface IConsoleInput
    {
        Task ExecuteCommandAsync(string command);
    }
}