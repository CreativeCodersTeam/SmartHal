using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Scripting.Base.Api
{
    [PublicAPI]
    public interface IItemsScriptApi
    {
        IItemApi GetItem(string name);
    }
}