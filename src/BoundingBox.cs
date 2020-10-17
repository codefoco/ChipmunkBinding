using System.Runtime.InteropServices;
using System;

#pragma warning disable IDE1006
#pragma warning disable IDE0032

namespace ChipmunkBinding
{
    /// <summary>
    /// Chipmunk's axis-aligned 2D bounding box type.
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct BoundingBox : IEquatable<BoundingBox>
    {
        private double left;
        private double bottom;
        private double right;
        private double top;

        /// <summary>
        /// Create a bounding box with the given coordinates.
        /// </summary>
        public BoundingBox(double left, double bottom, double right, double top)
        {
            this.left = left;
            this.bottom = bottom;
            this.right = right;
            this.top = top;
        }

        /// <summary>
        /// Left value of bounding box.
        /// </summary>
        public double Left { get => left; set => left = value; }

        /// <summary>
        /// Bottom value of bouding box.
        /// </summary>
        public double Bottom { get => bottom; set => bottom = value; }

        /// <summary>
        /// Right value of bouding box.
        /// </summary>
        public double Right { get => right; set => right = value; }

        /// <summary>
        /// Top value of bouding box.
        /// </summary>
        public double Top { get => top; set => top = value; }

        /// <summary>
        /// Return true if the dimensions of both bounding boxes are equal to another (within
        /// <see cref="float.Epsilon"/> distance of each other.)
        /// </summary>
        public bool Equals(BoundingBox other)
        {
            return Math.Abs(left - other.left) < float.Epsilon &&
                   Math.Abs(bottom - other.bottom) < float.Epsilon &&
                   Math.Abs(right - other.right) < float.Epsilon &&
                   Math.Abs(top - other.top) < float.Epsilon;
        }

        /// <summary>
        /// Return true if the given object is reference-equal.
        /// </summary>
        public override bool Equals(object obj)
        {
            var bb = obj as BoundingBox?;
            if (bb == null)
            {
                return false;
            }

            return this == bb.Value;
        }

        /// <summary>
        /// Get the bounding box hash code.
        /// </summary>
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
        /// Return a string displaying coordinates formatted like (left, bottom, right, top).
        /// </summary>
        public override string ToString()
        {
             return $"({left},{bottom},{right},{top})";
        }

        /// <summary>
        /// Return true if the dimensions of both bounding boxes are within
        /// <see cref="float.Epsilon"/> of each other.
        /// </summary>
        public static bool operator ==(BoundingBox left, BoundingBox right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///  Return true if the dimensions of both bounding boxes are not within
        ///  <see cref="float.Epsilon"/> of each other.
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
