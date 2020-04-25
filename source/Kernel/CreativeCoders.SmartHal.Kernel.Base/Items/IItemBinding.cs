using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.Base.Items
{
    public interface IItemBinding
    {
        Task WriteValueAsync(object value);
    }
}