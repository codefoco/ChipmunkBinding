using System;

namespace ChipmunkBinding
{
    /// <summary>
    /// Segment query info:
    /// Segment queries return more information than just a simple yes or no, they also return where a shape was hit and it’s surface normal at the hit point. This object hold that information.
    /// To test if the query hit something, check if SegmentQueryInfo.Shape == null or not.
    /// Segment queries are like ray casting, but because not all spatial indexes allow processing infinitely long ray queries it is limited to segments. In practice this is still very fast and you don’t need to worry too much about the performance as long as you aren’t using extremely long segments for your queries.
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
        /// Shape that was hit, or None if no collision occured
        /// </summary>
        public Shape Shape => shape;
        /// <summary>
        /// The point of impact.
        /// </summary>
        public Vect Point => point;
        /// <summary>
        /// The normal of the surface hit.
        /// </summary>
        public Vect Normal => normal;
        /// <summary>
        /// The normalized distance along the query segment in the range [0, 1]
        /// </summary>
        public double Alpha => alpha;

        /// <summary>
        /// Construct a Segment query info.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <param name="n"></param>
        /// <param name="a"></param>
        public SegmentQueryInfo(Shape s, Vect p, Vect n, double a)
        {
            shape = s;
            point = p;
            normal = n;
            alpha = a;
        }

        public override bool Equals(object obj)
        {
            var other = obj as SegmentQueryInfo;
            if (other == null)
                return false;

            return this == other;
        }

        public static bool operator ==(SegmentQueryInfo left, SegmentQueryInfo right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(SegmentQueryInfo a, SegmentQueryInfo b)
        {
            return !(a == b);
        }

        internal static SegmentQueryInfo FromQueryInfo(cpSegmentQueryInfo queryInfo)
        {
            Shape shape;

            if (queryInfo.shape == IntPtr.Zero)
                shape = null;
            else
                shape = Shape.FromHandle(queryInfo.shape);

            return new SegmentQueryInfo(shape,
                                        queryInfo.point,
                                        queryInfo.normal,
                                        queryInfo.alpha);
        }

        public bool Equals(SegmentQueryInfo other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (shape?.Handle != other.shape?.Handle)
                return false;
            if (point != other.point)
                return false;
            if (normal != other.normal)
                return false;

            return Math.Abs(alpha - other.alpha) < float.Epsilon;
        }

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
