using System.Reactive.Subjects;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Messaging
{
    [UsedImplicitly]
    public class MessageHub : IMessageHub
    {
        private static readonly ILogger Log = LogManager.GetLogger<MessageHub>();
        
        private readonly ISubject<object> _subject;
            
        public MessageHub()
        {
            _subject = Subject.Synchronize(new Subject<object>());
        }
        
        public void SendMessage<TMessage>(TMessage message)
            where TMessage : SmartHalMessageBase
        {
            Log.Debug($"Send message '{message}' MessageId = {message.MessageId}");
            
            _subject.OnNext(message);
        }

        public IHandlerRegistration<TMessage> Handle<TMessage>()
            where TMessage : SmartHalMessageBase
        {
            return new HandlerRegistration<TMessage>(_subject);
        }
    }
}