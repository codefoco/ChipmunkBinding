using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
        /// Create Vect
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vect(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// object Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Vect? vect = obj as Vect?;
            if (!vect.HasValue)
                return false;
            return this == vect.Value;
        }

        /// <summary>
        /// object GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (x.GetHashCode() << 16) ^ y.GetHashCode();
        }

        /// <summary>
        /// IEquatable Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vect other)
        {
            return this == other;
        }

        /// <summary>
        /// object ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"({x},{y})";
        }

        /// <summary>
        /// operator ==
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Vect a, Vect b)
        {
            return Math.Abs(a.x - b.x) < double.Epsilon &&
                   Math.Abs(a.y - b.y) < double.Epsilon;
        }

        /// <summary>
        /// operator !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Vect a, Vect b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Add two vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vect operator +(Vect a, Vect b)
        {
            return new Vect(a.x + b.x, a.y + b.y);
        }

        /// <summary>
        /// Subtract two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vect operator -(Vect a, Vect b)
        {
            return new Vect(a.x - b.x, a.y - b.y);
        }

        /// <summary>
        /// Negate a vector.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vect operator -(Vect a)
        {
            return new Vect(-a.x, -a.y);
        }

        /// <summary>
        /// Scalar multiplication.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vect operator *(Vect a, double s)
        {
            return new Vect(a.x * s, a.y * s);
        }

        /// <summary>
        /// Scalar division
        /// </summary>
        /// <param name="a"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vect operator /(Vect a, double s)
        {
            return new Vect(a.x / s, a.y / s);
        }

        /// <summary>
        /// Scalar multiplication.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vect operator *(double s, Vect a)
        {
            return new Vect(a.x * s, a.y * s);
        }

        /// <summary>
        /// Scalar division
        /// </summary>
        /// <param name="s"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vect operator /(double s, Vect a)
        {
            return new Vect(a.x / s, a.y / s);
        }

        /// <summary>
        /// Vector dot product.
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public double Dot(Vect v2)
        {
            return x * v2.x + y * v2.y;
        }

        /// <summary>
        /// 2D vector cross product analog.
        /// The cross product of 2D vectors results in a 3D vector with only a z component.
        /// This function returns the magnitude of the z value.
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public double Cross(Vect v2)
        {
            return x * v2.y - y * v2.x;
        }

        /// <summary>
        /// Returns a perpendicular vector. (-90 degree rotation)
        /// </summary>
        public Vect Perpendicurlar => new Vect(- y, x);

        /// <summary>
        /// Returns the vector projection of v1 onto v2.
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vect Project(Vect v2)
        {
            return v2 * Dot(v2) / v2.Dot(v2);
        }

        /// <summary>
        /// Returns the unit length vector for the given angle (in radians).
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vect ForAngle(double angle)
        {
            return new Vect(Math.Cos(angle), Math.Sin(angle));
        }

        /// <summary>
        /// Returns the angular direction v is pointing in (in radians).
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ToAngle()
        {
            return Math.Atan2(y, x);
        }

        /// <summary>
        /// Uses complex number multiplication to rotate v1 by v2. Scaling will occur if v1 is not a unit vector.
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vect Rotate(Vect v2)
        {
            return new Vect(x * v2.x - y * v2.y, x * v2.y + y * v2.x);
        }

        /// <summary>
        /// Inverse of Rotate().
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vect Unrotate(Vect v2)
        {
            return new Vect(x * v2.x + y * v2.y, y * v2.x - x * v2.y);
        }

        /// <summary>
        /// Returns the squared length of v. Faster than Length() when you only need to compare lengths.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double LengthSquared() => Dot(this);

        /// <summary>
        /// Returns the length of v.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Length()
        {
            return Math.Sqrt(Dot(this));
        }

        /// <summary>
        /// Linearly interpolate between this and v2.
        /// </summary>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vect Lerp(Vect v2, double t)
        {
            return this * (1.0 - t) + v2 * t;
        }

        /// <summary>
        /// Returns a normalized copy.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vect Normalize()
        {
            return this * (1.0 / (Length() + double.Epsilon));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static double Clamp(double value, double min, double max)
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
        /// Spherical linearly interpolate between v1 and v2.
        /// </summary>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
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
            return (this * Math.Sin((1.0f - t) * omega) * denom) + v2 * Math.Sin(t * omega) * denom;
        }

        /// <summary>
        /// Spherical linearly interpolate between v1 towards v2 by no more than angle a radians
        /// </summary>
        /// <param name="v2"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public Vect SLerpConst(Vect v2, double a)
        {
            double dot = Normalize().Dot(v2.Normalize());
            double omega = Math.Acos(Clamp(dot, -1.0f, 1.0f));

            return SLerp(v2, Math.Min(a, omega) / omega);
        }

        /// <summary>
        /// Clamp v to length len.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vect Clamp(double length)
        {
            return Dot(this) > length * length ? Normalize() * length : this;
        }

        /// <summary>
        /// Linearly interpolate between v1 towards v2 by distance d.
        /// </summary>
        /// <param name="v2"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public Vect LerpConst(Vect v2, double d)
        {
            return this + (v2 - this).Clamp(d);
        }

        /// <summary>
        /// Returns the distance between this and v2.
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Distance(Vect v2)
        {
            return (this - v2).Length();
        }

        /// <summary>
        /// Returns the squared distance between v1 and v2. Faster than cpvdist() when you only need to compare distances.
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double DistanceSquare(Vect v2)
        {
            return (this - v2).LengthSquared();
        }

        /// <summary>
        /// Returns true if the distance between v1 and v2 is less than dist.
        /// </summary>
        /// <param name="v2"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public bool Near(Vect v2, double distance)
        {
            return DistanceSquare(v2) < distance * distance;
        }

        /// <summary>
        /// (0, 0) Vector
        /// </summary>
        public static Vect Zero => zero;
    }
}

#pragma warning restore IDE1006
#pragma warning restore IDE0032
