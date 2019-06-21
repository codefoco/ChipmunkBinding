
using System.Collections.Generic;
using ChipmunkBinding;
using NUnit.Framework;

using cpBody = System.IntPtr;



namespace ChipmunkBindingTest.Tests
{
    [TestFixture]
    public class BodyTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void ManagedToNativeRoundtrip()
        {
            var body = new Body();

            cpBody handle = body.Handle;

            var bodyFromHandle = Body.FromHandle(handle);

            Assert.AreSame(body, bodyFromHandle, "#1");

            body.Dispose();
        }

        [Test]
        public void CreateBody ()
        {
            var body = new Body();

            Assert.IsNotNull(body.Handle, "#1");

            var body2 = new Body(10.0, 0.0, BodyType.Dinamic);

            Assert.AreEqual(10.0, body2.Mass, "#2");
            Assert.AreEqual(0.0, body2.Moment, "#3");
            Assert.AreEqual(BodyType.Dinamic, body2.Type, "#4");

            body.Dispose();
            body2.Dispose();
        }

        [Test]
        public void TypeProperty()
        {
            var body = new Body(BodyType.Kinematic);

            Assert.AreEqual(BodyType.Kinematic, body.Type, "#1");

            body.Type = BodyType.Static;

            Assert.AreEqual(BodyType.Static, body.Type, "#2");

            body.Dispose();
        }

        [Test]
        public void SpaceProperty()
        {
            var space = new Space();
            var body = new Body();

            Assert.IsNull(body.Space,"#1");

            space.AddBody(body);

            Assert.AreSame(space, body.Space, "#2");

            body.Dispose();
            space.Dispose();
        }

        [Test]
        public void AngleProperty()
        {
            var body = new Body();

            body.Angle = System.Math.PI;

            Assert.AreEqual(System.Math.PI, body.Angle, "#1");
            body.Dispose();
        }

        [Test]
        public void PositionProperty()
        {
            var body = new Body();
            var pos = new Vect(10, 20);

            body.Position = pos;

            Assert.AreEqual(pos, body.Position, "#1");

            body.Dispose();
        }

        [Test]
        public void CenterOfGravityProperty()
        {
            var body = new Body();
            var center = new Vect (10, 20);

            body.CenterOfGravity = center;

            Assert.AreEqual(center, body.CenterOfGravity, "#1");
            body.Dispose();
        }

        [Test]
        public void VelocityProperty()
        {
            var body = new Body();
            var velocity = new Vect(10,-20);

            body.Velocity = velocity;

            Assert.AreEqual(velocity, body.Velocity, "#1");
            body.Dispose();
        }

        [Test]
        public void ForceProperty()
        {
            var body = new Body();
            var force = new Vect(10,-20);

            body.Force = force;

            Assert.AreEqual(force, body.Force, "#1");
            body.Dispose();
        }

        [Test]
        public void AngularVelocityProperty()
        {
            var body = new Body();

            body.AngularVelocity = System.Math.PI;

            Assert.AreEqual(System.Math.PI, body.AngularVelocity, "#1");
            body.Dispose();
        }

        [Test]
        public void TorqueProperty()
        {
            var body = new Body();

            body.Torque = 10.0;

            Assert.AreEqual(10.0, body.Torque, "#1");
            body.Dispose();
        }

        [Test]
        public void ArbitersProperty()
        {
            var body = new Body();

            IReadOnlyList<Arbiter> arbiters = body.Arbiters;

            Assert.AreEqual(0, arbiters.Count, "#1");
            body.Dispose();
        }

        [Test]
        public void ShapesProperty()
        {
            var body = new Body();

            IReadOnlyList<Shape> shapes = body.Shapes;

            Assert.AreEqual(0, shapes.Count, "#1");
            body.Dispose();
        }

        [Test]
        public void ConstraintsProperty()
        {
            var body = new Body();

            IReadOnlyList<Constraint> constraints = body.Constraints;

            Assert.AreEqual(0, constraints.Count, "#1");
            body.Dispose();
        }

    }
}
