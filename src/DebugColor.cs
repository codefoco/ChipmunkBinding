
using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]

    public struct DebugColor : IEquatable<DebugColor>
    {
        #pragma warning disable IDE0032
        private float red;
        private float green;
        private float blue;
        private float alpha;
        #pragma warning restore IDE0032

        public float Red => red;
        public float Green => green;
        public float Blue => blue;
        public float Alfa => alpha;
        

        public DebugColor(float red, float green, float blue) 
            : this(red, green, blue, 1.0f)
        {

        }

        public DebugColor(float red, float green, float blue, float alpha)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }

        public override bool Equals(object obj)
        {
            var other = obj as DebugColor?;
            if (!other.HasValue)
                return false;

            return Equals(other.Value);
        }

        public bool Equals(DebugColor color)
        {
            return this == color;
        }

        public override int GetHashCode()
        {
            var hashCode = -1813971818;
            hashCode = hashCode * -1521134295 + red.GetHashCode();
            hashCode = hashCode * -1521134295 + green.GetHashCode();
            hashCode = hashCode * -1521134295 + blue.GetHashCode();
            hashCode = hashCode * -1521134295 + alpha.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"({red},{green},{blue},{alpha})";
        }

        public static bool operator == (DebugColor a, DebugColor b)
        {
            return a.red == b.red &&
                   a.green == b.green &&
                   a.blue == b.blue &&
                   a.alpha == b.alpha;
        }

        public static bool operator !=(DebugColor a, DebugColor b)
        {
            return !(a == b);
        }
    }
}
