using CreativeCoders.SmartHal.Kernel.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Messages
{
    [PublicAPI]
    public class ThingAddedMessage : SmartHalMessageBase
    {
        public ThingAddedMessage(IThing thing)
        {
            Thing = thing;
        }
        
        public IThing Thing { get; }
    }
}