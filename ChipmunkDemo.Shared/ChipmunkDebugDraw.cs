using System;
using System.Linq;
using ChipmunkBinding;
using Microsoft.Xna.Framework;

namespace ChipmunkDemo
{
    public class ChipmunkDebugDraw : IDebugDraw
    {
        static readonly DebugColor[] Colors = {
            new DebugColor(0xb5/255.0f, 0x89/255.0f, 0x00/255.0f, 1.0f),
            new DebugColor(0xcb/255.0f, 0x4b/255.0f, 0x16/255.0f, 1.0f),
            new DebugColor(0xdc/255.0f, 0x32/255.0f, 0x2f/255.0f, 1.0f),
            new DebugColor(0xd3/255.0f, 0x36/255.0f, 0x82/255.0f, 1.0f),
            new DebugColor(0x6c/255.0f, 0x71/255.0f, 0xc4/255.0f, 1.0f),
            new DebugColor(0x26/255.0f, 0x8b/255.0f, 0xd2/255.0f, 1.0f),
            new DebugColor(0x2a/255.0f, 0xa1/255.0f, 0x98/255.0f, 1.0f),
            new DebugColor(0x85/255.0f, 0x99/255.0f, 0x00/255.0f, 1.0f)
        };

        PrimitiveBatch primitiveBatch;

        public ChipmunkDebugDraw(PrimitiveBatch primitiveBatch)
        {
            this.primitiveBatch = primitiveBatch;
        }

        public DebugColor ColorForShape(Shape shape)
        {
            if (shape.Sensor)
                return new DebugColor(1.0f, 1.0f, 1.0f, 0.1f);

            Body body = shape.Body;

            if (body.IsSleeping)
                return new DebugColor(0x58 / 255.0f, 0x6e / 255.0f, 0x75 / 255.0f, 1.0f);

            uint val = (uint)shape.GetHashCode();

            // scramble the bits up using Robert Jenkins' 32 bit integer hash function
            val = (val + 0x7ed55d16) + (val << 12);
            val = (val ^ 0xc761c23c) ^ (val >> 19);
            val = (val + 0x165667b1) + (val << 5);
            val = (val + 0xd3a2646c) ^ (val << 9);
            val = (val + 0xfd7046c5) + (val << 3);
            val = (val ^ 0xb55a4f09) ^ (val >> 16);

            return Colors[val & 0x7];
        }

        public void DrawCircle(Vect pos, double angle, double radius, DebugColor outlineColor, DebugColor fillColor)
        {
            var center = new Vector2((float)pos.X, (float)pos.Y);

            primitiveBatch.DrawCircle(center, (float)radius, 
                new Color(fillColor.Red, fillColor.Green, fillColor.Blue),
                new Color(outlineColor.Red, outlineColor.Green, outlineColor.Blue)
                );
        }

        public void DrawDot(double size, Vect pos, DebugColor color)
        {
            var center = new Vector2((float)pos.X, (float)pos.Y);

            primitiveBatch.DrawDot(center, (float)size, new Color(color.Red, color.Green, color.Blue));
        }

        public void DrawFatSegment(Vect a, Vect b, double radius, DebugColor outlineColor, DebugColor fillColor)
        {
            var pos1 = new Vector2((float)a.X, (float)a.Y);
            var pos2 = new Vector2((float)b.X, (float)b.Y);

            primitiveBatch.DrawLine(pos1, pos2, new Color(fillColor.Red, fillColor.Green, fillColor.Blue));
        }

        public void DrawPolygon(Vect[] vectors, double radius, DebugColor outlineColor, DebugColor fillColor)
        {
            Vector2[] vertices = vectors.Select(v => new Vector2((float)v.X, (float)v.Y)).ToArray();

            primitiveBatch.DrawPolygon(vertices, new Color(fillColor.Red, fillColor.Green, fillColor.Blue),
                new Color(outlineColor.Red, outlineColor.Green, outlineColor.Blue));
        }

        public void DrawSegment(Vect a, Vect b, DebugColor color)
        {
            var pos1 = new Vector2((float)a.X, (float)a.Y);
            var pos2 = new Vector2((float)b.X, (float)b.Y);

            primitiveBatch.DrawLine(pos1, pos2, new Color(color.Red, color.Green, color.Blue));
        }
    }
}
