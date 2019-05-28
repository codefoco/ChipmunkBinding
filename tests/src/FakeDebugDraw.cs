

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

        public cpSpaceDebugColor ColorForShape(Shape shape)
        {
            return new cpSpaceDebugColor(0, 0, 1);
        }

        public string TracedCalls => stringBuilder.ToString();

        public void DrawCircle(cpVect pos, double angle, double radius, cpSpaceDebugColor outlineColor, cpSpaceDebugColor fillColor)
        {
            stringBuilder.Append($"DrawCircle\n");
            stringBuilder.Append($"pos = {pos.X}, {pos.Y}\n");
            stringBuilder.Append($"angle = {angle}\n");
            stringBuilder.Append($"radius = {radius}\n");
            stringBuilder.Append($"outlineColor = {outlineColor}\n");
            stringBuilder.Append($"outlineColor = {fillColor}\n");
        }

        public void DrawDot(double size, cpVect pos, cpSpaceDebugColor color)
        {
            stringBuilder.Append($"DrawDot\n");
            stringBuilder.Append($"size = {size}\n");
            stringBuilder.Append($"pos = {pos.X}, {pos.Y}\n");
            stringBuilder.Append($"color = {color}\n");
        }

        public void DrawFatSegment(cpVect a, cpVect b, double radius, cpSpaceDebugColor outlineColor, cpSpaceDebugColor fillColor)
        {
            stringBuilder.Append($"DrawFatSegment\n");
            stringBuilder.Append($"a = {a}\n");
            stringBuilder.Append($"b = {b}\n");
            stringBuilder.Append($"radius = {radius}\n");
            stringBuilder.Append($"outlineColor = {outlineColor}\n");
            stringBuilder.Append($"fillColor = {fillColor}\n");
        }

        public void DrawPolygon(cpVect[] vectors, double radius, cpSpaceDebugColor outlineColor, cpSpaceDebugColor fillColor)
        {
            stringBuilder.Append($"DrawPolygon\n");

            for (int i = 0; i < vectors.Length; i++)
                stringBuilder.Append($"vectors[{i}] = {vectors[i]}\n");
            stringBuilder.Append($"radius = {radius}\n");
            stringBuilder.Append($"outlineColor = {outlineColor}\n");
            stringBuilder.Append($"fillColor = {fillColor}\n");
        }

        public void DrawSegment(cpVect a, cpVect b, cpSpaceDebugColor color)
        {
            stringBuilder.Append($"DrawFatSegment\n");
            stringBuilder.Append($"a = {a}\n");
            stringBuilder.Append($"b = {b}\n");
            stringBuilder.Append($"color = {color}\n");
        }
    }
}
