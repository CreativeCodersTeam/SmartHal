using CreativeCoders.SmartHal.Kernel.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Messages
{
    [PublicAPI]
    public class GatewayAddedMessage : SmartHalMessageBase
    {
        public GatewayAddedMessage(IGateway gateway)
        {
            Gateway = gateway;
        }
        
        public IGateway Gateway { get; }
    }
}