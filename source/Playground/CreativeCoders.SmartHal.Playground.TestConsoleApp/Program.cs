using System;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Playground.TestConsoleApp
{
    [UsedImplicitly]
    public class Program
    {
        public static void Main()
        {
            ConfigTest.Run();

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }
    }
}