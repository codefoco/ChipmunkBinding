namespace ChipmunkBinding.Unsafe
{
    /// <summary>
    /// Unsafe Segment shape extensions
    /// </summary>
    public static class SegmentExtensions
    {
        /// <summary>
        /// Set the endpoints of a segment shape.
        /// These functions are used for mutating collision shapes.
        /// Chipmunk does not have any way to get velocity information on changing shapes,
        /// so the results will be unrealistic. You must explicity include the chipmunk_unsafe.h header to use them.
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void SetEndpoints(this Segment segment, Vect a, Vect b)
        {
            NativeMethods.cpSegmentShapeSetEndpoints(segment.Handle, a, b);
        }

        /// <summary>
        /// Set the radius of a segment shape.
        /// These functions are used for mutating collision shapes.
        /// Chipmunk does not have any way to get velocity information on changing shapes,
        /// so the results will be unrealistic. You must explicity include the chipmunk_unsafe.h header to use them.
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="radius"></param>
        public static void SetRadius(this Segment segment, double radius)
        {
            NativeMethods.cpSegmentShapeSetRadius(segment.Handle, radius);
        }
    }
}
