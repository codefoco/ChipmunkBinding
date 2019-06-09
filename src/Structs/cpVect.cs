using System;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006
#pragma warning disable IDE0032

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    public struct cpVect : IEquatable<cpVect>
    {
        private static readonly cpVect zero = new cpVect(0, 0);

        private double x;
        private double y;

        public double X 
        { 
          get => x;
          set => x = value;
        }

        public double Y 
        { 
          get => y;
          set => y = value;
        }

        public cpVect(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            cpVect? vect = obj as cpVect?;
            if (!vect.HasValue)
                return false;
            return this == vect.Value;
        }

        public override int GetHashCode()
        {
            return (x.GetHashCode() << 16) ^ y.GetHashCode();
        }

        public bool Equals(cpVect other)
        {
            return this == other;
        }

        public override string ToString()
        {
             return $"({x},{y})";
        }

        public static bool operator == (cpVect a, cpVect b)
        {
            return Math.Abs(a.x - b.x) < float.Epsilon && 
                   Math.Abs(a.y - b.y) < float.Epsilon;
        }

        public static bool operator !=(cpVect a, cpVect b)
        {
            return !(a == b);
        }

        public static cpVect Zero => zero;
    }
}

#pragma warning restore IDE1006
#pragma warning restore IDE0032
