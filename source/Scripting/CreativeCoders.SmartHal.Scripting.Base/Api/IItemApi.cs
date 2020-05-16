using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Scripting.Base.Api
{
    [PublicAPI]
    public interface IItemApi
    {
        object Value { get; }

        void SendCommand(object commandValue);
    }
}