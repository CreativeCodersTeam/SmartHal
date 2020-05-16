using System;

namespace CreativeCoders.Kernel.Services.ConsoleInterface
{
    public class ConsoleOutput : IConsoleOutput
    {
        public void Write<T>(T data)
        {
            Console.Write(data);
        }

        public void WriteLine<T>(T data)
        {
            Console.WriteLine(data);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }
    }
}