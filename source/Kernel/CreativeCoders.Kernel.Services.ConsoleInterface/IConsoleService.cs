namespace CreativeCoders.Kernel.Services.ConsoleInterface
{
    public interface IConsoleService
    {
        IConsoleInput CreateConsole(IConsoleOutput consoleOutput);
    }
}