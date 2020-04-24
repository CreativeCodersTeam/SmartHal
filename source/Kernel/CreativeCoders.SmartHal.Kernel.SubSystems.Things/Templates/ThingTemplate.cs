using System.Linq;
using CreativeCoders.SmartHal.Config.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.Things;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things.Templates
{
    public class ThingTemplate : IThingTemplate
    {
        private readonly IThingTemplateDefinition _thingTemplateDefinition;

        public ThingTemplate(IThingTemplateDefinition thingTemplateDefinition)
        {
            _thingTemplateDefinition = thingTemplateDefinition;
        }
        
        public bool IsChannelDefined(string channelName)
        {
            return _thingTemplateDefinition.Channels.Contains(channelName);
        }

        public string Name => _thingTemplateDefinition.Name;
    }
}