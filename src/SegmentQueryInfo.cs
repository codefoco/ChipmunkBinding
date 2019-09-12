﻿using System;

namespace ChipmunkBinding
{
    /// <summary>
    /// Segment queries return where a shape was hit and its surface normal at the point of contact.
    /// Use <see cref="SegmentQueryInfo.Shape"/> == null to test if a shape was hit. Segment queries
    /// are like ray casting, but because not all spatial indexes allow processing infinitely long
    /// ray queries, it's limited to segments. In practice, this is still very fast and you don’t
    /// need to worry too much about the performance as long as you aren’t using extremely long
    /// segments for your queries.
    /// </summary>
    public sealed class SegmentQueryInfo : IEquatable<SegmentQueryInfo>
    {
#pragma warning disable IDE0032
        private readonly Shape shape;
        private readonly Vect point;
        private readonly Vect normal;
        private readonly double alpha;
#pragma warning restore IDE0032

        /// <summary>
        /// Shape that was hit, or None if no collision occured.
        /// </summary>
        public Shape Shape => shape;

        /// <summary>
        /// The point of impact.
        /// </summary>
        public Vect Point => point;

        /// <summary>
        /// The normal of the surface that was hit.
        /// </summary>
        public Vect Normal => normal;

        /// <summary>
        /// The normalized distance along the query segment in the range [0, 1]
        /// </summary>
        public double Alpha => alpha;

        /// <summary>
        /// Construct a Segment query info.
        /// </summary>
        public SegmentQueryInfo(Shape s, Vect p, Vect n, double a)
        {
            shape = s;
            point = p;
            normal = n;
            alpha = a;
        }

        /// <summary>
        /// Return true if the given object is reference-equal to this one.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as SegmentQueryInfo;

            if (other == null)
            {
                return false;
            }

            return this == other;
        }

        /// <summary>
        /// Return true if both objects are reference-equal to each other.
        /// </summary>
        public static bool operator ==(SegmentQueryInfo left, SegmentQueryInfo right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Return true if both objects are not reference-equal to each other.
        /// </summary>
        public static bool operator !=(SegmentQueryInfo a, SegmentQueryInfo b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Create a SegmentQuery from a native struct cpSegmentQueryInfo.
        /// </summary>
        internal static SegmentQueryInfo FromQueryInfo(cpSegmentQueryInfo queryInfo)
        {
            Shape shape;

            if (queryInfo.shape == IntPtr.Zero)
            {
                shape = null;
            }
            else
            {
                shape = Shape.FromHandle(queryInfo.shape);
            }

            return new SegmentQueryInfo(
                shape,
                queryInfo.point,
                queryInfo.normal,
                queryInfo.alpha);
        }

        /// <summary>
        /// Return true if the fields in both objects are equivalent and the <see cref="alpha"/>
        /// field is within <see cref="Single.Epsilon"/> of the other's.
        /// </summary>
        public bool Equals(SegmentQueryInfo other)
        {
            if (ReferenceEquals(other, null) ||
                shape?.Handle != other.shape?.Handle ||
                point != other.point ||
                normal != other.normal)
            {
                return false;
            }

            return Math.Abs(alpha - other.alpha) < float.Epsilon;
        }

        /// <summary>
        /// Get the hash code.
        /// </summary>
        public override int GetHashCode()
        {
            var hashCode = -1275187100;
            hashCode = hashCode * -1521134295 + shape.GetHashCode();
            hashCode = hashCode * -1521134295 + point.GetHashCode();
            hashCode = hashCode * -1521134295 + normal.GetHashCode();
            hashCode = hashCode * -1521134295 + alpha.GetHashCode();
            return hashCode;
        }
    }
}
