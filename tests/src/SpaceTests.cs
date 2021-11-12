
using System.Linq;
using System.Text;

using ChipmunkBinding;

using NUnit.Framework;

using cpBody = System.IntPtr;



namespace ChipmunkBindingTest.Tests
{
    [TestFixture]
    public class SpaceTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void ManagedToNativeRoundtrip()
        {
            var space = new Space();

            cpBody handle = space.Handle;

            var spaceFromHandle = Space.FromHandle(handle);

            Assert.AreSame(space, spaceFromHandle, "#1");

            space.Dispose();
        }

        [Test]
        public void CreateSpace()
        {
            var space = new Space();

            Assert.IsNotNull(space.Handle, "#1");
            space.Dispose();
        }

        [Test]
        public void Iterarations()
        {
            var space = new Space
            {
                Iterations = 10
            };

            Assert.AreEqual(10, space.Iterations, "#1");

            space.Iterations = 15;

            Assert.AreEqual(15, space.Iterations, "#2");

            space.Dispose();
        }

        [Test]
        public void Gravity()
        {
            var space = new Space();
            var g = new Vect(0, -10);

            space.Gravity = g;

            Assert.AreEqual(g, space.Gravity, "#1");
            space.Dispose();
        }

        [Test]
        public void Damping()
        {
            var space = new Space
            {
                Damping = 0.10
            };

            Assert.AreEqual(0.10, space.Damping, "#1");
            space.Dispose();
        }

        [Test]
        public void IdleSpeedThreshold()
        {
            var space = new Space
            {
                IdleSpeedThreshold = 0.10
            };

            Assert.AreEqual(0.10, space.IdleSpeedThreshold, "#1");
            space.Dispose();
        }

        [Test]
        public void StaticBody()
        {
            var space = new Space();
            Body body = space.StaticBody;
            Body body2 = space.StaticBody;

            Assert.AreSame(body, body2, "#1");
            space.Dispose();
        }

        [Test]
        public void ContainsRemove()
        {
            var space = new Space();
            var body = new Body(1, 1.66);


            Assert.IsFalse(space.Contains(body), "#1");

            space.AddBody(body);

            Assert.IsTrue(space.Contains(body), "#2");

            space.RemoveBody(body);

            Assert.IsFalse(space.Contains(body), "#3");

            body.Dispose();
            space.Dispose();
        }

        [Test]
        public void AddPostStepCallback()
        {
            var space = new Space();
            string foo = string.Empty;

            _ = space.AddPostStepCallback((s, k, d) => foo = k + " " + d, "key", "data");

            space.Step(0.1);

            Assert.AreEqual("key data", foo, "#1");
            space.Dispose();
        }

        [Test]
        public void PointQueryTest()
        {
            var space = new Space();
            var body = new Body(1, 1.66);
            var shape = new Box(body, 100, 100, 0);

            body.Position = new Vect(0, 0);

            PointQueryInfo[] infos = space.PointQuery(body.Position, 10.0, -1).ToArray();

            Assert.AreEqual(0, infos.Length, "#1");

            space.AddBody(body);
            space.AddShape(shape);

            infos = space.PointQuery(body.Position, 10.0, -1).ToArray();

            Assert.AreEqual(1, infos.Length, "#2");
            Assert.AreSame(shape, infos[0].Shape, "#3");

            shape.Dispose();
            body.Dispose();
            space.Dispose();
        }

