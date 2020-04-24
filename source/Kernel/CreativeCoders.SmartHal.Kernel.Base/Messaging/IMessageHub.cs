using CreativeCoders.SmartHal.Kernel.Base.Messages;

namespace CreativeCoders.SmartHal.Kernel.Base.Messaging
{
    public interface IMessageHub
    {
        void SendMessage<TMessage>(TMessage message)
            where TMessage : SmartHalMessageBase;

        IHandlerRegistration<TMessage> Handle<TMessage>()
            where TMessage : SmartHalMessageBase;
    }
}