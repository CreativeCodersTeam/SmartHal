using CreativeCoders.SmartHal.Kernel.Base.Things;

namespace CreativeCoders.SmartHal.Web.Api.Core.Models
{
    public class GatewayModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ThingState State { get; set; }

        public string StateText { get; set; }
    }
}