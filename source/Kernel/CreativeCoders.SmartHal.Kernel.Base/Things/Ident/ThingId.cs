using System;

namespace CreativeCoders.SmartHal.Kernel.Base.Things.Ident
{
    public class ThingId : IdBase
    {
        private const int SegmentCount = 3;
        
        public ThingId(GatewayId gatewayId, string thing)
        {
            GatewayId = gatewayId;
            Thing = thing;
        }

        public static ThingId Parse(string id)
        {
            if (!TryParse(id, out var thingId))
            {
                throw new FormatException($"Thing id '{id}' has invalid format");
            }

            return thingId;
        }

        public static bool TryParse(string id, out ThingId thingId)
        {
            var segments = id.Split(":");

            if (segments.Length != SegmentCount)
            {
                thingId = null;
                return false;
            }

            thingId = new ThingId(
                new GatewayId(segments[SegmentIndex.Driver], segments[SegmentIndex.Gateway]),
                segments[SegmentIndex.Thing]);

            return true;
        }

        public override string ToString() => $"{GatewayId}:{Thing}";

        public string Driver => GatewayId.Driver;

        public string Gateway => GatewayId.Gateway;

        public string Thing { get; }

        public GatewayId GatewayId { get; }
    }
}