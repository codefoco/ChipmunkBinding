
using System;
using ChipmunkBinding;
using ChipmunkBinding.Unsafe;
using NUnit.Framework;

using cpShape = System.IntPtr;



namespace ChipmunkBindingTest.Tests
{
    [TestFixture]
    public class ShapeTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void ManagedToNativeRoundtrip()
        {
            var body = new Body(1, 1.66);
            var shape = new Box(body, 10, 10, 1);

            cpShape handle = shape.Handle;

            var shapeFromHandle = Shape.FromHandle(handle);

            Assert.AreSame(shape, shapeFromHandle, "#1");

            shape.Dispose();
        }

        [Test]
        public void CacheBoundBox()
        {
            var body = new Body(1, 1.66);
            var shape = new Box(body, 10, 10, 0);

            body.Position = new Vect(5, 5);

            BoundingBox boundingBox = shape.CacheBB();


            Assert.AreEqual(new BoundingBox(0, 0, 10, 10), boundingBox, "#1");
        }

        [Test]
        public void UpdateTransform()
        {
            var body = new Body(1, 1.66);
            var shape = new Box(body, 10, 10, 0);
            body.Angle = Math.PI / 4;
            var scale = new Transform(2, 0, 0, 2, 0, 0);

            BoundingBox boundingBox = shape.Update(scale);

            Assert.AreEqual(new BoundingBox(-10, -10, 10, 10), boundingBox, "#1");

        }

        [Test]
        public void PointQueryTest()
        {
            var body = new Body(1, 1.66);
            var shape = new Box(body, 2, 2, 0);

            PointQueryInfo point = shape.PointQuery(new Vect(3, 4));

            Assert.AreEqual(-5, point.Distance, "#1");
            Assert.AreEqual(new Vect(0, 0), point.Point, "#2");
            Assert.AreSame(shape, point.Shape, "#3");
            Assert.AreEqual(-0.6, point.Gradient.X, 0.00000000001, "#4");
            Assert.AreEqual(-0.8, point.Gradient.Y, 0.00000000001, "#5");
        }

        [Test]
        public void SegmentQueryTest()
        {
            var body = new Body(1, 1.66);
            var shape = new Box(body, 1, 1, 0);

            var a = new Vect(-3, 0);
            var b = new Vect(3, 0);

            SegmentQueryInfo info = shape.SegmentQuery(a, b, 1.0);

            Assert.AreEqual(0, info.Alpha, "#1");
            Assert.AreEqual(new Vect(-1, 0), info.Normal, "#2");
            Assert.AreSame(shape, info.Shape, "#3");
            Assert.AreEqual(new Vect(3, 0), info.Point, "#4");
        }

        [Test]
        public void CollideTest()
        {
            var space = new Space();

            var body = new Body(1, 1.66);
            var shape = new Box(body, 2, 2, 0);

            var body2 = new Body(1, 1.66);
            var shape2 = new Box(body2, 3, 3, 0);

            body.Position = new Vect(2, 1);
            body2.Position = new Vect(3, 2);

            space.AddBody(body);
            space.AddShape(shape);

            space.AddBody(body2);
            space.AddShape(shape2);

            ContactPointSet pointSet = shape.Collide(shape2);

            Assert.AreEqual(2, pointSet.Count, "#1");
            Assert.AreEqual(new Vect(0, 1), pointSet.Normal, "#2");

            Assert.AreEqual(2, pointSet.Points.Count, "#3.0");

            Assert.AreEqual(new Vect(3, 2), pointSet.Points[0].PointA, "#3.1");
            Assert.AreEqual(new Vect(3, 0.5), pointSet.Points[0].PointB, "#3.2");
            Assert.AreEqual(-1.5, pointSet.Points[0].Distance, "#3.3");

            Assert.AreEqual(new Vect(1.5, 2), pointSet.Points[1].PointA, "#4.1");
            Assert.AreEqual(new Vect(1.5, 0.5), pointSet.Points[1].PointB, "#4.2");
            Assert.AreEqual(-1.5, pointSet.Points[1].Distance, "#4.3");

            space.Dispose();
        }

