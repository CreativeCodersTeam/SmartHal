using System;
using System.Collections.Generic;

namespace CreativeCoders.SmartHal.Kernel.Base.Things.Ident
{
    public class ThingId : IdBase
    {
        private const int SegmentCount = 3;
        
        public ThingId(GatewayId gatewayId, string thing)
        {
            SetSegment(SegmentIndex.Driver, gatewayId.Driver);
            SetSegment(SegmentIndex.Gateway, gatewayId.Gateway);
            SetSegment(SegmentIndex.Thing, thing);
        }

        private ThingId(IEnumerable<string> segments) : base(segments)
        {
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
            
            thingId = new ThingId(segments);
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
    }
}