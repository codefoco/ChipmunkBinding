using System;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006
#pragma warning disable IDE0032

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vect : IEquatable<Vect>
    {
        private static readonly Vect zero = new Vect(0, 0);

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

        public Vect(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            Vect? vect = obj as Vect?;
            if (!vect.HasValue)
                return false;
            return this == vect.Value;
        }

        public override int GetHashCode()
        {
            return (x.GetHashCode() << 16) ^ y.GetHashCode();
        }

        public bool Equals(Vect other)
        {
            return this == other;
        }

        public override string ToString()
        {
             return $"({x},{y})";
        }

        public static bool operator == (Vect a, Vect b)
        {
            return Math.Abs(a.x - b.x) < float.Epsilon && 
                   Math.Abs(a.y - b.y) < float.Epsilon;
        }

        public static bool operator !=(Vect a, Vect b)
        {
            return !(a == b);
        }

        public static Vect Zero => zero;
    }
}

#pragma warning restore IDE1006
#pragma warning restore IDE0032
