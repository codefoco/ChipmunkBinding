
namespace ChipmunkBinding
{
    /// <summary>
    /// A circle shape defined by a radius
    /// This is the fastest and simplest collision shape
    /// </summary>
    public class Circle : Shape
    {
        /// <summary>
        /// body is the body attach the circle to, offset is the offset from the body’s center of gravity in body local coordinates.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="radius"></param>
        public Circle(Body body, double radius): this(body, radius, cpVect.Zero)
        {

        }

        /// <summary>
        /// body is the body attach the circle to, offset is the offset from the body’s center of gravity in body local coordinates.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="radius"></param>
        /// <param name="offset"></param>
        public Circle(Body body, double radius, cpVect offset)
            :base (NativeMethods.cpCircleShapeNew(body.Handle, radius, offset))
        {
        }

        /// <summary>
        /// Offset of a circle shape.
        /// </summary>
        public cpVect Offset => NativeMethods.cpCircleShapeGetOffset(Handle);

        /// <summary>
        ///  Get the radius of a circle shape.
        /// </summary>
        public double Radius => NativeMethods.cpCircleShapeGetRadius(Handle);

    }
}
