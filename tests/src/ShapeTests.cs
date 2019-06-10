﻿
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

        [Test]
        public void CollideTest()
        {
            var space = new Space();

            var body = new Body();
            var shape = new Shape(body, 2, 2, 0);

            var body2 = new Body();
            var shape2 = new Shape(body2, 3, 3, 0);

            body.Position = new cpVect(2, 1);
            body2.Position = new cpVect(3, 2);

            space.AddBody(body);
            space.AddShape(shape);

            space.AddBody(body2);
            space.AddShape(shape2);

            ContactPointSet pointSet = shape.Collide(shape2);

            Assert.AreEqual(2, pointSet.Count, "#1");
            Assert.AreEqual(new cpVect(0, 1), pointSet.Normal, "#2");

            Assert.AreEqual(2, pointSet.Points.Count, "#3.0");

            Assert.AreEqual(new cpVect(3, 2), pointSet.Points[0].PointA, "#3.1");
            Assert.AreEqual(new cpVect(3, 0.5), pointSet.Points[0].PointB, "#3.2");
            Assert.AreEqual(-1.5, pointSet.Points[0].Distance, "#3.3");

            Assert.AreEqual(new cpVect(1.5, 2), pointSet.Points[1].PointA, "#4.1");
            Assert.AreEqual(new cpVect(1.5, 0.5), pointSet.Points[1].PointB, "#4.2");
            Assert.AreEqual(-1.5, pointSet.Points[1].Distance, "#4.3");
        }
    }
}