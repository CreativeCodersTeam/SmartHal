using System;

namespace CreativeCoders.SmartHal.Kernel.Base.Things.Ident
{
    public class GatewayId : IdBase
    {
        private const int SegmentCount = 2;
        
        public GatewayId(string driver, string gateway)
        {
            Driver = driver;
            Gateway = gateway;
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
            
            gatewayId = new GatewayId(segments[SegmentIndex.Driver], segments[SegmentIndex.Gateway]);

            return true;
        }

        public override string ToString() => $"{Driver}:{Gateway}";

        public string Driver { get; }

        public string Gateway { get; }
    }
}