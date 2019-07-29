using System;
using ChipmunkBinding;

namespace ChipmunkDemo
{
    public class PyramidTopple : DemoBase
    {
        const double Width = 4.0;
        const double Height = 30.0;

        void AddDomino(Vect pos, bool flipped)
        {
            double mass = 1.0;
            double radius = 0.5;
            double moment = Box.MomentForBox(mass, Width, Height);

            var body = new Body(mass, moment);
            space.AddBody(body);
            body.Position = pos;

            var shape = flipped ? new Box(body, Height, Width, 0.0) : new Box(body, Width - radius * 2.0, Height, radius);
            space.AddShape(shape);

            shape.Elasticity = 0.0;
            shape.Friction =  0.6;
        }

        public override Space LoadContent()
        {
            space = new Space();
            space.Iterations = 30;
            space.Gravity = new Vect(0, -300);
            space.SleepTimeThreshold = 0.5f;
            space.CollisionSlop = 0.5;

            // Add a floor.
            Shape shape = new Segment(space.StaticBody, new Vect(-600, -240), new Vect(600, -240), 0.0);
            space.AddShape(shape);

            shape.Elasticity = 1.0;
            shape.Friction = 1.0;
            shape.Filter = ChipmunkDemoGame.NotGrabbableFilter;


            // Add the dominoes.
            int n = 12;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < (n - i); j++)
                {
                    var offset = new Vect((j - (n - 1 - i) * 0.5) * 1.5 * Height, (i + 0.5) * (Height + 2 * Width) - Width - 240);
                    AddDomino(offset, false);
                    Vect pos = offset + new Vect(0, (Height + Width) / 2.0);

                    AddDomino(pos, true);

                    if (j == 0)
                    {
                        pos = offset + new Vect(0.5 * (Width - Height), Height + Width);
                        AddDomino(pos, false);
                    }

                    if (j != n - i - 1)
                    {
                        AddDomino(offset + new Vect(Height * 0.75f, (Height + 3 * Width) / 2.0f), true);
                    }
                    else
                    {
                        AddDomino(offset + new Vect(0.5 * (Height - Width), Height + Width), false);
                    }
                }
            }

            return space;
        }
    }
}
