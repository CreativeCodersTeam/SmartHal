using CreativeCoders.SmartHal.Kernel.Base.Things;

namespace CreativeCoders.SmartHal.Web.Api.Core.Models
{
    public class ThingChannelModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ThingState State { get; set; }

        public object Value { get; set; }
    }
}