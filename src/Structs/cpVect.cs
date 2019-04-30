using System.Collections.Generic;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    public struct cpVect : IEqualityComparer<cpVect>
    {
        public double x;
        public double y;

        public bool Equals(cpVect x, cpVect y)
        {
            return x == y;
        }

        public int GetHashCode(cpVect obj)
        {
            return (obj.x.GetHashCode() << 16) ^ obj.y.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            cpVect? vect = obj as cpVect?;
            if (!vect.HasValue)
                return false;
            return Equals(this, vect.Value);
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        public static bool operator == (cpVect a, cpVect b)
        {
            return System.Math.Abs(a.x - b.x) < float.Epsilon && System.Math.Abs(a.x - b.x) < float.Epsilon;
        }

        public static bool operator !=(cpVect a, cpVect b)
        {
            return !(a == b);
        }
    }

}

#pragma warning restore IDE1006
