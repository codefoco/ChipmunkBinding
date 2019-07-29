using System;
using System.Collections.Generic;
using System.Linq;
using ChipmunkBinding;
using Microsoft.Xna.Framework;

namespace ChipmunkDemo
{
    public class Slice : DemoBase
    {
        const double Density = 1.0 / 10000.0;

        class SliceContext
        {
            public Vect A { get; set; }
            public Vect B { get; set; }
        }

        bool rightClick;
        Vect sliceStart;

        private void ClipPoly(Polygon shape, Vect n, double distance)
        {
            Body body = shape.Body;

            int count = shape.Count;

            var clipped = new List<Vect>(count);

            for (int i = 0, j = count - 1; i < count; j = i, i++)
            {
                Vect a = body.LocalToWorld(shape.GetVertex(j));
                double a_dist = a.Dot(n) - distance;

                if (a_dist < 0.0)
                    clipped.Add(a);

                Vect b = body.LocalToWorld(shape.GetVertex(i));
                double b_dist = b.Dot(n) - distance;

                if (a_dist * b_dist < 0.0)
                {
                    double t = Math.Abs(a_dist) / (Math.Abs(a_dist) + Math.Abs(b_dist));

                    clipped.Add(a.Lerp(b, t));
                }
            }

            Vect centroid = Polygon.CentroidForPoly(clipped);
            double mass = Polygon.AreaForPoly(clipped, 0.0f) * Density;

            double moment = Polygon.MomentForPolygon(mass, clipped, -centroid, 0.0f);

            var new_body = new Body(mass, moment);
            space.AddBody(new_body);
            new_body.Position = centroid;
            new_body.Velocity =  body.GetVelocityAtWorldPoint(centroid);
            new_body.AngularVelocity = body.AngularVelocity;

            var transform = Transform.CreateTranslation(-centroid);
            Shape new_shape = new Polygon(new_body, clipped, transform, 0.0);
            space.AddShape(new_shape);
            new_shape.Friction = shape.Friction;
        }

        private void SliceShapePostStep(Space space, object s, object c)
        {
            var context = (SliceContext)c;
            var shape = (Polygon)s;

            Vect a = context.A;
            Vect b = context.B;

            // Clipping plane normal and distance.
            Vect diff = b - a;
            Vect n = diff.Perpendicurlar.Normalize();

            double dist = a.Dot(n);

            ClipPoly(shape, n, dist);
            ClipPoly(shape, -n, -dist);

            Body body = shape.Body;
            space.RemoveShape(shape);
            space.RemoveBody(body);
            shape.Free();
            body.Free();
        }

        private void SliceQuery(Shape shape, Vect point, Vect normal, double alpha, SliceContext context)
        {
            Vect a = context.A;
            Vect b = context.B;

            // Check that the slice was complete by checking that the endpoints aren't in the sliced shape.
            PointQueryInfo infoA = shape.PointQuery(a);
            if (infoA == null || infoA.Distance <= 0.0)
                return;

            PointQueryInfo infoB = shape.PointQuery(b);
            if (infoB == null || infoB.Distance <= 0.0)
                return;

            // Can't modify the space during a query.
            // Must make a post-step callback to do the actual slicing.
            space.AddPostStepCallback(SliceShapePostStep, shape, context);
        }

        public override Space LoadContent()
        {
            space = new Space();
            space.Iterations = 30;
            space.Gravity = new Vect(0, -500);
            space.SleepTimeThreshold = 0.5;
            space.CollisionSlop = 0.5;

            Body body;
            Body staticBody = space.StaticBody;

            Shape shape;

            shape = new Segment(staticBody, new Vect(-1000, -230), new Vect(1000, -230), 0.0);
            space.AddShape(shape);

            shape.Elasticity = 1.0;
            shape.Friction = 1.0;
            shape.Filter = ChipmunkDemoGame.NotGrabbableFilter;

            double width = 200;
            double height = 300;
            double mass = width * height * Density;
            double moment = Box.MomentForBox(mass, width, height);

            body = new Body(mass, moment);
            space.AddBody(body);

            Vect[] vects = {
                new Vect(width/2, height/2),
                new Vect(-width/2, height/2),
                new Vect(-width/2, -height/2),
                new Vect(width/2, -height/2),
            };

            shape = new Polygon(body, vects, 0.0);
            space.AddShape(shape);
            shape.Friction = 0.6;

            return space;
        }

        public override void OnMouseRightButtonDown(Vect chipmunkDemoMouse)
        {
            rightClick = true;
            sliceStart = ChipmunkDemoGame.ChipmunkDemoMouse;
        }

        public override void OnMouseRightButtonUp(Vect chipmunkDemoMouse)
        {
            rightClick = false;
            var context = new SliceContext {
                A = sliceStart,
                B = ChipmunkDemoGame.ChipmunkDemoMouse
            };

            SegmentQueryInfo[] infos = space.SegmentQuery(sliceStart, context.B, 0.0, ChipmunkDemoGame.GrabbableFilter).ToArray();

            if (infos.Length == 0)
                return;

            foreach (SegmentQueryInfo info in infos)
            {
                SliceQuery(info.Shape, info.Point, info.Normal, info.Alpha, context);
            }
        }

        public override void Update(double dt)
        {
            base.Update(dt);
        }

        public override void Draw(GameTime gameTime, IDebugDraw debugDraw)
        {
            base.Draw(gameTime, debugDraw);

            if (rightClick)
                debugDraw.DrawSegment(sliceStart, ChipmunkDemoGame.ChipmunkDemoMouse, new DebugColor(1, 0, 0, 1));
        }
    }
}
