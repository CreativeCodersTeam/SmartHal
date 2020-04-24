using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using CreativeCoders.Core.SysEnvironment;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;

namespace CreativeCoders.SmartHal.Kernel.Messaging
{
    internal class HandlerRegistration<TMessage> : IHandlerRegistration<TMessage>
        where TMessage : SmartHalMessageBase
    {
        private readonly ISubject<object> _subject;
        
        private Func<TMessage, bool> _wherePredicate;
        
        private int _maxConcurrency;

        private bool _once;

        public HandlerRegistration(ISubject<object> subject)
        {
            _subject = subject;

            _maxConcurrency = Env.ProcessorCount;
        }

        public IHandlerRegistration<TMessage> Where(Func<TMessage, bool> predicate)
        {
            _wherePredicate = predicate;

            return this;
        }

        public IHandlerRegistration<TMessage> SetMaxConcurrency(int maxConcurrency)
        {
            _maxConcurrency = maxConcurrency;

            return this;
        }

        public IHandlerRegistration<TMessage> Synchronize()
        {
            return SetMaxConcurrency(1);
        }

        public IHandlerRegistration<TMessage> Once()
        {
            _once = true;

            return this;
        }

        public IDisposable Register(Func<TMessage, Task> execute)
        {
            var observable = _subject
                .OfType<TMessage>()
                .Publish()
                .RefCount()
                .ObserveOn(TaskPoolScheduler.Default);

            if (_wherePredicate != null)
            {
                observable = observable.Where(_wherePredicate);
            }

            if (_once)
            {
                observable = observable.FirstAsync();
            }

            return observable
                .Select(message => Observable.FromAsync(async () => await execute(message).ConfigureAwait(false)))
                .Merge(_maxConcurrency)
                .Subscribe();
        }
    }
}