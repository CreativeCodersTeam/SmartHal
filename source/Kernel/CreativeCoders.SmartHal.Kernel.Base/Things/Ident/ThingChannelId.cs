using System;

namespace CreativeCoders.SmartHal.Kernel.Base.Things.Ident
{
    public class ThingChannelId : IdBase
    {
        private const int SegmentCount = 4;
        
        public ThingChannelId(ThingId thingId, string channel)
        {
            ThingId = thingId;
            Channel = channel;
        }
        
        public static ThingChannelId Parse(string id)
        {
            if (!TryParse(id, out var thingChannelId))
            {
                throw new FormatException($"Thing channel id '{id}' has invalid format");
            }

            return thingChannelId;
        }

        public static bool TryParse(string id, out ThingChannelId thingChannelId)
        {
            var segments = id.Split(":");

            if (segments.Length != SegmentCount)
            {
                thingChannelId = null;
                return false;
            }

            thingChannelId = new ThingChannelId(
                new ThingId(
                    new GatewayId(segments[SegmentIndex.Driver], segments[SegmentIndex.Gateway]),
                    segments[SegmentIndex.Thing]),
                segments[SegmentIndex.Channel]);

            return true;
        }

        public override string ToString() => $"{ThingId}:{Channel}";

        public string Driver => ThingId.Driver;

        public string Gateway => ThingId.Gateway;

        public string Thing => ThingId.Thing;

        public string Channel { get; }

        public ThingId ThingId { get; }
    }
}