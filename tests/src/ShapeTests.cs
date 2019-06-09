
using System;
using ChipmunkBinding;
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
            var body = new Body();
            var shape = new Shape(body, 10, 10, 1);

            cpShape handle = shape.Handle;

            var shapeFromHandle = Shape.FromHandle(handle);

            Assert.AreSame(shape, shapeFromHandle, "#1");

            shape.Dispose();
        }

        [Test]
        public void CacheBoundBox()
        {
            var body = new Body();
            var shape = new Shape(body, 10, 10, 0);

            body.Position = new cpVect(5, 5);

            cpBB boundingBox = shape.CacheBB();


            Assert.AreEqual(new cpBB(0, 0, 10, 10), boundingBox, "#1");
        }

        [Test]
        public void UpdateTransform()
        {
            var body = new Body();
            var shape = new Shape(body, 10, 10, 0);
            body.Angle = Math.PI / 4;
            var scale = new cpTransform(2, 0, 0, 2, 0, 0);

            cpBB boundingBox = shape.Update(scale);

            Assert.AreEqual(new cpBB(-10, -10, 10, 10), boundingBox, "#1");

        }

        [Test]
        public void PointQueryTest()
        {
            var body = new Body();
            var shape = new Shape(body, 2, 2, 0);

            PointQueryInfo point = shape.PointQuery(new cpVect(3, 4));

            Assert.AreEqual(-5, point.Distance, "#1");
            Assert.AreEqual(new cpVect(0, 0), point.Point, "#2");
            Assert.AreSame(shape, point.Shape, "#3");
            Assert.AreEqual(-0.6, point.Gradient.X, 0.00000000001, "#4");
            Assert.AreEqual(-0.8, point.Gradient.Y, 0.00000000001, "#5");
        }

        [Test]
        public void SegmentQueryTest()
        {
            var body = new Body();
            var shape = new Shape(body, 1, 1, 0);

            var a = new cpVect(-3, 0);
            var b = new cpVect(3, 0);

            SegmentQueryInfo info = shape.SegmentQuery(a, b, 1.0);

            Assert.AreEqual(0, info.Alpha, "#1");
            Assert.AreEqual(new cpVect(-1, 0), info.Normal, "#2");
            Assert.AreSame(shape, info.Shape, "#3");
            Assert.AreEqual(new cpVect(3, 0), info.Point, "#4");
        }
    }
}
