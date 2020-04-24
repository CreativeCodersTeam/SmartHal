using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Things
{
    [PublicAPI]
    public interface IThingChannel
    {
        ThingChannelId Id { get; }

        string Name { get; }

        ThingState State { get; }
        
        object Value { get; }
    }
}