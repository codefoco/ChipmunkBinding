using System;
using ChipmunkBinding;

namespace ChipmunkDemo
{
    public class Tank : DemoBase
    {
        Body tankBody;
        Body tankControlBody;

        private Random random = new Random();

        public override void Update(double dt)
        {
            // turn the control body based on the angle relative to the actual body
            Vect mouseDelta = ChipmunkDemoGame.ChipmunkDemoMouse - tankBody.Position;
            double turn = tankBody.Rotation.Unrotate(mouseDelta).ToAngle();
            tankControlBody.Angle = tankBody.Angle - turn;

            // drive the tank towards the mouse
            if (ChipmunkDemoGame.ChipmunkDemoMouse.Near(tankBody.Position, 30.0))
            {
                tankControlBody.Velocity = Vect.Zero;
            }
            else
            {
                double direction = mouseDelta.Dot(tankBody.Rotation) > 0.0 ? 1.0 : -1.0;
                tankControlBody.Velocity = tankBody.Rotation.Rotate(new Vect(30.0 * direction, 0.0f));
            }
            base.Update(dt);
        }

        Body AddBox(double size, double mass)
        {
            double radius = (new Vect(size, size)).Length();

            var body = new Body(mass, Box.MomentForBox(mass, size, size));
            space.AddBody(body);

            body.Position = new Vect(random.NextDouble() * (640 - 2 * radius) - (320 - radius),
                                     random.NextDouble() * (480 - 2 * radius) - (240 - radius));

            Shape shape = new Box(body, size, size, 0.0);
            space.AddShape(shape);
            shape.Elasticity = 0.0;
            shape.Friction = 0.7;

            return body;
        }

        public override Space LoadContent()
        {
            space = ChipmunkDemoGame.CreateSpace();
            space.Iterations = 10;
            space.SleepTimeThreshold = 0.5;
            space.CollisionSlop = 0.5;

            Body staticBody = space.StaticBody;
            Shape shape = new Segment(staticBody, new Vect(-320, -240), new Vect(-320, 240), 0.0);

            // Create segments around the edge of the screen.
            space.AddShape(shape);

            shape.Elasticity = 1.0;
            shape.Friction = 1.0;
            staticBody.CollisionMask = ChipmunkDemoGame.NotGrabbableFilter;

            shape = new Segment(staticBody, new Vect(320, -240), new Vect(320, 240), 0.0);
            space.AddShape(shape);
            shape.Elasticity = 1.0;
            shape.Friction = 1.0;

            shape = new Segment(staticBody, new Vect(-320, -240), new Vect(320, -240), 0.0);
            space.AddShape(shape);
            shape.Elasticity = 1.0;
            shape.Friction = 1.0;

            shape = new Segment(staticBody, new Vect(-320, 240), new Vect(320, 240), 0.0);
            space.AddShape(shape);
            shape.Elasticity = 1.0;
            shape.Friction = 1.0;


            for (int i = 0; i < 50; i++)
            {
                Body body = AddBox(20, 1);

                Constraint p = new PivotJoint(staticBody, body, Vect.Zero, Vect.Zero);
                space.AddConstraint(p);
                p.MaxBias = 0; // disable joint correction
                p.MaxForce = 1000.0; // emulate linear friction

                Constraint g = new GearJoint(staticBody, body, 0.0, 1.0);
                space.AddConstraint(g);
                g.MaxBias = 0; // disable joint correction
                g.MaxForce = 5000.0; // emulate angular friction
            }

            // We joint the tank to the control body and control the tank indirectly by modifying the control body.
            tankControlBody = new Body(BodyType.Kinematic);
            space.AddBody(tankControlBody);
            tankBody = AddBox(30, 10);

            Constraint pivot = new PivotJoint(tankControlBody, tankBody, Vect.Zero, Vect.Zero);
            space.AddConstraint(pivot);
            pivot.MaxBias = 0; // disable joint correction
            pivot.MaxForce = 10000.0; // emulate linear friction

            Constraint gear = new GearJoint(tankControlBody, tankBody, 0.0, 1.0);
            space.AddConstraint(gear);
            gear.ErrorBias = 0; // attempt to fully correct the joint each step
            gear.MaxBias = 1.2;  // but limit it's angular correction rate
            gear.MaxForce = 50000.0; // emulate angular friction

            return space;
        }
    }
}
