using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Things.Ident
{
    public abstract class IdBase
    {
        private readonly IList<string> _segments;
        
        [UsedImplicitly]
        protected IdBase()
        {
            _segments = new List<string>();
        }
        
        protected IdBase(IEnumerable<string> segments)
        {
            _segments = new List<string>(segments);
        }
        
        public override bool Equals(object obj)
        {
            return obj switch
            {
                IdBase id => Equals(id),
                string idText => Equals(idText),
                _ => false
            };
        }
        
        public bool Equals(string id)
        {
            return ToString().Equals(id, StringComparison.InvariantCultureIgnoreCase);
        }
        
        public bool Equals(IdBase id)
        {
            if (ReferenceEquals(id, null))
            {
                return false;
            }

            return ReferenceEquals(this, id) || Equals(id.ToString());
        }
        
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        
        public override string ToString()
        {
            return string.Join(":", _segments);
        }
        
        protected string GetSegment(SegmentIndex segmentIndex)
        {
            var index = (int) segmentIndex;
            return _segments.Count <= index ? string.Empty : _segments[index];
        }

        protected void SetSegment(SegmentIndex segmentIndex, string value)
        {
            var index = (int) segmentIndex;
            while (_segments.Count <= index) { _segments.Add(string.Empty); }
            _segments[index] = value;
        }
        
        public string Driver
        {
            get => GetSegment(SegmentIndex.Driver);
            set => SetSegment(SegmentIndex.Driver, value);
        }
    }
}