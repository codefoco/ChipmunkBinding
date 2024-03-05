// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2024 Codefoco LTDA - The above copyright notice and this permission notice shall be
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
#if !NET_4_0
using System.Runtime.CompilerServices;
#endif
using System.Runtime.InteropServices;

// ReSharper disable ConvertToAutoProperty

#pragma warning disable IDE1006
#pragma warning disable IDE0032

// Chipmunk has it own Vector class,
// We can't use System.Numerics.Vector2 since is not blitable with the native Vect from Chipmunk

namespace ChipmunkBinding
{
    /// <summary>
    /// 2D Vector struct
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vect : IEquatable<Vect>
    {
        private static readonly Vect zero = new Vect(0, 0);

        private double x;
        private double y;

        /// <summary>
        /// X value
        /// </summary>
        public double X
        {
            get => x;
            set => x = value;
        }

        /// <summary>
        /// Y value
        /// </summary>
        public double Y
        {
            get => y;
            set => y = value;
        }

        /// <summary>
        /// Create a vector.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vect(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Return true if both objects are within <see cref="Single.Epsilon"/> of each other.
        /// </summary>
        public override bool Equals(object obj)
        {
            var vect = obj as Vect?;

            if (vect == null)
            {
                return false;
            }

            return this == vect.Value;
        }

        /// <summary>
        /// Get the hash code.
        /// </summary>
        public override int GetHashCode()
        {
#pragma warning disable RECS0025 // Non-readonly field referenced in 'GetHashCode()'
            return (x.GetHashCode() << 16) ^ y.GetHashCode();
#pragma warning restore RECS0025 // Non-readonly field referenced in 'GetHashCode()'
        }

        /// <summary>
        /// Return true if both objects are within <see cref="Single.Epsilon"/> of each other.
        /// </summary>
        public bool Equals(Vect other)
        {
            return this == other;
        }

        /// <summary>
        /// Return a string formatted like "(x,y)".
        /// </summary>
        public override string ToString()
        {
            return $"({x},{y})";
        }

        /// <summary>
        /// Return true if both objects are within <see cref="Single.Epsilon"/> of each other.
        /// </summary>
        public static bool operator ==(Vect a, Vect b)
        {
            return Math.Abs(b.x - a.x) <= double.Epsilon &&
                   Math.Abs(b.y - a.y) <= double.Epsilon;
        }

        /// <summary>
        /// Return true if both objects are not within <see cref="Single.Epsilon"/> of each other.
        /// </summary>
        public static bool operator !=(Vect a, Vect b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Add two vectors.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Vect operator +(Vect a, Vect b)
        {
            return new Vect(a.x + b.x, a.y + b.y);
        }

        /// <summary>
        /// Subtract two vectors.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Vect operator -(Vect a, Vect b)
        {
            return new Vect(a.x - b.x, a.y - b.y);
        }

        /// <summary>
        /// Negate a vector.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Vect operator -(Vect a)
        {
            return new Vect(-a.x, -a.y);
        }

        /// <summary>
        /// Scalar multiplication.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Vect operator *(Vect a, double s)
        {
            return new Vect(a.x * s, a.y * s);
        }

        /// <summary>
        /// Scalar division.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Vect operator /(Vect a, double s)
        {
            return new Vect(a.x / s, a.y / s);
        }

        /// <summary>
        /// Scalar multiplication.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Vect operator *(double s, Vect a)
        {
            return new Vect(a.x * s, a.y * s);
        }

        /// <summary>
        /// Scalar division.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Vect operator /(double s, Vect a)
        {
            return new Vect(a.x / s, a.y / s);
        }

        /// <summary>
        /// Vector dot product.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public double Dot(Vect v2)
        {
            return (x * v2.x) + (y * v2.y);
        }

        /// <summary>
        /// 2D vector cross product analog. The cross product of 2D vectors results in a 3D vector
        /// with only a z component. This function returns the magnitude of the z value.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public double Cross(Vect v2)
        {
            return (x * v2.y) - (y * v2.x);
        }

        /// <summary>
        /// Returns a perpendicular vector (-90 degree rotation).
        /// </summary>
        public Vect Perpendicurlar => new Vect(-y, x);

        /// <summary>
        /// Returns the vector projection of v1 onto v2.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vect Project(Vect v2)
        {
            return v2 * Dot(v2) / v2.Dot(v2);
        }

        /// <summary>
        /// Returns the unit length vector for the given angle (in radians).
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Vect ForAngle(double angle)
        {
            return new Vect(Math.Cos(angle), Math.Sin(angle));
        }

        /// <summary>
        /// Returns the angular direction v is pointing in (in radians).
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public double ToAngle()
        {
            return Math.Atan2(y, x);
        }

        /// <summary>
        /// Uses complex number multiplication to rotate v1 by v2. Scaling will occur if v1 is not a
        /// unit vector.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vect Rotate(Vect v2)
        {
            return new Vect((x * v2.x) - (y * v2.y), (x * v2.y) + (y * v2.x));
        }

        /// <summary>
        /// Inverse of Rotate().
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vect Unrotate(Vect v2)
        {
            return new Vect((x * v2.x) + (y * v2.y), (y * v2.x) - (x * v2.y));
        }

        /// <summary>
        /// Returns the squared length of v. Faster than <see cref="Length"/> when you only need to
        /// compare lengths.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public double LengthSquared()
        {
            return Dot(this);
        }

        /// <summary>
        /// Returns the length of v.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public double Length()
        {
            return Math.Sqrt(Dot(this));
        }

        /// <summary>
        /// Linearly interpolate between this and <paramref name="v2"/>.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vect Lerp(Vect v2, double t)
        {
            return (this * (1.0 - t)) + (v2 * t);
        }

        /// <summary>
        /// Returns a normalized copy.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vect Normalize()
        {
            return this * (1.0 / (Length() + double.Epsilon));
        }

#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static double Clamp(double value, double min, double max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Spherical linear interpolation between current position and <paramref name="v2"/> based
        /// on <paramref name="t"/>.
        /// </summary>
        public Vect SLerp(Vect v2, double t)
        {
            double dot = Normalize().Dot(v2.Normalize());
            double omega = Math.Acos(Clamp(dot, -1.0f, 1.0f));

            if (omega < 1e-3)
            {
                // If the angle between two vectors is very small, lerp instead to avoid precision issues.
                return Lerp(v2, t);
            }

            double denom = 1.0 / Math.Sin(omega);
            return (this * Math.Sin((1.0f - t) * omega) * denom) + (v2 * Math.Sin(t * omega) * denom);
        }

        /// <summary>
        /// Spherical linear interpolation between current position towards <paramref name="v2"/> by
        /// no more than angle <paramref name="a"/> radians.
        /// </summary>
        public Vect SLerpConst(Vect v2, double a)
        {
            double dot = Normalize().Dot(v2.Normalize());
            double omega = Math.Acos(Clamp(dot, -1.0f, 1.0f));

            return SLerp(v2, Math.Min(a, omega) / omega);
        }

        /// <summary>
        /// Clamp the magnitude to the given length.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vect Clamp(double length)
        {
            return Dot(this) > length * length ? Normalize() * length : this;
        }

        /// <summary>
        /// Linearly interpolate between the current position towards <paramref name="v2"/> by
        /// distance <paramref name="d"/>.
        /// </summary>
        public Vect LerpConst(Vect v2, double d)
        {
            return this + (v2 - this).Clamp(d);
        }

        /// <summary>
        /// Return the distance between this and <paramref name="v2"/>.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public double Distance(Vect v2)
        {
            return (this - v2).Length();
        }

        /// <summary>
        /// Return the squared distance between current position and <paramref name="v2"/>. Faster
        /// than <see cref="Distance"/> when you only need to compare distances.
        /// </summary>
#if !NET_4_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public double DistanceSquare(Vect v2)
        {
            return (this - v2).LengthSquared();
        }

        /// <summary>
        /// Return true if the distance between current position and <paramref name="v2"/> is less
        /// than <paramref name="distance"/>.
        /// </summary>
        public bool Near(Vect v2, double distance)
        {
            return DistanceSquare(v2) < distance * distance;
        }

        /// <summary>
        /// (0, 0) Vector.
        /// </summary>
        public static Vect Zero => zero;
    }
}

#pragma warning restore IDE1006
#pragma warning restore IDE0032