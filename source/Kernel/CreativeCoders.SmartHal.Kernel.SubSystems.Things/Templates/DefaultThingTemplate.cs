using CreativeCoders.SmartHal.Kernel.Base.Things;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things.Templates
{
    public class DefaultThingTemplate : IThingTemplate
    {
        public bool IsChannelDefined(string channelName)
        {
            return true;
        }

        public string Name => string.Empty;
    }
}