        [Test]
        public void PointQueryTestCategories()
        {
            var space = new Space();
            var body1 = new Body(1, 1.66);
            var shape1 = new Box(body1, 100, 100, 0);

            body1.Category = 1;
            body1.ContactMask = 0;
            body1.CollisionMask = 0;

            var body2 = new Body(1, 1.66);
            var shape2 = new Box(body2, 100, 100, 0);

            body2.Category = 2;
            body2.ContactMask = 0;
            body2.CollisionMask = 0;

            var body3 = new Body(1, 1.66);
            var shape3 = new Box(body3, 100, 100, 0);

            body3.Category = 4;
            body3.ContactMask = 0;
            body3.CollisionMask = 0;

            body1.Position = new Vect(0, 0);
            body2.Position = new Vect(0, 0);

            space.AddBody(body1);
            space.AddShape(shape1);

            space.AddBody(body2);
            space.AddShape(shape2);

            space.AddBody(body3);
            space.AddShape(shape3);

            PointQueryInfo[] infos = space.PointQuery(body1.Position, 0.0, 1).ToArray();

            Assert.AreEqual(1, infos.Length, "#1");
            Assert.AreSame(shape1, infos[0].Shape, "#2");

            infos = space.PointQuery(body1.Position, 0.0, 2).ToArray();

            Assert.AreEqual(1, infos.Length, "#3");
            Assert.AreSame(shape2, infos[0].Shape, "#4");

            infos = space.PointQuery(body1.Position, 0.0, 2 | 1).ToArray();

            Assert.AreEqual(2, infos.Length, "#3");
            Assert.AreSame(shape2, infos[0].Shape, "#4.2");
            Assert.AreSame(shape1, infos[1].Shape, "#4.1");

            infos = space.PointQuery(body1.Position, 0.0, -1).ToArray();

            Assert.AreEqual(3, infos.Length, "#5");
            Assert.AreSame(shape3, infos[0].Shape, "#6.1");
            Assert.AreSame(shape2, infos[1].Shape, "#6.2");
            Assert.AreSame(shape1, infos[2].Shape, "#6.3");

            shape1.Dispose();
            body1.Dispose();

            shape2.Dispose();
            body2.Dispose();

            shape3.Dispose();
            body3.Dispose();

            space.Dispose();
        }

        [Test]
        public void SegmentQueryTest()
        {
            var space = new Space();
            var body = new Body(1, 1.66);
            var shape = new Box(body, 100, 100, 0);

            body.Position = new Vect(0, 0);
            var end = new Vect(0, 1);

            SegmentQueryInfo[] infos = space.SegmentQuery(body.Position, end, 2.0, -1).ToArray();

            Assert.AreEqual(0, infos.Length, "#1");

            space.AddBody(body);
            space.AddShape(shape);

            infos = space.SegmentQuery(body.Position, end, 2.0, -1).ToArray();

            SegmentQueryInfo first = space.SegmentQueryFirst(body.Position, end, 2.0, -1);


            Assert.AreEqual(1, infos.Length, "#2");
            Assert.AreSame(shape, infos[0].Shape, "#3");

            Assert.AreEqual(infos[0], first, "#4");

            shape.Dispose();
            body.Dispose();
            space.Dispose();
        }

        [Test]
        [Ignore("TODO: ShapeQuery test better")] // TODO: ShapeQuery test better
        public void ShapeQueryTest()
        {
            var space = new Space();
            var body = new Body(1, 1.66);
            var shape = new Box(body, 1, 1, 0);

            body.Position = new Vect(0, 0);

            ContactPointSet[] pointSet = space.ShapeQuery(shape).ToArray();

            Assert.AreEqual(0, pointSet.Length, "#1");

            space.AddBody(body);
            space.AddShape(shape);

            pointSet = space.ShapeQuery(shape).ToArray();

            Assert.AreEqual(1, pointSet.Length, "#2");

            shape.Dispose();
            body.Dispose();
            space.Dispose();
        }

        [Test]
        public void BoundBoxQueryTest()
        {
            var space = new Space();
            var body = new Body(1, 1.66);
            var shape = new Box(body, 5, 5, 0);

            var pos = new Vect(3, 3);

            body.Position = pos;

            var bb = new BoundingBox
            {
                Left = -20,
                Top = 20,
                Right = +20,
                Bottom = -20
            };

            Shape[] shapes = space.BoundBoxQuery(bb, -1).ToArray();

            Assert.AreEqual(0, shapes.Length, "#1");

            space.AddBody(body);
            space.AddShape(shape);

            shapes = space.BoundBoxQuery(bb, -1).ToArray();

            Assert.AreEqual(1, shapes.Length, "#2");
            Assert.AreSame(shape, shapes[0], "#3");

            shape.Dispose();
            body.Dispose();
            space.Dispose();
        }

