﻿// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2025 Codefoco LTDA - The above copyright notice and this permission notice shall be
//     included in all copies or substantial portions of the Software.
//
//     Redistribution and use in source and binary forms, with or without
//     modification, are permitted only if explicitly approved by the authors.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
//     EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//     OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//     NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//     HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//     FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//     OTHER DEALINGS IN THE SOFTWARE.

using System;

using System.Collections.Generic;
using System.Linq;

using ChipmunkBinding;

using Microsoft.Xna.Framework;

namespace ChipmunkDemo
{
    public class Slice : DemoBase
    {
        private const double Density = 1.0 / 10000.0;

        private class SliceContext
        {
            public Vect A { get; set; }
            public Vect B { get; set; }
        }

        private bool rightClick;
        private Vect sliceStart;

        private bool pinching = false;
        private Vect sliceEnd;

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

            Vect centroid = Polygon.CentroidForPoly(clipped.ToArray());
            double mass = Polygon.AreaForPoly(clipped.ToArray(), 0.0f) * Density;

            double moment = Polygon.MomentForPolygon(mass, clipped.ToArray(), -centroid, 0.0f);

            var new_body = new Body(mass, moment);
            space.AddBody(new_body);
            new_body.Position = centroid;
            new_body.Velocity = body.GetVelocityAtWorldPoint(centroid);
            new_body.AngularVelocity = body.AngularVelocity;

            var transform = Transform.CreateTranslation(-centroid);
            Shape new_shape = new Polygon(new_body, clipped.ToArray(), transform, 0.0);
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
            space = ChipmunkDemoGame.CreateSpace();
            space.Iterations = 10;
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
            staticBody.Category = ChipmunkDemoGame.NotGrabbableFilter;

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

            SliceObject(sliceStart, ChipmunkDemoGame.ChipmunkDemoMouse);
        }

        private void SliceObject(Vect from, Vect to)
        {
            var context = new SliceContext {
                A = from,
                B = to
            };

            SegmentQueryInfo[] infos = space.SegmentQuery(sliceStart, context.B, 0.0, ChipmunkDemoGame.GrabbableFilter).ToArray();

            if (infos.Length == 0)
                return;

            foreach (SegmentQueryInfo info in infos)
            {
                SliceQuery(info.Shape, info.Point, info.Normal, info.Alpha, context);
            }
        }

        public override void OnPinch(Vect from, Vect to)
        {
            sliceStart = from;
            sliceEnd = to;
            pinching = true;
        }

        public override void OnPinchComplete()
        {
            pinching = false;

            SliceObject(sliceStart, sliceEnd);
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
            if (pinching)
                debugDraw.DrawSegment(sliceStart, sliceEnd, new DebugColor(1, 0, 0, 1));
        }
    }
}