        [Test]
        public void SpaceTest()
        {
            var space = new Space();

            var body = new Body(1, 1.66);
            var shape = new Box(body, 2, 2, 0);

            Assert.IsNull(shape.Space, "#1");

            space.AddBody(body);
            space.AddShape(shape);

            Assert.AreSame(space, shape.Space, "#2");

            space.Dispose();
        }

        [Test]
        public void BodyTest()
        {
            var body = new Body(1, 1.66);
            var shape = new Box(body, 2, 2, 0);

            Assert.AreSame(body, shape.Body, "#1");

            var body2 = new Body(BodyType.Static);

            shape.Body = body2;

            Assert.AreSame(body2, shape.Body, "#2");
        }

        [Test]
        public void MassTest()
        {
            var body = new Body(10, 16.666);
            var shape = new Box(body, 2, 2, 0);

            shape.Mass = 10;

            Assert.AreEqual(10, shape.Mass, "#1");
        }

        [Test]
        public void MiscPropertyTest()
        {
            var space = new Space();
            var body = new Body(10, 16.666);
            var shape = new Box(body, 2, 2, 0);

            body.Position = new Vect(3, 2);

            shape.Density = 10;

            space.AddBody(body);
            space.AddShape(shape);

            Assert.AreEqual(10, shape.Density, "#1");

            double moment = shape.Moment;
            Assert.AreEqual(26.666666666, moment, 0.0000001, "#2");

            double area = shape.Area;
            Assert.AreEqual(4, area, "#3");

            Vect center = shape.CenterOfGravity;
            Assert.AreEqual(new Vect(0, 0), center, "#4");

            BoundingBox boundBox = shape.BoundingBox;
            Assert.AreEqual(new BoundingBox(2, 1, 4, 3), boundBox, "#5");

            bool sensor = shape.Sensor;
            Assert.AreEqual(false, sensor, "#6");

            shape.Sensor = true;
            Assert.AreEqual(true, shape.Sensor, "#6.1");

            shape.Elasticity = 0.7;
            Assert.AreEqual(0.7, shape.Elasticity, "#7");

            shape.Friction = 0.4;
            Assert.AreEqual(0.4, shape.Friction, "#8");

            shape.SurfaceVelocity = new Vect(-1, 2);
            Assert.AreEqual(new Vect(-1, 2), shape.SurfaceVelocity, "#9");

            shape.CollisionType = 13;
            Assert.AreEqual(13, shape.CollisionType, "#10");

            ShapeFilter filter = shape.Filter;

            shape.Filter = ShapeFilter.All;
            Assert.AreEqual(ShapeFilter.All, shape.Filter, "#11");

            space.Dispose();
        }

        [Test]
        public void CircleOffset()
        {
            var space = new Space();
            var body = new Body(10, 360);
            var shape = new Circle(body, 2, new Vect(3, 5));

            body.Position = new Vect(3, 2);

            shape.Density = 10;

            space.AddBody(body);
            space.AddShape(shape);

            Assert.AreEqual(new Vect(3, 5), shape.Offset, "#1");
            Assert.AreEqual(2, shape.Radius, "#2");

            double moment = shape.MomentForMass(10);
            double area = shape.Area;

            Assert.AreEqual(360, moment, "#3");
            Assert.AreEqual(12.5663706143592, area, 0.00001, "#4");

            space.Dispose();
        }

