
namespace ChipmunkBinding
{
    /// <summary>
    /// Interface to draw debug primitives (circle, point, segment)
    /// </summary>
    public interface IDebugDraw
    {
        /// <summary>
        /// Draw stroked circle
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="angle"></param>
        /// <param name="radius"></param>
        /// <param name="outlineColor"></param>
        /// <param name="fillColor"></param>
        void DrawCircle(cpVect pos, double angle, double radius, cpSpaceDebugColor outlineColor, cpSpaceDebugColor fillColor);

        /// <summary>
        /// Draws a line segment
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="color"></param>
        void DrawSegment(cpVect a, cpVect b, cpSpaceDebugColor color);

        /// <summary>
        /// Draws a thick line segment
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="radius"></param>
        /// <param name="outlineColor"></param>
        /// <param name="fillColor"></param>
        /// <param name="data"></param>
        void DrawFatSegment(cpVect a, cpVect b, double radius, cpSpaceDebugColor outlineColor, cpSpaceDebugColor fillColor);

        /// <summary>
        /// Draws a convex polygon
        /// </summary>
        /// <param name="vectors"></param>
        /// <param name="radius"></param>
        /// <param name="outlineColor"></param>
        /// <param name="fillColor"></param>
        void DrawPolygon(cpVect [] vectors, double radius, cpSpaceDebugColor outlineColor, cpSpaceDebugColor fillColor);

        /// <summary>
        /// Draws a dot
        /// </summary>
        /// <param name="size"></param>
        /// <param name="pos"></param>
        /// <param name="color"></param>
        void DrawDot(double size, cpVect pos, cpSpaceDebugColor color);
        
        /// <summary>
        /// Returns a color for a given shape. This gives you an opportunity to color shapes based on how they are used in your engine.
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        cpSpaceDebugColor ColorForShape(Shape shape);
    }
}
