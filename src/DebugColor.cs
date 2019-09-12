
using System;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    /// <summary>
    /// RGBA channels as floats used to represent the color for debug drawing.
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
        /// Red component in the RGBA color space.
        /// </summary>
        public float Red => red;

        /// <summary>
        /// Green component in the RGBA color space.
        /// </summary>
        public float Green => green;

        /// <summary>
        /// Blue component in the RGBA color space.
        /// </summary>
        public float Blue => blue;

        /// <summary>
        /// Alpha component in the RGBA color space.
        /// </summary>
        public float Alpha => alpha;
        
        /// <summary>
        /// Create a <see cref="DebugColor"/> with the given color channel values.
        /// </summary>
        public DebugColor(float red, float green, float blue) 
            : this(red, green, blue, 1.0f)
        {
        }

        /// <summary>
        /// Create a <see cref="DebugColor"/> with the given color channel values.
        /// </summary>
        public DebugColor(float red, float green, float blue, float alpha)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }

        /// <summary>
        /// Check if a <see cref="DebugColor"/> is equal to another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            var other = obj as DebugColor?;

            if (!other.HasValue)
            {
                return false;
            }

            return Equals(other.Value);
        }

        /// <summary>
        /// Check if a <see cref="DebugColor"/> is reference-equal to the other.
        /// </summary>
        public bool Equals(DebugColor color)
        {
            return this == color;
        }

        /// <summary>
        /// Get the hash code.
        /// </summary>
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
        /// Return a string formatted as "(R, G, B, A)".
        /// </summary>
        public override string ToString()
        {
            return $"({red},{green},{blue},{alpha})";
        }

        /// <summary>
        /// Return true if two <see cref="DebugColor"/> are reference-equal.
        /// </summary>
        public static bool operator == (DebugColor a, DebugColor b)
        {
            return a.red == b.red &&
                a.green == b.green &&
                a.blue == b.blue &&
                a.alpha == b.alpha;
        }


        /// <summary>
        /// Return true if two <see cref="DebugColor"/> are not reference-equal.
        /// </summary>
        public static bool operator !=(DebugColor a, DebugColor b)
        {
            return !(a == b);
        }
    }
}
