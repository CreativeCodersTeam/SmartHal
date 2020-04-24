using System;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Messaging
{
    [PublicAPI]
    public interface IHandlerRegistration<out TMessage>
        where TMessage : SmartHalMessageBase
    {
        IHandlerRegistration<TMessage> Where(Func<TMessage, bool> predicate);

        IHandlerRegistration<TMessage> SetMaxConcurrency(int maxConcurrency);

        IHandlerRegistration<TMessage> Synchronize();

        IHandlerRegistration<TMessage> Once();

        IDisposable Register(Func<TMessage, Task> execute);
    }
}