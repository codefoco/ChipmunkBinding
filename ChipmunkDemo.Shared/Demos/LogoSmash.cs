using System;
using System.Linq;
using ChipmunkBinding;
using Microsoft.Xna.Framework;

namespace ChipmunkDemo
{
    public class LogoSmash : DemoBase
    {

        private const int ImageWidth = 188;
        private const int ImageHeight = 35;
        private const int ImageRowLength = 24;

        private Random random = new Random();

        private static readonly sbyte[] _imageBitmap = {
            15,-16,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,-64,15,63,-32,-2,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,31,-64,15,127,-125,-1,-128,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,127,-64,15,127,15,-1,-64,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,-1,-64,15,-2,
            31,-1,-64,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,-1,-64,0,-4,63,-1,-32,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,1,-1,-64,15,-8,127,-1,-32,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
            1,-1,-64,0,-8,-15,-1,-32,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,-31,-1,-64,15,-8,-32,
            -1,-32,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,-15,-1,-64,9,-15,-32,-1,-32,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,31,-15,-1,-64,0,-15,-32,-1,-32,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,63,-7,-1,-64,9,-29,-32,127,-61,-16,63,15,-61,-1,-8,31,-16,15,-8,126,7,-31,
            -8,31,-65,-7,-1,-64,9,-29,-32,0,7,-8,127,-97,-25,-1,-2,63,-8,31,-4,-1,15,-13,
            -4,63,-1,-3,-1,-64,9,-29,-32,0,7,-8,127,-97,-25,-1,-2,63,-8,31,-4,-1,15,-13,
            -2,63,-1,-3,-1,-64,9,-29,-32,0,7,-8,127,-97,-25,-1,-1,63,-4,63,-4,-1,15,-13,
            -2,63,-33,-1,-1,-32,9,-25,-32,0,7,-8,127,-97,-25,-1,-1,63,-4,63,-4,-1,15,-13,
            -1,63,-33,-1,-1,-16,9,-25,-32,0,7,-8,127,-97,-25,-1,-1,63,-4,63,-4,-1,15,-13,
            -1,63,-49,-1,-1,-8,9,-57,-32,0,7,-8,127,-97,-25,-8,-1,63,-2,127,-4,-1,15,-13,
            -1,-65,-49,-1,-1,-4,9,-57,-32,0,7,-8,127,-97,-25,-8,-1,63,-2,127,-4,-1,15,-13,
            -1,-65,-57,-1,-1,-2,9,-57,-32,0,7,-8,127,-97,-25,-8,-1,63,-2,127,-4,-1,15,-13,
            -1,-1,-57,-1,-1,-1,9,-57,-32,0,7,-1,-1,-97,-25,-8,-1,63,-1,-1,-4,-1,15,-13,-1,
            -1,-61,-1,-1,-1,-119,-57,-32,0,7,-1,-1,-97,-25,-8,-1,63,-1,-1,-4,-1,15,-13,-1,
            -1,-61,-1,-1,-1,-55,-49,-32,0,7,-1,-1,-97,-25,-8,-1,63,-1,-1,-4,-1,15,-13,-1,
            -1,-63,-1,-1,-1,-23,-49,-32,127,-57,-1,-1,-97,-25,-1,-1,63,-1,-1,-4,-1,15,-13,
            -1,-1,-63,-1,-1,-1,-16,-49,-32,-1,-25,-1,-1,-97,-25,-1,-1,63,-33,-5,-4,-1,15,
            -13,-1,-1,-64,-1,-9,-1,-7,-49,-32,-1,-25,-8,127,-97,-25,-1,-1,63,-33,-5,-4,-1,
            15,-13,-1,-1,-64,-1,-13,-1,-32,-49,-32,-1,-25,-8,127,-97,-25,-1,-2,63,-49,-13,
            -4,-1,15,-13,-1,-1,-64,127,-7,-1,-119,-17,-15,-1,-25,-8,127,-97,-25,-1,-2,63,
            -49,-13,-4,-1,15,-13,-3,-1,-64,127,-8,-2,15,-17,-1,-1,-25,-8,127,-97,-25,-1,
            -8,63,-49,-13,-4,-1,15,-13,-3,-1,-64,63,-4,120,0,-17,-1,-1,-25,-8,127,-97,-25,
            -8,0,63,-57,-29,-4,-1,15,-13,-4,-1,-64,63,-4,0,15,-17,-1,-1,-25,-8,127,-97,
            -25,-8,0,63,-57,-29,-4,-1,-1,-13,-4,-1,-64,31,-2,0,0,103,-1,-1,-57,-8,127,-97,
            -25,-8,0,63,-57,-29,-4,-1,-1,-13,-4,127,-64,31,-2,0,15,103,-1,-1,-57,-8,127,
            -97,-25,-8,0,63,-61,-61,-4,127,-1,-29,-4,127,-64,15,-8,0,0,55,-1,-1,-121,-8,
            127,-97,-25,-8,0,63,-61,-61,-4,127,-1,-29,-4,63,-64,15,-32,0,0,23,-1,-2,3,-16,
            63,15,-61,-16,0,31,-127,-127,-8,31,-1,-127,-8,31,-128,7,-128,0,0
        };

        static int GetPixel(int x, int y)
        {
            return (_imageBitmap[(x >> 3) + y * ImageRowLength] >> (~x & 0x7)) & 1;
        }

        private readonly static DebugColor dotColor = new DebugColor(0xee / 255.0f, 0xe8 / 255.0f, 0xd5 / 255.0f, 1.0f);

        void DrawDot(Body body, IDebugDraw debugDraw)
        {
            debugDraw.DrawDot(1.75, body.Position, dotColor);
        }

        public override void Draw(GameTime gameTime, IDebugDraw debugDraw)
        {
            int lenght = bodies.Length;
            for (int i = 0; i < lenght; i++)
                DrawDot(bodies[i], debugDraw);
        }

        Body[] bodies;

        private static Shape MakeBall(double x, double y)
        {
            var body = new Body(1.0, double.PositiveInfinity);
            body.Position = new Vect(x, y);

            var shape = new Circle(body, 0.95);
            shape.Elasticity = 0.0;
            shape.Friction = 0.0;

            return shape;
        }

        public override Space LoadContent()
        {
            space = new Space();
            space.Iterations = 1;

            // The space will contain a very large number of similary sized objects.
            // This is the perfect candidate for using the spatial hash.
            // Generally you will never need to do this.
            space.UseSpatialHash(2.0, 10000);

            Body body;
            Shape shape;

            for (int y = 0; y < ImageHeight; y++)
            {
                for (int x = 0; x < ImageWidth; x++)
                {
                    if (GetPixel(x, y) == 0)
                        continue;

                    double x_jitter = 0.05 * random.NextDouble();
                    double y_jitter = 0.05 * random.NextDouble();

                    shape = MakeBall(2 * (x - ImageWidth / 2 + x_jitter), 2 * (ImageHeight / 2 - y + y_jitter));
                    space.AddBody(shape.Body);
                    space.AddShape(shape);
                }
            }

            body = new Body(1e9, double.PositiveInfinity);
            space.AddBody(body);

            body.Position = new Vect(-1000, -10);
            body.Velocity = new Vect(400, 0);

            shape = new Circle(body, 8.0);
            space.AddShape(shape);

            shape.Elasticity = 0.0;
            shape.Friction = 0.0;
            shape.Filter = ChipmunkDemoGame.NotGrabbableFilter;

            bodies = space.Bodies.ToArray();

            return space;
        }


        public LogoSmash()
        {
        }
    }
}
