
namespace ChipmunkBinding
{
    /// <summary>
    /// A line segment shape between two points, which is mainly useful when it behaves statically,
    /// though it can be beveled to give it thickness.
    /// </summary>
    public class Segment : Shape
    {
        /// <summary>
        /// Create a line segment.
        /// </summary>
        /// <param name="body">The body to attach the segment to.</param>
        /// <param name="a">The first endpoint of the segment.</param>
        /// <param name="b">The second endpoint of the segment.</param>
        /// <param name="radius">The thickness of the segment.</param>
        public Segment(Body body, Vect a, Vect b, double radius)
            : base(NativeMethods.cpSegmentShapeNew(body.Handle, a, b, radius))
        {
        }

        /// <summary>
        /// Let Chipmunk know about the geometry of adjacent segments to avoid colliding with endcaps.
        /// </summary>
        public void SetNeighbors(Vect prev, Vect next)
        {
            NativeMethods.cpSegmentShapeSetNeighbors(Handle, prev, next);
        }

        /// <summary>
        /// Get the first endpoint of the segment shape.
        /// </summary>
        public Vect A => NativeMethods.cpSegmentShapeGetA(Handle);

        /// <summary>
        /// Get the second endpoint of the segment shape.
        /// </summary>
        public Vect B => NativeMethods.cpSegmentShapeGetB(Handle);

        /// <summary>
        /// Get the normal of the segment shape.
        /// </summary>
        public Vect Normal => NativeMethods.cpSegmentShapeGetNormal(Handle);

        /// <summary>
        /// Get the segment radius.
        /// </summary>
        public double Radius => NativeMethods.cpSegmentShapeGetRadius(Handle);

        /// <summary>
        /// Calculate the area of the segment, assuming a thickness has been provided. The area is
        /// calculated assuming the endpoints would be rounded, like a capsule.
        /// </summary>
        public new double Area => AreaForSegment(A, B, Radius);

        /// <summary>
        /// Calculate the moment of inertia of the segment.
        /// </summary>
        public double MomentForMass(double mass)
        {
            return MomentForSegment(mass, A, B, Radius);
        }

        /// <summary>
        /// Calculate the moment of inertia for the line segment.
        /// </summary>
        public static double MomentForSegment(double mass, Vect a, Vect b, double radius)
        {
            return NativeMethods.cpMomentForSegment(mass, a, b, radius);
        }

        /// <summary>
        /// Calculate the area of a segment, assuming a thickness has been provided. The area is
        /// calculated assuming the endpoints would be rounded, like a capsule.
        /// </summary>
        public static double AreaForSegment(Vect a, Vect b, double radius)
        {
            return NativeMethods.cpAreaForSegment(a, b, radius);
        }
    }
}
