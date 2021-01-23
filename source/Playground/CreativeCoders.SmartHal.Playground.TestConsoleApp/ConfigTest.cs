using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CreativeCoders.Config;
using CreativeCoders.Config.Base;
using CreativeCoders.Di.MsServiceProvider;
using CreativeCoders.SmartHal.Config.Base.Items;
using CreativeCoders.SmartHal.Config.Base.Things;
using CreativeCoders.SmartHal.Config.FileSystem.Building;
using Microsoft.Extensions.DependencyInjection;

namespace CreativeCoders.SmartHal.Playground.TestConsoleApp
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    [SuppressMessage("ReSharper", "CommentTypo")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public static class ConfigTest
    {
        private const string ConfigPath = @"C:\temp\smarthal\hm";
        
        public static void Run()
        {
            var config = new FileConfigurationBuilder(ConfigPath, true).Build();
            
            var containerBuilder = new ServiceProviderDiContainerBuilder(new ServiceCollection());
            
            containerBuilder.Configure(config);

            var container = containerBuilder.Build();

            var gatewaySettings = container.GetInstance<ISettings<IGatewayConfiguration>>();

            var thingSettings = container.GetInstance<ISettings<IThingConfiguration>>();

            var itemSettings = container.GetInstance<ISettings<IItemConfiguration>>();

            TestThreadPool();
        }

        private static void TestThreadPool()
        {
            ThreadPool.GetMinThreads(out var workerThreads, out var completionPortThreads);
            ThreadPool.GetMaxThreads(out var maxWorkerThreads, out var maxCompletionPortThreads);
            
            ThreadPool.SetMinThreads(workerThreads * 10, completionPortThreads * 10);
            
            var items = Enumerable.Range(1, 100);

            if (items.Any(item => !ThreadPool.QueueUserWorkItem(async _ => await DoWork(item))))
            {
                throw new InvalidOperationException();
            }

            // while (ThreadPool.PendingWorkItemCount > 0)
            // {
            //     Thread.Sleep(100);
            // }
            //
            // Console.WriteLine($"Finished: {ThreadPool.CompletedWorkItemCount}");
        }
        
        private static void DoPrint(int index)
        {
            Console.WriteLine($"Begin work: {index}");
            Console.WriteLine($"End work: {index}");
        }

        private static async Task DoWork(int index)
        {
            Console.WriteLine($"Begin work: {index}");
            await Task.Delay(Math.Min(index % 10, 200)).ConfigureAwait(false);
            await Task.Delay(Math.Min(index % 10, 200)).ConfigureAwait(false);
            await Task.Delay(Math.Min(index % 10, 200)).ConfigureAwait(false);
            Console.WriteLine($"End work: {index}");
        }
    }
}