        [Test]
        public void SegmentTest()
        {
            var space = new Space();
            var body = new Body(10, 224.037008503093);
            var shape = new Segment(body, new Vect(1, 2), new Vect(3, 5), 2);

            body.Position = new Vect(3, 2);

            shape.Density = 10;

            space.AddBody(body);
            space.AddShape(shape);

            Assert.AreEqual(new Vect(1, 2), shape.A, "#1");
            Assert.AreEqual(new Vect(3, 5), shape.B, "#2");

            Assert.AreEqual(2, shape.Radius, "#3");

            Assert.AreEqual(0.832050294337844, shape.Normal.X, 0.00001, "#3");
            Assert.AreEqual(-0.554700196225229, shape.Normal.Y, 0.00001, "#4");

            double moment = shape.MomentForMass(10);
            double area = shape.Area;

            Assert.AreEqual(224.037008503093, moment, 0.0000001, "#5");
            Assert.AreEqual(26.9885757162151, area, 0.0000001, "#6");

            space.Dispose();
        }

        [Test]
        public void PolygonTest()
        {
            var space = new Space();
            var body = new Body(10, 16.666);
            var body2 = new Body(10, 16.666);

            Vect[] vertexs = { new Vect(-1, 0), new Vect(0, -1), new Vect(1, 0), new Vect(0, 1) };

            var polygon = new Polygon(body, vertexs, 2.0);
            var polygon2 = new Polygon(body2, vertexs, Transform.Identity, 1.0);

            space.AddBody(body);
            space.AddShape(polygon);

            space.AddBody(body2);
            space.AddShape(polygon2);


            Assert.AreEqual(vertexs.Length, polygon.Count, "#1");
            Assert.AreEqual(vertexs.Length, polygon2.Count, "#1.1");

            for (int i = 0; i < vertexs.Length; i++)
            {
                Assert.AreEqual(vertexs[i], polygon.GetVertex(i), "#2.1." + i);
                Assert.AreEqual(vertexs[i], polygon2.GetVertex(i), "#2.2." + i);
            }

            Assert.AreEqual(2.0, polygon.Radius, "#3.1");
            Assert.AreEqual(1.0, polygon2.Radius, "#3.1");

            space.Dispose();
        }

        [Test]
        public void UnsafeCircle()
        {
            var space = new Space();
            var body = new Body(10, 16.666);
            var shape = new Circle(body, 2, new Vect(3, 5));

            body.Position = new Vect(3, 2);

            shape.Density = 10;

            space.AddBody(body);
            space.AddShape(shape);

            Assert.AreEqual(new Vect(3, 5), shape.Offset, "#1");
            Assert.AreEqual(2, shape.Radius, "#2");

            shape.SetOffset(new Vect(10, 13));
            shape.SetRadius(4);

            Assert.AreEqual(new Vect(10, 13), shape.Offset, "#3");
            Assert.AreEqual(4, shape.Radius, "#4");

            space.Dispose();
        }

        [Test]
        public void UnsafePolygon()
        {
            var space = new Space();
            var body = new Body(10, 16.666);

            Vect[] vertexs = { new Vect(-1, 0), new Vect(0, -1), new Vect(1, 0), new Vect(0, 1) };

            var polygon = new Polygon(body, vertexs, 2.0);

            space.AddBody(body);
            space.AddShape(polygon);

            Vect[] vertexs2 = { new Vect(-2, 0), new Vect(0, -2), new Vect(2, 0), new Vect(0, 2) };

            polygon.SetVertexes(vertexs2);

            for (int i = 0; i < vertexs.Length; i++)
            {
                Assert.AreEqual(vertexs2[i], polygon.GetVertex(i), "#2.1." + i);
            }

            space.Dispose();

        }

        [Test]
        public void UnsafeSegment()
        {
            var space = new Space();
            var body = new Body(10, 16.666);
            var shape = new Segment(body, new Vect(1, 2), new Vect(3, 5), 2);

            body.Position = new Vect(3, 2);

            shape.Density = 10;

            space.AddBody(body);
            space.AddShape(shape);

            shape.SetEndpoints(new Vect(2, 4), new Vect(6, 2));
            shape.SetRadius(3);

            Assert.AreEqual(new Vect(2, 4), shape.A, "#1");
            Assert.AreEqual(new Vect(6, 2), shape.B, "#2");
            Assert.AreEqual(3, shape.Radius, "#3");
        }


        [Test]
        public void MiscFunctions()
        {

        }
    }
}
