using System;

namespace ChipmunkBinding
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PointQueryInfo : IEquatable<PointQueryInfo>
    {
#pragma warning disable IDE0032
        private readonly Shape shape;
        private readonly Vect point;
        private readonly double distance;
        private readonly Vect gradient;
#pragma warning restore IDE0032

        public Shape Shape => shape;
        public Vect Point => point;
        public double Distance => distance;

        public Vect Gradient => gradient;

        public PointQueryInfo(Shape s, Vect p, double d, Vect g)
        {
            shape = s;
            point = p;
            distance = d;
            gradient = g;
        }

        public override bool Equals(object obj)
        {
            var other = obj as PointQueryInfo;
            if (other == null)
                return false;

            return this == other;
        }

        public override int GetHashCode()
        {
            return shape.Handle.ToInt32() ^ 
                point.GetHashCode() ^ 
                distance.GetHashCode() ^ 
                (gradient.GetHashCode () << 4); 
        }

        public static bool operator == (PointQueryInfo left, PointQueryInfo right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator != (PointQueryInfo a, PointQueryInfo b)
        {
            return !(a == b);
        }

        internal static PointQueryInfo FromQueryInfo(cpPointQueryInfo queryInfo)
        {
            var shape = Shape.FromHandle(queryInfo.shape);
            return new PointQueryInfo(shape,
                                      queryInfo.point,
                                      queryInfo.distance,
                                      queryInfo.gradient);
        }

        public bool Equals(PointQueryInfo other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (shape.Handle != other.shape.Handle)
                return false;
            if (point != other.point)
                return false;
            if (gradient != other.gradient)
                return false;

            return Math.Abs(distance - other.distance) < float.Epsilon;
        }
    }
}
