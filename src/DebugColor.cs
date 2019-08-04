
using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    /// <summary>
    /// 4 floating point values representing the color to debug drawing functions
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DebugColor : IEquatable<DebugColor>
    {
        #pragma warning disable IDE0032
        private float red;
        private float green;
        private float blue;
        private float alpha;
        #pragma warning restore IDE0032

        /// <summary>
        /// Red component
        /// </summary>
        public float Red => red;

        /// <summary>
        /// Green component
        /// </summary>
        public float Green => green;

        /// <summary>
        /// Blue component
        /// </summary>
        public float Blue => blue;

        /// <summary>
        /// Alfa component
        /// </summary>
        public float Alpha => alpha;
        
        /// <summary>
        /// Create a DebugColor with given red, green blue components
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        public DebugColor(float red, float green, float blue) 
            : this(red, green, blue, 1.0f)
        {

        }

        /// <summary>
        /// Create a DebugColor with given red, green, blue and alpha components
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        public DebugColor(float red, float green, float blue, float alpha)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }

        /// <summary>
        /// Check if an DebugColor is equal to an object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as DebugColor?;
            if (!other.HasValue)
                return false;

            return Equals(other.Value);
        }

        /// <summary>
        /// Check if a DebugColor is equal to another
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool Equals(DebugColor color)
        {
            return this == color;
        }

        /// <summary>
        /// GetHashCode from a DebugColor
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = -1813971818;
            hashCode = hashCode * -1521134295 + red.GetHashCode();
            hashCode = hashCode * -1521134295 + green.GetHashCode();
            hashCode = hashCode * -1521134295 + blue.GetHashCode();
            hashCode = hashCode * -1521134295 + alpha.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"({red},{green},{blue},{alpha})";
        }

        /// <summary>
        /// operator ==
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator == (DebugColor a, DebugColor b)
        {
            return a.red == b.red &&
                   a.green == b.green &&
                   a.blue == b.blue &&
                   a.alpha == b.alpha;
        }


        /// <summary>
        /// operator !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(DebugColor a, DebugColor b)
        {
            return !(a == b);
        }
    }
}
