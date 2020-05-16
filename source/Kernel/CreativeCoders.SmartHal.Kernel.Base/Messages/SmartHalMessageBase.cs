using System.Threading;

namespace CreativeCoders.SmartHal.Kernel.Base.Messages
{
    public abstract class SmartHalMessageBase
    {
        private static long _counter;

        public long MessageId { get; } = Interlocked.Increment(ref _counter);
    }
}