        [Test]
        public void DebugDrawTest()
        {
            var space = new Space();
            var body = new Body(1, 1.66);
            var shape = new Box(body, 100, 100, 0);

            string expected_calls = "DrawPolygon\nvectors[0] = (50,-50)\nvectors[1] = (50,50)\nvectors[2] = (-50,50)\nvectors[3] = (-50,-50)\nradius = 0\noutlineColor = (1,1,1,1)\nfillColor = (0,0,1,1)\n";

            body.Position = new Vect(0, 0);

            space.AddBody(body);
            space.AddShape(shape);

            var debugDraw = new FakeDebugDraw();

            space.DebugDraw(debugDraw);

            Assert.AreEqual(expected_calls, debugDraw.TracedCalls, "#1");

            shape.Dispose();
            body.Dispose();
            space.Dispose();
        }

        [Test]
        public void BodiesProperty()
        {
            var space = new Space();
            var body = new Body(1, 1.66);
            var body2 = new Body(1, 1.66);
            var body3 = new Body(BodyType.Static);

            body.Position = new Vect(10, 10);
            body2.Position = new Vect(20, 20);


            Assert.AreEqual(0, space.Bodies.Count, "#1");

            space.AddBody(body);

            Body[] bodies = space.Bodies.ToArray();
            Assert.AreEqual(1, bodies.Length, "#2.1");
            Assert.AreSame(body, bodies[0], "#2.2");

            Body[] contactedBodies = body.AllContactedBodies.ToArray();

            Assert.AreEqual(0, contactedBodies.Length, "#contactedBodies.Length");

            space.AddBody(body2);

            bodies = space.Bodies.ToArray();

            Assert.AreEqual(2, bodies.Length, "#3.1");
            Assert.AreSame(body, bodies[0], "#3.2");
            Assert.AreSame(body2, bodies[1], "#3.3");

            space.AddBody(body3);

            space.Step(0.1);

            bodies = space.Bodies.ToArray();

            Assert.AreEqual(3, bodies.Length, "#4.1");
            Assert.AreSame(body, bodies[0], "#4.2");
            Assert.AreSame(body2, bodies[1], "#4.3");
            Assert.AreSame(body3, bodies[2], "#4.4");

            Body[] dynamicBodies = space.DynamicBodies.ToArray();

            Assert.AreEqual(2, dynamicBodies.Length, "#5.1");
            Assert.AreSame(body, dynamicBodies[0], "#5.2");
            Assert.AreSame(body2, dynamicBodies[1], "#5.3");

            body.Dispose();
            space.Dispose();
        }

        [Test]
        public void ShapesProperty()
        {
            var space = new Space();
            var body = new Body(1, 1.66);
            var body2 = new Body(1, 1.66);

            var shape = new Box(body, 100, 100, 0);
            var shape2 = new Box(body2, 100, 100, 0);


            body.Position = new Vect(10, 10);
            body2.Position = new Vect(20, 20);

            Assert.AreEqual(0, space.Shapes.Count, "#1");

            space.AddBody(body);
            space.AddShape(shape);

            Shape[] shapes = space.Shapes.ToArray();
            Assert.AreEqual(1, shapes.Length, "#2.1");
            Assert.AreSame(shape, shapes[0], "#2.2");

            space.AddBody(body2);
            space.AddShape(shape2);

            shapes = space.Shapes.ToArray();

            Assert.AreEqual(2, shapes.Length, "#3.1");
            Assert.AreSame(shape, shapes[0], "#3.2");
            Assert.AreSame(shape2, shapes[1], "#3.3");


            body.Dispose();
            space.Dispose();
        }

