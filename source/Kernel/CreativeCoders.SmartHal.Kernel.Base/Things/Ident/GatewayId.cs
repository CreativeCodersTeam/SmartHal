using System;
using System.Collections.Generic;

namespace CreativeCoders.SmartHal.Kernel.Base.Things.Ident
{
    public class GatewayId : IdBase
    {
        private const int SegmentCount = 2;
        
        public GatewayId(string driver, string gateway)
        {
            SetSegment(SegmentIndex.Driver, driver);
            SetSegment(SegmentIndex.Gateway, gateway);
        }

        private GatewayId(IEnumerable<string> segments) : base(segments)
        {
        }

        public static GatewayId Parse(string id)
        {
            if (!TryParse(id, out var gatewayId))
            {
                throw new FormatException($"Gateway id '{id}' has invalid format");
            }

            return gatewayId;
        }

        public static bool TryParse(string id, out GatewayId gatewayId)
        {
            var segments = id.Split(":");

            if (segments.Length != SegmentCount)
            {
                gatewayId = null;
                return false;
            }
            
            gatewayId = new GatewayId(segments);
            return true;
        }

        public string Gateway
        {
            get => GetSegment(SegmentIndex.Gateway);
            set => SetSegment(SegmentIndex.Gateway, value);
        }
    }
}