using System;
using ChipmunkBinding;

namespace ChipmunkDemo
{
    public class Tumble : DemoBase
    {
        Body _kinematicBoxBody;

        private Random random = new Random();

        void AddBox(Vect pos, double mass, double width, double height)
        {
            var body = new Body(mass, Body.MomentForBox(mass, width, height));
            space.AddBody(body);
            body.Position = pos;

            var shape = new Box(body, width, height, 0.0);

            space.AddShape(shape);

            shape.Elasticity = 0.0;
            shape.Friction = 0.7;
        }

        void AddCircle(Vect pos, double mass, double radius)
        {
            var body = new Body(mass, Circle.MomentForCircle(mass, 0.0, radius, Vect.Zero));
            body.Position = pos;
            space.AddBody(body);

            var shape = new Circle(body, radius, Vect.Zero);
            space.AddShape(shape);

            shape.Elasticity = 0.0;
            shape.Friction = 0.7;
        }

        void AddSegment(Vect pos, double mass, double width, double height)
        {
            var body = new Body(mass, Box.MomentForBox(mass, width, height));
            space.AddBody(body);
            body.Position = pos;

            var shape = new Segment(body, new Vect(0.0, (height - width) / 2.0), new Vect(0.0, (width - height) / 2.0), width / 2.0);
            space.AddShape(shape);

            shape.Elasticity = 0.0;
            shape.Friction = 0.7;
        }

        public override Space LoadContent()
        {
            space = new Space();
            space.Gravity = new Vect(0, -600);

            _kinematicBoxBody = new Body(BodyType.Kinematic);

            space.AddBody(_kinematicBoxBody);

            _kinematicBoxBody.AngularVelocity = -0.4;

            // Set up the static box.
            var a = new Vect(-200, -200);
            var b = new Vect(-200, 200);
            var c = new Vect(200, 200);
            var d = new Vect(200, -200);

            Shape shape = new Segment(_kinematicBoxBody, a, b, 0.0);
            space.AddShape(shape);
            shape.Elasticity = 1.0;
            shape.Friction = 1.0;
            shape.Filter = ChipmunkDemoGame.NotGrabbableFilter;

            shape = new Segment(_kinematicBoxBody, b, c, 0.0);
            space.AddShape(shape);
            shape.Elasticity = 1.0;
            shape.Friction = 1.0;
            shape.Filter = ChipmunkDemoGame.NotGrabbableFilter;

            shape = new Segment(_kinematicBoxBody, c, d, 0.0);
            space.AddShape(shape);
            shape.Elasticity = 1.0;
            shape.Friction = 1.0;
            shape.Filter = ChipmunkDemoGame.NotGrabbableFilter;

            shape = new Segment(_kinematicBoxBody, d, a, 0.0);
            space.AddShape(shape);
            shape.Elasticity = 1.0;
            shape.Friction = 1.0;
            shape.Filter = ChipmunkDemoGame.NotGrabbableFilter;

            double mass = 1;
            double width = 30;
            double height = width * 2;

            // Add the bricks.
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var pos = new Vect(i * width - 150, j * height - 150);

                    int type = random.Next(3000) / 1000;

                    if (type == 0)
                    {
                        AddBox(pos, mass, width, height);
                    }
                    else if (type == 1)
                    {
                        AddSegment(pos, mass, width, height);
                    }
                    else
                    {
                        AddCircle(pos + new Vect(0.0, (height - width) / 2.0), mass, width / 2.0);
                        AddCircle(pos + new Vect(0.0, (width - height) / 2.0), mass, width / 2.0);
                    }
                }
            }

            return space;
        }

        
    }
}
