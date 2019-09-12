
namespace ChipmunkBinding
{
    /// <summary>
    /// A circle shape defined by a radius
    /// This is the fastest and simplest collision shape
    /// </summary>
    public class Circle : Shape
    {
        /// <summary>
        /// Create and initialize a circle polygon shape.
        /// </summary>
        /// <param name="body">The body to attach the circle to.</param>
        /// <param name="radius">The radius of the circle.</param>
        public Circle(Body body, double radius)
            : this(body, radius, Vect.Zero)
        {
        }

        /// <summary>
        /// Create and initialize an offset circle polygon shape.
        /// </summary>
        /// <param name="body">The body to attach the circle to.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="offset">
        /// The offset from the body's center of gravity in coordinates local to the body.
        /// </param>
        public Circle(Body body, double radius, Vect offset)
            : base (NativeMethods.cpCircleShapeNew(body.Handle, radius, offset))
        {
        }

        /// <summary>
        /// Get the offset of the circle.
        /// </summary>
        public Vect Offset => NativeMethods.cpCircleShapeGetOffset(Handle);

        /// <summary>
        ///  Get the radius of the circle.
        /// </summary>
        public double Radius => NativeMethods.cpCircleShapeGetRadius(Handle);

        /// <summary>
        /// Get the calculated area of the circle.
        /// </summary>
        public new double Area => AreaForCircle(0.0, Radius);

        /// <summary>
        /// Calculate the moment of the circle for the given mass.
        /// </summary>
        public double MomentForMass(double mass)
        {
            return MomentForCircle(mass, Radius, Offset);
        }

        /// <summary>
        /// Calculate the moment of inertia for the circle.
        /// </summary>
        public static double MomentForCircle(double mass, double innerRadius, double radius, Vect offset)
        {
            return NativeMethods.cpMomentForCircle(mass, innerRadius, radius, offset);
        }

        /// <summary>
        /// Calculate the moment of inertia for the circle.
        /// </summary>
        public static double MomentForCircle(double mass, double radius, Vect offset)
        {
            return NativeMethods.cpMomentForCircle(mass, 0.0, radius, offset);
        }

        /// <summary>
        /// Calculate the area of a circle or donut.
        /// </summary>
        /// <param name="innerRadius">
        /// The radius of the 'donut hole', which defines the area missing.
        /// </param>
        /// <param name="radius">
        /// The outer radius of the donut. This should be greater than the
        /// <paramref name="innerRadius"/>.
        /// </param>
        public static double AreaForCircle(double innerRadius, double radius)
        {
            return NativeMethods.cpAreaForCircle(innerRadius, radius);
        }
    }
}
