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

using System.Text;

using ChipmunkBinding;

namespace ChipmunkBindingTest
{
    internal sealed class FakeDebugDraw : IDebugDraw
    {
        private readonly StringBuilder stringBuilder;

        public FakeDebugDraw()
        {
            stringBuilder = new StringBuilder();
        }

        public DebugColor ColorForShape(Shape shape)
        {
            return new DebugColor(0, 0, 1);
        }

        public string TracedCalls => stringBuilder.ToString();

#pragma warning disable CA1305 // Specify IFormatProvider
#pragma warning disable IDE0058 // Expression value is never used

        public void DrawCircle(Vect pos, double angle, double radius, DebugColor outlineColor, DebugColor fillColor)
        {
            stringBuilder.Append("DrawCircle\n");
            stringBuilder.Append($"pos = {pos.X}, {pos.Y}\n");
            stringBuilder.Append($"angle = {angle}\n");
            stringBuilder.Append($"radius = {radius}\n");
            stringBuilder.Append($"outlineColor = {outlineColor}\n");
            stringBuilder.Append($"outlineColor = {fillColor}\n");
        }

        public void DrawDot(double size, Vect pos, DebugColor color)
        {
            stringBuilder.Append("DrawDot\n");
            stringBuilder.Append($"size = {size}\n");
            stringBuilder.Append($"pos = {pos.X}, {pos.Y}\n");
            stringBuilder.Append($"color = {color}\n");
        }

        public void DrawFatSegment(Vect a, Vect b, double radius, DebugColor outlineColor, DebugColor fillColor)
        {
            stringBuilder.Append("DrawFatSegment\n");
            stringBuilder.Append($"a = {a}\n");
            stringBuilder.Append($"b = {b}\n");
            stringBuilder.Append($"radius = {radius}\n");
            stringBuilder.Append($"outlineColor = {outlineColor}\n");
            stringBuilder.Append($"fillColor = {fillColor}\n");
        }

        public void DrawPolygon(Vect[] vectors, double radius, DebugColor outlineColor, DebugColor fillColor)
        {
            stringBuilder.Append("DrawPolygon\n");

            for (int i = 0; i < vectors.Length; i++)
                stringBuilder.Append($"vectors[{i}] = {vectors[i]}\n");
            stringBuilder.Append($"radius = {radius}\n");
            stringBuilder.Append($"outlineColor = {outlineColor}\n");
            stringBuilder.Append($"fillColor = {fillColor}\n");
        }

        public void DrawSegment(Vect a, Vect b, DebugColor color)
        {
            stringBuilder.Append("DrawFatSegment\n");
            stringBuilder.Append($"a = {a}\n");
            stringBuilder.Append($"b = {b}\n");
            stringBuilder.Append($"color = {color}\n");
        }
#pragma warning restore IDE0058 // Expression value is never used
#pragma warning restore CA1305 // Specify IFormatProvider
    }
}