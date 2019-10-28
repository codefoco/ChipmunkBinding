using ChipmunkBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipmunkDemo
{
    public class Plink : DemoBase
    {
        private const int NumVertices = 5;

        double pentagonMass;
        double pentagonMoment;

        private Random random = new Random();

        // Vertexes for a triangle shape.
        private readonly Vect[] _tris = {
                new Vect(-15,-15),
                new Vect(  0, 10),
                new Vect( 15,-15),
        };

        public override Space LoadContent()
        {
            space = ChipmunkDemoGame.CreateSpace();
            space.Iterations = 5;
            space.Gravity = new Vect(0, -100);

            Body body;
            Body staticBody = space.StaticBody;
            Shape shape;

            // Create the static triangles.
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    double stagger = (j % 2) * 40;
                    var offset = new Vect(i * 80 - 320 + stagger, j * 70 - 240);
                    shape = new Polygon(staticBody, _tris, Transform.CreateTranslation(offset), 0.0);
                    space.AddShape(shape);
                    shape.Elasticity =  1.0f;
                    shape.Friction = 1.0f;
                    shape.Filter = ChipmunkDemoGame.NotGrabbableFilter;
                }
            }

            // Create vertexes for a pentagon shape.
            Vect [] verts = new Vect[NumVertices];

            for (int i = 0; i < NumVertices; i++)
            {
                double angle = -2.0 * Math.PI * i / NumVertices;
                verts[i] = new Vect(10 * Math.Cos(angle), 10 * Math.Sin(angle));
            }

            pentagonMass = 1.0;
            pentagonMoment = Polygon.MomentForPolygon(1.0, verts, Vect.Zero, 0.0f);

            // Add lots of pentagons.
            for (int i = 0; i < 300; i++)
            {
                body = new Body(pentagonMass, pentagonMoment);
                space.AddBody(body);

                double x = random.NextDouble() * 640 - 320;
                body.Position = new Vect(x, 350);

                shape = new Polygon(body, verts, Transform.Identity, 0.0);
                space.AddShape(shape);

                shape.Elasticity = 0.0;
                shape.Friction = 0.4;
            }

            return space;
        }

        public override void Update(double dt)
        {
            MoveBodysBack();

            base.Update(dt);
        }

        void MoveBodysBack()
        {
            IReadOnlyList<Body> bodies = space.Bodies;

            var outsideBodies = bodies.Where(b => b.Position.Y < -260);

            foreach (Body body in outsideBodies)
            {
                double x = random.NextDouble() * 640 - 320;
                body.Position = new Vect(x, 260);
            }
        }

        public override void OnMouseRightButtonDown(Vect chipmunkDemoMouse)
        {
            PointQueryInfo info = space.PointQueryNearest(chipmunkDemoMouse, 0.0, ChipmunkDemoGame.GrabbableFilter);
            if (info == null || info.Shape == null)
                return;

            Body body = info.Shape.Body;
            if (body.Type == BodyType.Static)
            {
                body.Type = BodyType.Dynamic;
                body.Mass = pentagonMass;
                body.Moment = pentagonMoment;
            }
            else
            {
                body.Type = BodyType.Static;
            }
        }
    }
}
