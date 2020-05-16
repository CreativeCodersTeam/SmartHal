using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Items;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.Bindings
{
    public class NullBinding : IItemBinding
    {
        public Task WriteValueAsync(object value)
        {
            return Task.CompletedTask;
        }
    }
}