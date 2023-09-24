// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2023 Codefoco LTDA - The above copyright notice and this permission notice shall be
//     included in all copies or substantial portions of the Software.
//
//     Redistribution and use in source and binary forms, with or without
//     modification, are permitted only if explicitly approved by the authors.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
//     EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//     OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//     NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//     HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//     FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//     OTHER DEALINGS IN THE SOFTWARE.

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