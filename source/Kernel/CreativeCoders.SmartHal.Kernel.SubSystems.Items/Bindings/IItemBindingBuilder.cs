using System.Collections.Generic;
using CreativeCoders.SmartHal.Kernel.Base.Items;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.Bindings
{
    public interface IItemBindingBuilder
    {
        IItemBinding Build(IReadOnlyCollection<string> channelIds);
    }
}