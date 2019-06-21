using System.Runtime.InteropServices;
using System;

#pragma warning disable IDE1006
#pragma warning disable IDE0032

namespace ChipmunkBinding
{
    [StructLayout (LayoutKind.Sequential)]
    public struct BoundingBox : IEquatable<BoundingBox>
    {
        private double left;
        private double bottom;
        private double right;
        private double top;

        public BoundingBox(double left, double bottom, double right, double top)
        {
            this.left = left;
            this.bottom = bottom;
            this.right = right;
            this.top = top;
        }

        public double Left { get => left; set => left = value; }
        public double Bottom { get => bottom; set => bottom = value; }
        public double Right { get => right; set => right = value; }
        public double Top { get => top; set => top = value; }

        public bool Equals(BoundingBox other)
        {
            return Math.Abs(left - other.left) < float.Epsilon &&
                   Math.Abs(bottom - other.bottom) < float.Epsilon &&
                   Math.Abs(right - other.right) < float.Epsilon &&
                   Math.Abs(top - other.top) < float.Epsilon;
        }

        public override bool Equals(object obj)
        {
            BoundingBox? vect = obj as BoundingBox?;
            if (!vect.HasValue)
                return false;
            return this == vect.Value;
        }

        public override int GetHashCode()
        {
            var hashCode = -1064806749;
            hashCode = hashCode * -1521134295 + left.GetHashCode();
            hashCode = hashCode * -1521134295 + bottom.GetHashCode();
            hashCode = hashCode * -1521134295 + right.GetHashCode();
            hashCode = hashCode * -1521134295 + top.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
             return $"({left},{bottom},{right},{top})";
        }

        public static bool operator ==(BoundingBox left, BoundingBox right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BoundingBox left, BoundingBox right)
        {
            return !(left == right);
        }
    }
}

#pragma warning restore IDE1006
#pragma warning restore IDE0032
