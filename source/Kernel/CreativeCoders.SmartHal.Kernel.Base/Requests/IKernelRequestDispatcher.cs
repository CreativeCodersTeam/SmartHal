using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Requests
{
    [PublicAPI]
    public interface IKernelRequestDispatcher
    {
        void Process(KernelRequest request);

        void Process(Func<Task> execute);
    }
}