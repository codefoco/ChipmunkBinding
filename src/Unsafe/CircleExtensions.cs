
namespace ChipmunkBinding.Unsafe
{
    public static class CircleExtensions
    {
        /// <summary>
        /// Set the radius of a circle shape.
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="radius"></param>
        public static void SetRadius(this Circle circle, double radius)
        {
            NativeMethods.cpCircleShapeSetRadius(circle.Handle, radius);
        }

        /// <summary>
        /// Set the offset of a circle shape.
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="offset"></param>
        public static void SetOffset(this Circle circle, Vect offset)
        {
            NativeMethods.cpCircleShapeSetOffset(circle.Handle, offset);
        }

    }
}
