
namespace ChipmunkBinding
{
    /// <summary>
    /// A line segment shape between two points
    /// Meant mainly as a static shape.Can be beveled in order to give them a thickness.
    /// </summary>
    public class Segment : Shape
    {
        /// <summary>
        /// Create a Segment
        /// </summary>
        /// <param name="body">The body to attach the segment to</param>
        /// <param name="a">The first endpoint of the segment</param>
        /// <param name="b">The second endpoint of the segment</param>
        /// <param name="radius">The thickness of the segment</param>
        public Segment(Body body, Vect a, Vect b, double radius)
            : base(NativeMethods.cpSegmentShapeNew(body.Handle, a, b, radius))
        {
        }

        /// <summary>
        /// Let Chipmunk know about the geometry of adjacent segments to avoid colliding with endcaps.
        /// </summary>
        /// <param name="prev"></param>
        /// <param name="next"></param>
        public void SetNeighbors(Vect prev, Vect next)
        {
            NativeMethods.cpSegmentShapeSetNeighbors(Handle, prev, next);
        }

        /// <summary>
        /// Get the first endpoint of a segment shape.
        /// </summary>
        public Vect A => NativeMethods.cpSegmentShapeGetA(Handle);

        /// <summary>
        /// Get the second endpoint of a segment shape.
        /// </summary>
        public Vect B => NativeMethods.cpSegmentShapeGetB(Handle);

        /// <summary>
        /// Get the normal of a segment shape.
        /// </summary>
        public Vect Normal => NativeMethods.cpSegmentShapeGetNormal(Handle);

        /// <summary>
        /// Get segment radius
        /// </summary>
        public double Radius => NativeMethods.cpSegmentShapeGetRadius(Handle);

        /// <summary>
        /// Calculate the area of a fattened (capsule shaped) of this segment.
        /// </summary>
        public new double Area => AreaForSegment(A, B, Radius);

        /// <summary>
        /// Calculate the moment of inertia this segment.
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        public double MomentForMass(double mass)
        {
            return MomentForSegment(mass, A, B, Radius);
        }

        /// <summary>
        /// Calculate the moment of inertia for a line segment.
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static double MomentForSegment(double mass, Vect a, Vect b, double radius)
        {
            return NativeMethods.cpMomentForSegment(mass, a, b, radius);
        }

        /// <summary>
        /// Calculate the area of a fattened (capsule shaped) line segment.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static double AreaForSegment(Vect a, Vect b, double radius)
        {
            return NativeMethods.cpAreaForSegment(a, b, radius);
        }
    }
}
