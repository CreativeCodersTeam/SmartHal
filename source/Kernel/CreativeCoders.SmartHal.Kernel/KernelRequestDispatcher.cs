﻿using System;
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
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                Log.Debug($"Kernel request (id = {request.Id}) '{request.DisplayName}' execution starting");

                try
                {
                    await request.ExecuteAsync().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Log.Error("Kernel request execution failed.", e);
                }
                
                Log.Debug($"Kernel request (id = {request.Id}) '{request.DisplayName}' execution finished");
            });
        }

        public void Process(Func<Task> execute)
        {
            Process(new KernelRequest(execute));
        }
    }
}