
using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]

    public struct cpSpaceDebugColor : IEquatable<cpSpaceDebugColor>
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
        

        public cpSpaceDebugColor(float red, float green, float blue) 
            : this(red, green, blue, 1.0f)
        {

        }

        public cpSpaceDebugColor(float red, float green, float blue, float alpha)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }

        public override bool Equals(object obj)
        {
            var other = obj as cpSpaceDebugColor?;
            if (!other.HasValue)
                return false;

            return Equals(other.Value);
        }

        public bool Equals(cpSpaceDebugColor color)
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

        public static bool operator == (cpSpaceDebugColor a, cpSpaceDebugColor b)
        {
            return a.red == b.red &&
                   a.green == b.green &&
                   a.blue == b.blue &&
                   a.alpha == b.alpha;
        }

        public static bool operator !=(cpSpaceDebugColor a, cpSpaceDebugColor b)
        {
            return !(a == b);
        }
    }
}
