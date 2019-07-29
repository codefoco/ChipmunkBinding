using ChipmunkBinding;

namespace ChipmunkDemo
{
    public class PyramidStack : DemoBase
    {
        public override Space LoadContent()
        {
            space = ChipmunkDemoGame.CreateSpace();
            space.Iterations = 30;
            space.Gravity = new Vect(0, -100);
            space.SleepTimeThreshold = 0.5f;
            space.CollisionSlop = 0.5f;

            Body staticBody = space.StaticBody;

            Shape shape = new Segment(staticBody, new Vect(-320, -240), new Vect(-320, 240), 0.0f);
            space.AddShape(shape);

            shape.Elasticity = 1.0f;
            shape.Friction = 1.0f;
            shape.Filter = ChipmunkDemoGame.NotGrabbableFilter;

            shape = new Segment(staticBody, new Vect(320, -240), new Vect(320, 240), 0.0f);
            space.AddShape(shape);

            shape.Elasticity = 1.0f;
            shape.Friction = 1.0f;
            shape.Filter = ChipmunkDemoGame.NotGrabbableFilter;

            shape = new Segment(staticBody, new Vect(-320, -240), new Vect(320, -240), 0.0f);
            space.AddShape(shape);

            shape.Elasticity = 1.0f;
            shape.Friction = 1.0f;
            shape.Filter = ChipmunkDemoGame.NotGrabbableFilter;

            // Add lots of boxes.
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    var body = new Body(1.0f, Box.MomentForBox(1.0f, 30.0f, 30.0f));
                    space.AddBody(body);

                    body.Position = new Vect(j * 32 - i * 16, 300 - i * 32);
                    shape = new Box(body, 30.0f, 30.0f, 0.5f);
                    space.AddShape(shape);

                    shape.Elasticity = 0.0f;
                    shape.Friction = 0.8f;
                }
            }


            // Add a ball to make things more interesting
            double radius = 15.0f;
            var circleBody = new Body(10.0f, Circle.MomentForCircle(10.0f, 0.0f, radius, Vect.Zero));
            space.AddBody(circleBody);

            circleBody.Position = new Vect(0, -240 + radius + 5);

            shape = new Circle(circleBody, radius);
            space.AddShape(shape);

            shape.Elasticity = 0.0f;
            shape.Friction = 0.9f;

            return space;
        }
    }
}
