using System.Collections.Generic;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Things
{
    [PublicAPI]
    public interface IThing
    {
        string Name { get; }
        
        ThingId Id { get; }
        
        ThingState State { get; }
        
        IReadOnlyCollection<IThingChannel> Channels { get; }
    }
}