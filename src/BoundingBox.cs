using System.Runtime.InteropServices;
using System;

#pragma warning disable IDE1006
#pragma warning disable IDE0032

namespace ChipmunkBinding
{
    /// <summary>
    /// Chipmunk's axis-aligned 2D bounding box type. (left, bottom, right, top)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct BoundingBox : IEquatable<BoundingBox>
    {
        private double left;
        private double bottom;
        private double right;
        private double top;

        /// <summary>
        /// Create a bounding box with given left, bottom, right and top
        /// </summary>
        /// <param name="left"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        /// <param name="top"></param>
        public BoundingBox(double left, double bottom, double right, double top)
        {
            this.left = left;
            this.bottom = bottom;
            this.right = right;
            this.top = top;
        }

        /// <summary>
        /// Left value of bounding box
        /// </summary>
        public double Left { get => left; set => left = value; }
        /// <summary>
        /// Bottom value of bouding box
        /// </summary>
        public double Bottom { get => bottom; set => bottom = value; }
        /// <summary>
        /// Right value of bouding box
        /// </summary>
        public double Right { get => right; set => right = value; }
        /// <summary>
        /// Top value of bouding box
        /// </summary>
        public double Top { get => top; set => top = value; }

        /// <summary>
        /// Check if a bounding box is equal to another
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(BoundingBox other)
        {
            return Math.Abs(left - other.left) < float.Epsilon &&
                   Math.Abs(bottom - other.bottom) < float.Epsilon &&
                   Math.Abs(right - other.right) < float.Epsilon &&
                   Math.Abs(top - other.top) < float.Epsilon;
        }

        /// <summary>
        /// Check if a bounding box is equal to a object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            BoundingBox? vect = obj as BoundingBox?;
            if (!vect.HasValue)
                return false;
            return this == vect.Value;
        }

        /// <summary>
        /// Bounding box hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = -1064806749;
            hashCode = hashCode * -1521134295 + left.GetHashCode();
            hashCode = hashCode * -1521134295 + bottom.GetHashCode();
            hashCode = hashCode * -1521134295 + right.GetHashCode();
            hashCode = hashCode * -1521134295 + top.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Bounding box ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
             return $"({left},{bottom},{right},{top})";
        }

        /// <summary>
        /// operator == for bounding box
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(BoundingBox left, BoundingBox right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///  operator != for bounding box
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(BoundingBox left, BoundingBox right)
        {
            return !(left == right);
        }
    }
}

#pragma warning restore IDE1006
#pragma warning restore IDE0032
