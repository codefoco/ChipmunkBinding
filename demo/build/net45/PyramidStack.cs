using System;
using System.Collections.Generic;
using ChipmunkBinding;

namespace TestMac
{
    public class PyramidStack
    {
        public PyramidStack()
        {
        }

        Space space;

        public enum ShapeCategorie
        {
            GrabbableMaskBit = 1 << 31,
            NotGrabbableMaskBit = ~(1 << 31)
        }
        readonly ShapeFilter GrabbableFilter = new ShapeFilter(0, (int)ShapeCategorie.GrabbableMaskBit, (int)ShapeCategorie.GrabbableMaskBit);
        readonly ShapeFilter NotGrabbableFilter = new ShapeFilter(0, (int)ShapeCategorie.NotGrabbableMaskBit, (int)ShapeCategorie.NotGrabbableMaskBit);

        List<object> obj = new List<object>();

        public Space Initialize()
        {
            space = new Space();
            space.Iterations = 30;
            space.Gravity = new Vect(0, -100);
            space.SleepTimeThreshold = 0.5f;
            space.CollisionSlop = 0.5f;

            Body body;
            Body staticBody = space.StaticBody;

            Shape shape = new Segment(staticBody, new Vect(-320, -240), new Vect(-320, 240), 0.0f);
            space.AddShape(shape);
            obj.Add(shape);

            shape.Elasticity = 1.0f;
            shape.Friction = 1.0f;
            shape.Filter = NotGrabbableFilter;

            shape = new Segment(staticBody, new Vect(320, -240), new Vect(320, 240), 0.0f);
            space.AddShape(shape);
            obj.Add(shape);

            shape.Elasticity = 1.0f;
            shape.Friction = 1.0f;
            shape.Filter = NotGrabbableFilter;

            shape = new Segment(staticBody, new Vect(-320, -240), new Vect(320, -240), 0.0f);
            space.AddShape(shape);
            obj.Add(shape);

            shape.Elasticity = 1.0f;
            shape.Friction = 1.0f;
            shape.Filter = NotGrabbableFilter;

            // Add lots of boxes.
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    body = new Body(1.0f, Box.MomentForBox(1.0f, 30.0f, 30.0f));
                    space.AddBody(body);
                    obj.Add(body);

                    body.Position = new Vect(j * 32 - i * 16, 300 - i * 32);
                    shape = new Box(body, 30.0f, 30.0f, 0.5f);
                    space.AddShape(shape);
                    obj.Add(shape);

                    shape.Elasticity = 0.0f;
                    shape.Friction = 0.8f;
                }
            }

            // Add a ball to make things more interesting
            double radius = 15.0f;
            body = new Body(10.0f, Circle.MomentForCircle(10.0f, 0.0f, radius, Vect.Zero));
            space.AddBody(body);
            obj.Add(body);

            body.Position = new Vect(0, -240 + radius + 5);

            shape = new Circle(body, radius);
            space.AddShape(shape);
            obj.Add(shape);

            shape.Elasticity = 0.0f;
            shape.Friction = 0.9f;

            return space;
        }

        public void Update(double dt)
        {
            space.Step(dt);
        }

        public void Dispose()
        {
            foreach (Shape s in space.Shapes)
            {
                space.Remove(s);
                s.Dispose();
            }

            foreach(Constraint c in space.Constraints)
            {
                space.Remove(c);
                c.Dispose();
            }

            space.Dispose();
        }

        internal void Draw(IDebugDraw debugDraw)
        {
            space.DebugDraw(debugDraw);
        }
    }
}
