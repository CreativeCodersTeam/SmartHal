using JetBrains.Annotations;

namespace CreativeCoders.Kernel.Services.ConsoleInterface
{
    [PublicAPI]
    public interface IConsoleOutput
    {
        void Write<T>(T data);

        void WriteLine<T>(T data);

        void WriteLine();
    }
}