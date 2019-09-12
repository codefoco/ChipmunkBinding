namespace ChipmunkBinding.Unsafe
{
    /// <summary>
    /// Unsafe extensions methods for the <see cref="Segment"/> shape.
    /// </summary>
    public static class SegmentExtensions
    {
        /// <summary>
        /// Set the endpoints of a segment shape. This mutates collision shapes. Chipmunk can't get
        /// velocity information on changing shapes, so the results will be unrealistic.
        /// </summary>
        public static void SetEndpoints(this Segment segment, Vect a, Vect b)
        {
            NativeMethods.cpSegmentShapeSetEndpoints(segment.Handle, a, b);
        }

        /// <summary>
        /// Set the radius of a segment shape. This mutates collision shapes. Chipmunk can't get
        /// velocity information on changing shapes, so the results will be unrealistic.
        /// </summary>
        public static void SetRadius(this Segment segment, double radius)
        {
            NativeMethods.cpSegmentShapeSetRadius(segment.Handle, radius);
        }
    }
}
