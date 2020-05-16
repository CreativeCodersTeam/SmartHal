using System;
using System.Collections.Generic;

namespace CreativeCoders.SmartHal.Kernel.Base.Things.Ident
{
    public class ThingChannelId : IdBase
    {
        private const int SegmentCount = 4;
        
        public ThingChannelId(ThingId thingId, string channel)
        {
            SetSegment(SegmentIndex.Driver, thingId.Driver);
            SetSegment(SegmentIndex.Gateway, thingId.Gateway);
            SetSegment(SegmentIndex.Thing, thingId.Thing);
            SetSegment(SegmentIndex.Channel, channel);
        }
        
        private ThingChannelId(IEnumerable<string> segments) : base(segments)
        {
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
            
            thingChannelId = new ThingChannelId(segments);
            return true;
        }
        
        public string Gateway
        {
            get => GetSegment(SegmentIndex.Gateway);
            set => SetSegment(SegmentIndex.Gateway, value);
        }
        
        public string Thing
        {
            get => GetSegment(SegmentIndex.Thing);
            set => SetSegment(SegmentIndex.Thing, value);
        }

        public string Channel
        {
            get => GetSegment(SegmentIndex.Channel);
            set => SetSegment(SegmentIndex.Channel, value);
        }
    }
}