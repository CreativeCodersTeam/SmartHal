using System;

namespace CreativeCoders.SmartHal.Kernel.Base.Things.Ident
{
    public abstract class IdBase
    {
        public static bool operator ==(IdBase id1, IdBase id2)
        {
            if (ReferenceEquals(id1, id2))
            {
                return true;
            }

            if (id1 is null || id2 is null)
            {
                return false;
            }

            return id1.Equals(id2);
        }

        public static bool operator !=(IdBase id1, IdBase id2)
        {
            if (ReferenceEquals(id1, id2))
            {
                return false;
            }

            if (id1 is null || id2 is null)
            {
                return true;
            }

            return !id1.Equals(id2);
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
            if (id is null)
            {
                return false;
            }

            return ReferenceEquals(this, id) || Equals(id.ToString());
        }
        
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public abstract override string ToString();
    }
}