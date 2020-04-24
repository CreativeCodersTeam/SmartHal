using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;

namespace CreativeCoders.SmartHal.Kernel.Base.Drivers
{
    public interface IThingChannelHandler
    {
        Task SetupAsync(IMessageHub messageHub);
        
        Task InitAsync();
        
        string Name { get; }
    }
}