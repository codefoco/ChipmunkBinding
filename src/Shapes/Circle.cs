
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
        public Circle(Body body, double radius): this(body, radius, Vect.Zero)
        {

        }

        /// <summary>
        /// body is the body attach the circle to, offset is the offset from the body’s center of gravity in body local coordinates.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="radius"></param>
        /// <param name="offset"></param>
        public Circle(Body body, double radius, Vect offset)
            :base (NativeMethods.cpCircleShapeNew(body.Handle, radius, offset))
        {
        }

        /// <summary>
        /// Offset of a circle shape.
        /// </summary>
        public Vect Offset => NativeMethods.cpCircleShapeGetOffset(Handle);

        /// <summary>
        ///  Get the radius of a circle shape.
        /// </summary>
        public double Radius => NativeMethods.cpCircleShapeGetRadius(Handle);

        /// <summary>
        /// Calculate area this circle.
        /// </summary>
        public new double Area => AreaForCircle(0.0, Radius);

        /// <summary>
        /// Calculate moment of this circle for the given mass
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        public double MomentForMass(double mass)
        {
            return MomentForCircle(mass, Radius, Offset);
        }

        /// <summary>
        /// Calculate the moment of inertia for a circle.
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="innerRadius"></param>
        /// <param name="radius"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static double MomentForCircle(double mass, double innerRadius, double radius, Vect offset)
        {
            return NativeMethods.cpMomentForCircle(mass, innerRadius, radius, offset);
        }

        /// <summary>
        /// Calculate the moment of inertia for a circle.
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="radius"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static double MomentForCircle(double mass, double radius, Vect offset)
        {
            return NativeMethods.cpMomentForCircle(mass, 0.0, radius, offset);
        }

        /// <summary>
        /// Calculate area of a hollow circle.
        /// </summary>
        /// <param name="innerRadius"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static double AreaForCircle(double innerRadius, double radius)
        {
            return NativeMethods.cpAreaForCircle(innerRadius, radius);
        }
    }
}
