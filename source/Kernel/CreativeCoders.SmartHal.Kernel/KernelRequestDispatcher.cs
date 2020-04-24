using System;
using System.Threading;
using System.Threading.Tasks;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Kernel.Base.Requests;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel
{
    [UsedImplicitly]
    public class KernelRequestDispatcher : IKernelRequestDispatcher
    {
        private static readonly ILogger Log = LogManager.GetLogger<KernelRequestDispatcher>();
        
        public void Process(KernelRequest request)
        {
            ThreadPool.QueueUserWorkItem(async state =>
            {
                Log.Debug($"Kernel request (id = {request.Id}) '{request.DisplayName}' execution starting");
                
                await request.ExecuteAsync().ConfigureAwait(false);
                
                Log.Debug($"Kernel request (id = {request.Id}) '{request.DisplayName}' execution finished");
            });
        }

        public void Process(Func<Task> execute)
        {
            Process(new KernelRequest(execute));
        }
    }
}