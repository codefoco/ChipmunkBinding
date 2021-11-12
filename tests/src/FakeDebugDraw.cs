

using System.Text;
using ChipmunkBinding;

namespace ChipmunkBindingTest
{
    class FakeDebugDraw : IDebugDraw
    {
        StringBuilder stringBuilder;

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
            stringBuilder.Append($"DrawCircle\n");
            stringBuilder.Append($"pos = {pos.X}, {pos.Y}\n");
            stringBuilder.Append($"angle = {angle}\n");
            stringBuilder.Append($"radius = {radius}\n");
            stringBuilder.Append($"outlineColor = {outlineColor}\n");
            stringBuilder.Append($"outlineColor = {fillColor}\n");
        }

        public void DrawDot(double size, Vect pos, DebugColor color)
        {
            stringBuilder.Append($"DrawDot\n");
            stringBuilder.Append($"size = {size}\n");
            stringBuilder.Append($"pos = {pos.X}, {pos.Y}\n");
            stringBuilder.Append($"color = {color}\n");
        }

        public void DrawFatSegment(Vect a, Vect b, double radius, DebugColor outlineColor, DebugColor fillColor)
        {
            stringBuilder.Append($"DrawFatSegment\n");
            stringBuilder.Append($"a = {a}\n");
            stringBuilder.Append($"b = {b}\n");
            stringBuilder.Append($"radius = {radius}\n");
            stringBuilder.Append($"outlineColor = {outlineColor}\n");
            stringBuilder.Append($"fillColor = {fillColor}\n");
        }

        public void DrawPolygon(Vect[] vectors, double radius, DebugColor outlineColor, DebugColor fillColor)
        {
            stringBuilder.Append($"DrawPolygon\n");

            for (int i = 0; i < vectors.Length; i++)
                stringBuilder.Append($"vectors[{i}] = {vectors[i]}\n");
            stringBuilder.Append($"radius = {radius}\n");
            stringBuilder.Append($"outlineColor = {outlineColor}\n");
            stringBuilder.Append($"fillColor = {fillColor}\n");
        }

        public void DrawSegment(Vect a, Vect b, DebugColor color)
        {
            stringBuilder.Append($"DrawFatSegment\n");
            stringBuilder.Append($"a = {a}\n");
            stringBuilder.Append($"b = {b}\n");
            stringBuilder.Append($"color = {color}\n");
        }
#pragma warning restore IDE0058 // Expression value is never used
#pragma warning restore CA1305 // Specify IFormatProvider
    }
}
