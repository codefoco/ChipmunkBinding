using System;

namespace ChipmunkBinding
{
    /// <summary>
    /// <see cref="PointQueryInfo"/> holds the result of a point query made on a <see cref="Shape"/>
    /// or <see cref="Space"/>.
    /// </summary>
    public sealed class PointQueryInfo : IEquatable<PointQueryInfo>
    {
#pragma warning disable IDE0032
        private readonly Shape shape;
        private readonly Vect point;
        private readonly double distance;
        private readonly Vect gradient;
#pragma warning restore IDE0032

        /// <summary>
        /// The nearest shape, None if no shape was within range.
        /// </summary>
        public Shape Shape => shape;

        /// <summary>
        /// The closest point on the shape’s surface (in world space coordinates).
        /// </summary>
        public Vect Point => point;

        /// <summary>
        /// The distance to the point. The distance is negative if the point is inside the shape.
        /// </summary>
        public double Distance => distance;

        /// <summary>
        /// The gradient of the signed distance function.
        /// </summary>
        public Vect Gradient => gradient;

        /// <summary>
        /// Create a <see cref="PointQueryInfo"/>.
        /// </summary>
        public PointQueryInfo(Shape s, Vect p, double d, Vect g)
        {
            shape = s;
            point = p;
            distance = d;
            gradient = g;
        }

        /// <summary>
        /// Return true if this <see cref="PointQueryInfo"/> object is reference-equal to the given
        /// object.
        /// </summary>
        public override bool Equals(object obj)
        {
            var other = obj as PointQueryInfo;

            if (other == null)
            {
                return false;
            }

            return this == other;
        }

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return shape.Handle.ToInt32() ^
                point.GetHashCode() ^
                distance.GetHashCode() ^
                (gradient.GetHashCode() << 4);
        }

        /// <summary>
        /// Return true if this <see cref="PointQueryInfo"/> object is reference-equal to the given
        /// object.
        /// </summary>
        public static bool operator ==(PointQueryInfo left, PointQueryInfo right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Return true if this <see cref="PointQueryInfo"/> object is not reference-equal to the
        /// given object.
        /// </summary>
        public static bool operator !=(PointQueryInfo a, PointQueryInfo b)
        {
            return !(a == b);
        }

        internal static PointQueryInfo FromQueryInfo(cpPointQueryInfo queryInfo)
        {
            var shape = Shape.FromHandle(queryInfo.shape);

            return new PointQueryInfo(
                shape,
                queryInfo.point,
                queryInfo.distance,
                queryInfo.gradient);
        }

        /// <summary>
        /// Return true if this <see cref="PointQueryInfo"/> object's distance is within
        /// <see cref="Single.Epsilon"/> of the other and all other fields are equivalent.
        /// </summary>
        public bool Equals(PointQueryInfo other)
        {
            if (ReferenceEquals(other, null) ||
                shape.Handle != other.shape.Handle ||
                point != other.point ||
                gradient != other.gradient)
            {
                return false;
            }

            return Math.Abs(distance - other.distance) < float.Epsilon;
        }
    }
}