        [Test]
        public void TestCollisionHandler()
        {
            var space = new Space
            {
                CollisionBias = 1.0,
            };

            float radius = 5.0f;

            var body1 = new Body(1, 1)
            {
                Position = new Vect(0, 0),
                ContactMask = 1
            };

            space.AddBody(body1);
            space.AddShape(new Circle(body1, radius));

            var body2 = new Body(1, 1)
            {
                Position = new Vect(0, 0),
                ContactMask = 1
            };

            space.AddBody(body2);

            var shape2 = new Circle(body2, radius);
            space.AddShape(shape2);

            Body[] contactedBodies1 = body1.AllContactedBodies.ToArray();
            Body[] contactedBodies2 = body2.AllContactedBodies.ToArray();

            Assert.AreEqual(0, contactedBodies1.Length, "#contactedBodies1.Length");
            Assert.AreEqual(0, contactedBodies2.Length, "#contactedBodies2.Length");

            Assert.IsFalse(body1.ContactWith(body2), "#body1.ContactWith(body2).1.false");
            Assert.IsFalse(body2.ContactWith(body1), "#body2.ContactWith(body1).1.false");

            CollisionHandler handler = space.GetOrCreateCollisionHandler(0, 0);

            CollisionHandler handler2 = space.GetOrCreateCollisionHandler(0, 0);

            Assert.AreSame(handler, handler2, "#0");

            handler.Data = new StringBuilder();

            handler.Begin = (a, s, data) =>
            {
                StringBuilder builder = (StringBuilder)data;
                _ = builder.Append("Begin-");
            };

            handler.PreSolve = (a, s, data) =>
            {
                StringBuilder builder = (StringBuilder)data;
                _ = builder.Append("PreSolve-");
                return true;
            };

            handler.PostSolve = (a, s, data) =>
            {
                StringBuilder builder = (StringBuilder)data;
                _ = builder.Append("PostSolve-");
            };

            handler.Separate = (a, s, data) =>
            {
                StringBuilder builder = (StringBuilder)data;
                _ = builder.Append("Separete-");
            };

            space.Step(0.1);

            Body[] contactedBodies = body1.AllContactedBodies.ToArray();
            Assert.AreEqual(1, contactedBodies.Length, "#contactedBodies.Length");
            Assert.AreSame(contactedBodies[0], body2, "#contactedBodies.body2");

            contactedBodies = body2.AllContactedBodies.ToArray();
            Assert.AreEqual(1, contactedBodies.Length, "#contactedBodies.Length2");
            Assert.AreSame(contactedBodies[0], body1, "#contactedBodies.body1");

            Assert.IsTrue(body1.ContactWith(body2), "#body1.ContactWith(body2).1.true");
            Assert.IsTrue(body2.ContactWith(body1), "#body2.ContactWith(body1).1.true");

            // We don't receive Begin, because we change to make PostSolve/Begin mutually exclusive
            Assert.AreEqual("PreSolve-PostSolve-", handler.Data.ToString(), "#1");

            space.Step(0.1);

            // PreSolve is only triggered on the first contact to avoid native round trips
            Assert.AreEqual("PreSolve-PostSolve-PreSolve-", handler.Data.ToString(), "#2");

            handler.PostSolve = null;

            space.Step(0.1);

            CollisionHandler handler3 = space.GetOrCreateCollisionHandler(-1, -2);

            Assert.AreEqual(-1, handler3.TypeA, "#TypeA");
            Assert.AreEqual(-2, handler3.TypeB, "#TypeB");

            shape2.Dispose();
            body1.Dispose();
            body2.Dispose();
            space.Dispose();
        }

        [Test]
        public void TestCrashLinux()
        {
            var space = new Space();


            var body1 = new Body(1, 1)
            {
                Position = new Vect(0, 0)
            };

            space.AddBody(body1);
            space.AddShape(new Circle(body1, 5));

            var body2 = new Body(1, 1)
            {
                Position = new Vect(0, 0)
            };

            space.AddBody(body2);

            space.AddShape(new Circle(body2, 5));

            space.Step(0.1);
            space.Step(0.1);

            body1.Dispose();
            body2.Dispose();
            space.Dispose();
            Assert.Pass("#1");
        }

