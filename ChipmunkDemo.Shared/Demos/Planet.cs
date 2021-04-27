using System;
using ChipmunkBinding;

namespace ChipmunkDemo
{
    public class Planet : DemoBase
    {
        Body planetBody;
        const double gravityStrength = 5.0e6;

        private Random random = new Random();

        private static void PlanetGravityVelocityFunction(Body body, Vect gravity, double damping, double dt)
        {
            // Gravitational acceleration is proportional to the inverse square of
            // distance, and directed toward the origin. The central planet is assumed
            // to be massive enough that it affects the satellites but not vice versa.
            Vect p = body.Position;
            double sqdist = p.LengthSquared();
            Vect g = p *  (-gravityStrength / (sqdist * Math.Sqrt(sqdist)));

            body.UpdateVelocity(g, damping, dt);
        }

        static Action<Body,Vect,double,double> planetGravityFunctionCallback = PlanetGravityVelocityFunction;

        Vect RandPosition(double radius)
        {
            Vect v;
            do
            {
                v = new Vect(random.NextDouble() * (640 - 2 * radius) - (320 - radius), random.NextDouble() * (480 - 2 * radius) - (240 - radius));

            } while (v.Length() < 85.0f);

            return v;
        }

        void AddBox(Space space)
        {
            const double size = 10.0;
            const double mass = 1.0;

            Vect[] verts = {
                new Vect(-size,-size),
                new Vect(-size, size),
                new Vect( size, size),
                new Vect( size,-size)
            };

            double radius = verts[0].Length();
            Vect position = RandPosition(radius);

            var body = new Body(mass, Polygon.MomentForPolygon(mass, verts, Vect.Zero, 0.0));
            space.AddBody(body);

            body.VelocityUpdateFunction = planetGravityFunctionCallback;

            body.Position = position;

            // Set the box's velocity to put it into a circular orbit from its
            // starting position.

            double r = position.Length();
            double v = Math.Sqrt(gravityStrength / r) / r;
            body.Velocity = position.Perpendicurlar * v;

            // Set the box's angular velocity to match its orbital period and
            // align its initial angle with its position.
            body.AngularVelocity = v;
            body.Angle = Math.Atan2(position.Y, position.X);


            var shape = new Polygon(body, verts, Transform.Identity, 0.0);
            space.AddShape(shape);

            shape.Elasticity = 0.0;
            shape.Friction =  0.7;
        }


        public override Space LoadContent()
        {
            space = ChipmunkDemoGame.CreateSpace();
            space.Iterations = 10;

            planetBody = new Body(BodyType.Kinematic);

            space.AddBody(planetBody);
            planetBody.AngularVelocity = 0.2;

            for (int i = 0; i < 30; i++)
            {
                AddBox(space);
            }

            planetBody.CollisionMask = ChipmunkDemoGame.NotGrabbableFilter;

            Shape shape = new Circle(planetBody, 70.0, Vect.Zero);
            space.AddShape(shape);
            shape.Elasticity = 1.0;
            shape.Friction = 1.0;

            return space;
        }
    }
}
