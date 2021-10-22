
namespace ChipmunkBinding
{
    /// <summary>
    /// Interface to draw debug primitives (circle, point, segment).
    /// </summary>
    public interface IDebugDraw
    {
        /// <summary>
        /// Draw stroked circle.
        /// </summary>
        void DrawCircle(Vect pos, double angle, double radius, DebugColor outlineColor, DebugColor fillColor);

        /// <summary>
        /// Draws a line segment.
        /// </summary>
        void DrawSegment(Vect a, Vect b, DebugColor color);

        /// <summary>
        /// Draws a thick line segment.
        /// </summary>
        void DrawFatSegment(Vect a, Vect b, double radius, DebugColor outlineColor, DebugColor fillColor);

        /// <summary>
        /// Draws a convex polygon.
        /// </summary>
        void DrawPolygon(Vect[] vectors, double radius, DebugColor outlineColor, DebugColor fillColor);

        /// <summary>
        /// Draws a dot.
        /// </summary>
        void DrawDot(double size, Vect pos, DebugColor color);

        /// <summary>
        /// Returns a color for a given shape. This gives you an opportunity to color shapes based
        /// on how they are used in your engine.
        /// </summary>
        DebugColor ColorForShape(Shape shape);
    }
}