        [Test]
        public void TestArbiterProperties()
        {
            Space space;
            using (space = new Space())
            {
                space.CollisionBias = 1.0;

                float radius = 5.0f;

                var body1 = new Body(1, 1.666)
                {
                    Position = new Vect(0, 0),
                    ContactMask = 1
                };

                space.AddBody(body1);

                Shape shape = new Circle(body1, radius);
                space.AddShape(shape);

                var body2 = new Body(1, 1.666)
                {
                    Position = new Vect(0, 0),
                    ContactMask = 1
                };

                space.AddBody(body2);

                var shape2 = new Circle(body2, radius);
                space.AddShape(shape2);


                CollisionHandler handler = space.GetOrCreateCollisionHandler(0, 0);

                handler.Data = "object data";

                Body bodyA = null;
                Body bodyB = null;
                Shape shapeA = null;
                Shape shapeB = null;
                double restituition = -1;
                double fricction = -1;
                Vect surfaceVelocity = new Vect(-1, -1);
                Vect totalImpulse = new Vect(-1, -1);
                double ke = -1;
                ContactPointSet pointSet = null;
                bool first = false;
                bool removal = true;
                int count = -1;
                Vect normal = new Vect(-1, -1);
                Vect pointA = new Vect(-1, -1);
                Vect pointB = new Vect(-1, -1);
                double depth = -1;

                var arb = default(Arbiter);

                handler.PostSolve = (arbiter, s, obj) =>
                {
                    Assert.IsNull(arbiter.Data, "arbiter.Data");

                    arbiter.Data = "another data";

                    arbiter.GetBodies(out bodyA, out bodyB);
                    arbiter.GetShapes(out shapeA, out shapeB);
                    restituition = arbiter.Restitution;
                    fricction = arbiter.Friction;
                    surfaceVelocity = arbiter.SurfaceVelocity;
                    totalImpulse = arbiter.TotalImpulse;
                    ke = arbiter.TotalKE;
                    pointSet = arbiter.ContactPointSet;
                    first = arbiter.IsFirstContact;
                    removal = arbiter.IsRemoval;
                    count = arbiter.Count;
                    normal = arbiter.Normal;
                    pointA = arbiter.GetPointA(0);
                    pointB = arbiter.GetPointB(0);
                    depth = arbiter.GetDepth(0);
                    arb = arbiter;
                };

                space.Step(0.1);

                Assert.AreSame(body2, bodyA, "#1");
                Assert.AreSame(body1, bodyB, "#1.1");

                System.Collections.Generic.IReadOnlyList<Arbiter> arbiters = bodyA.Arbiters;

                Assert.AreEqual(1, arbiters.Count, "#0");
                Assert.AreEqual(arb, arbiters[0], "#0.1");
                Assert.AreEqual("another data", arbiters[0].Data, "#0.2");

                Assert.AreSame(shape2, shapeA, "#2");
                Assert.AreSame(shape, shapeB, "#2.1");

                Assert.AreEqual(0, restituition, 0.00001, "#3");
                Assert.AreEqual(0, fricction, 0.00001, "#4");
                Assert.AreEqual(Vect.Zero, surfaceVelocity, "#5");
                Assert.AreEqual(Vect.Zero, totalImpulse, "#6");
                Assert.AreEqual(0, ke, "#7");

                Assert.AreEqual(1, pointSet.Count, "8.1");
                Assert.AreEqual(new Vect(1, 0), pointSet.Normal, "8.2");
                Assert.AreEqual(-10, pointSet.Points[0].Distance, 0.00001, "8.3");
                Assert.AreEqual(new Vect(5, 0), pointSet.Points[0].PointA, "8.4");
                Assert.AreEqual(new Vect(-5, 0), pointSet.Points[0].PointB, "8.5");
                Assert.AreEqual(0, pointSet.Points[1].Distance, 0.00001, "8.6");
                Assert.AreEqual(new Vect(0, 0), pointSet.Points[1].PointA, "8.7");
                Assert.AreEqual(new Vect(0, 0), pointSet.Points[1].PointB, "8.8");

                Assert.IsTrue(first, "#9");
                Assert.IsFalse(removal, "#10");
                Assert.AreEqual(1, count, "#11");
                Assert.AreEqual(new Vect(1, 0), normal, "#12");
                Assert.AreEqual(new Vect(5, 0), pointA, "#13");
                Assert.AreEqual(new Vect(-5, 0), pointB, "#14");
                Assert.AreEqual(-10, depth, 0.00001, "#15");
            }

        }

        [Test]
        public void AddHastyPostStepCallback()
        {
            var space = new HastySpace();
            string foo = string.Empty;

            space.Threads = 0;

            _ = space.AddPostStepCallback((s, k, d) => foo = k + " " + d, "key", "data");

            space.Step(0.1);

            Assert.AreEqual("key data", foo, "#1");
            space.Dispose();
        }
    }
}
;
