using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Things;

namespace CreativeCoders.SmartHal.Kernel.Base.Drivers
{
    public interface IThingHandler
    {
        Task SetupAsync(IMessageHub messageHub);

        Task<ThingState> InitAsync();
    }
}