
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
            var body = new Body(1, 1.66);

            cpBody handle = body.Handle;

            var bodyFromHandle = Body.FromHandle(handle);

            Assert.AreSame(body, bodyFromHandle, "#1");

            body.Dispose();
        }

        [Test]
        public void CreateBody ()
        {
            var body = new Body(1, 1.66);

            Assert.IsNotNull(body.Handle, "#1");

            var body2 = new Body(10.0, 0.0, BodyType.Dynamic);

            Assert.AreEqual(10.0, body2.Mass, "#2");
            Assert.AreEqual(0.0, body2.Moment, "#3");
            Assert.AreEqual(BodyType.Dynamic, body2.Type, "#4");

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
            var body = new Body(1, 1.66);

            Assert.IsNull(body.Space,"#1");

            space.AddBody(body);

            Assert.AreSame(space, body.Space, "#2");

            body.Dispose();
            space.Dispose();
        }

        [Test]
        public void AngleProperty()
        {
            var body = new Body(1, 1.66);

            body.Angle = System.Math.PI;

            Assert.AreEqual(System.Math.PI, body.Angle, "#1");
            body.Dispose();
        }

        [Test]
        public void PositionProperty()
        {
            var body = new Body(1, 1.66);
            var pos = new Vect(10, 20);

            body.Position = pos;

            Assert.AreEqual(pos, body.Position, "#1");

            body.Dispose();
        }

        [Test]
        public void CenterOfGravityProperty()
        {
            var body = new Body(1, 1.66);
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
            var body = new Body(1, 1.66);
            var force = new Vect(10,-20);

            body.Force = force;

            Assert.AreEqual(force, body.Force, "#1");
            body.Dispose();
        }

        [Test]
        public void AngularVelocityProperty()
        {
            var body = new Body(1, 1.66);

            body.AngularVelocity = System.Math.PI;

            Assert.AreEqual(System.Math.PI, body.AngularVelocity, "#1");
            body.Dispose();
        }

        [Test]
        public void TorqueProperty()
        {
            var body = new Body(1, 1.66);

            body.Torque = 10.0;

            Assert.AreEqual(10.0, body.Torque, "#1");
            body.Dispose();
        }

        [Test]
        public void ArbitersProperty()
        {
            var body = new Body(1, 1.66);

            IReadOnlyList<Arbiter> arbiters = body.Arbiters;

            Assert.AreEqual(0, arbiters.Count, "#1");
            body.Dispose();
        }

        [Test]
        public void ShapesProperty()
        {
            var body = new Body(1, 1.66);

            IReadOnlyList<Shape> shapes = body.Shapes;

            Assert.AreEqual(0, shapes.Count, "#1");
            body.Dispose();
        }

        [Test]
        public void ConstraintsProperty()
        {
            var body = new Body(1, 1.66);

            IReadOnlyList<Constraint> constraints = body.Constraints;

            Assert.AreEqual(0, constraints.Count, "#1");
            body.Dispose();
        }

        bool calledMyVelocityUpdateFunction;
        Body myUpdateFunctionBody;
        Vect myVelocityUpdateFunctionGravity = new Vect(10, 0);
        double myVelocityUpdateFunctionDamping = -1;
        double myUpdateFunctionDt;

        [Test]
        public void VelocityUpdateFunctionCallback()
        {
            myUpdateFunctionDt = 0.0;
            myUpdateFunctionBody = null;

            var body = new Body(1, 1.66);
            var space = new Space();

            space.AddBody(body);

            body.VelocityUpdateFunction = MyVelocityUpdateFunction;

            body.Force = new Vect(10, 0);

            space.Step(0.2);

            Assert.True(calledMyVelocityUpdateFunction, "#1");

            Assert.AreSame(myUpdateFunctionBody, body, "#2");
            Assert.AreEqual(Vect.Zero, myVelocityUpdateFunctionGravity, "#3");
            Assert.AreEqual(1, myVelocityUpdateFunctionDamping, "#4");
            Assert.AreEqual(0.2, myUpdateFunctionDt, "#5");

            body.VelocityUpdateFunction = null;
            calledMyVelocityUpdateFunction = false;

            space.Step(0.2);

            Assert.False(calledMyVelocityUpdateFunction, "#6");

            space.Dispose();
        }

        private void MyVelocityUpdateFunction(Body body, Vect gravity, double damping, double dt)
        {
            calledMyVelocityUpdateFunction = true;
            myUpdateFunctionBody = body;
            myVelocityUpdateFunctionGravity = gravity;
            myUpdateFunctionDt = dt;
            myVelocityUpdateFunctionDamping = damping;
        }

        bool calledMyPositionUpdateFunction;

        [Test]
        public void PositionUpdateFunctionCallback()
        {
            myUpdateFunctionDt = 0.0;
            myUpdateFunctionBody = null;

            var body = new Body(1, 1.66);
            var space = new Space();

            space.AddBody(body);

            body.PositionUpdateFunction = MyPositionUpdateFunction;

            body.Velocity = new Vect(10, 0);

            space.Step(0.2);

            Assert.True(calledMyPositionUpdateFunction, "#1");

            Assert.AreSame(myUpdateFunctionBody, body, "#2");
            Assert.AreEqual(0.2, myUpdateFunctionDt, "#3");

            body.PositionUpdateFunction = null;
            calledMyPositionUpdateFunction = false;

            space.Step(0.2);

            Assert.False(calledMyVelocityUpdateFunction, "#4");

            space.Dispose();
        }

        private void MyPositionUpdateFunction(Body body, double dt)
        {
            calledMyPositionUpdateFunction = true;
            myUpdateFunctionDt = dt;
            myUpdateFunctionBody = body;
        }
    }
}
