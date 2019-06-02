using System;

namespace ChipmunkBinding
{
    /// <summary>
    /// 
    /// </summary>
    public class PointQueryInfo : IEquatable<PointQueryInfo>
    {
#pragma warning disable IDE0032
        private readonly Shape shape;
        private readonly cpVect point;
        private readonly double distance;
        private readonly cpVect gradient;
#pragma warning restore IDE0032

        public Shape Shape => shape;
        public cpVect Point => point;
        public double Distance => distance;

        public cpVect Gradient => gradient;

        public PointQueryInfo(Shape s, cpVect p, double d, cpVect g)
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

        public static bool operator == (PointQueryInfo a, PointQueryInfo b)
        {
            if (a.shape.Handle != b.shape.Handle)
                return false;
            if (a.point != b.point)
                return false;
            if (a.gradient != b.gradient)
                return false;

            return Math.Abs(a.distance - b.distance) < float.Epsilon;
        }

        public static bool operator != (PointQueryInfo a, PointQueryInfo b)
        {
            return !(a == b);
        }

        internal cpPointQueryInfo ToQueryInfo()
        {
            var info = new cpPointQueryInfo();
            info.shape = shape.Handle;
            info.point = point;
            info.distance = distance;
            info.gradient = gradient;

            return info;
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
            return this == other;
        }
    }
}
