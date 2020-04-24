using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.Base.Booting
{
    public interface IThingsBootStep
    {
        Task InitThingsAsync();
    }
}