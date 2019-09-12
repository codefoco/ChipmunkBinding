
namespace ChipmunkBinding.Unsafe
{
    /// <summary>
    /// Unsafe extension methods for the <see cref="Circle"/> shape.
    /// </summary>
    public static class CircleExtensions
    {
        /// <summary>
        /// Change the radius of the circle shape.
        /// </summary>
        public static void SetRadius(this Circle circle, double radius)
        {
            NativeMethods.cpCircleShapeSetRadius(circle.Handle, radius);
        }

        /// <summary>
        /// Change the offset of the circle shape.
        /// </summary>
        public static void SetOffset(this Circle circle, Vect offset)
        {
            NativeMethods.cpCircleShapeSetOffset(circle.Handle, offset);
        }

    }
}
