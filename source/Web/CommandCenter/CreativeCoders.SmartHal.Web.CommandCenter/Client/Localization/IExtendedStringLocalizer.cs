using Microsoft.Extensions.Localization;

namespace CreativeCoders.SmartHal.Web.CommandCenter.Client.Localization
{
    public interface IExtendedStringLocalizer<out T> : IStringLocalizer<T>
    {
        
    }
}