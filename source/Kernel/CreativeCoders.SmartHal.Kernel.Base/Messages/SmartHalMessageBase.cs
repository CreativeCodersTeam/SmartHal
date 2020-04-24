using System.Threading;

namespace CreativeCoders.SmartHal.Kernel.Base.Messages
{
    public class SmartHalMessageBase
    {
        private static long _counter;

        public long MessageId { get; } = Interlocked.Increment(ref _counter);
    }
}