// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2026 Codefoco LTDA - The above copyright notice and this permission notice shall be
//     included in all copies or substantial portions of the Software.
//
//     Redistribution and use in source and binary forms, with or without
//     modification, are permitted only if explicitly approved by the authors.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
//     EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//     OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//     NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//     HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//     FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//     OTHER DEALINGS IN THE SOFTWARE.

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
        private readonly float red;
        private readonly float green;
        private readonly float blue;
        private readonly float alpha;
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

            if (other == null)
                return false;

            return Equals(other.Value);
        }

        /// <summary>
        /// Check if a <see cref="DebugColor"/> is reference-equal to the other.
        /// </summary>
        public bool Equals(DebugColor other)
        {
            return this == other;
        }

#pragma warning disable IDE0070 // Use 'System.HashCode'
        /// <summary>
        /// Get the hash code.
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = -1813971818;
            hashCode = (hashCode * -1521134295) + red.GetHashCode();
            hashCode = (hashCode * -1521134295) + green.GetHashCode();
            hashCode = (hashCode * -1521134295) + blue.GetHashCode();
            hashCode = (hashCode * -1521134295) + alpha.GetHashCode();
            return hashCode;
        }
#pragma warning restore IDE0070 // Use 'System.HashCode'

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
        public static bool operator ==(DebugColor a, DebugColor b)
        {
#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator
            return a.red == b.red &&
                a.green == b.green &&
                a.blue == b.blue &&
                a.alpha == b.alpha;
#pragma warning restore RECS0018 // Comparison of floating point numbers with equality operator
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