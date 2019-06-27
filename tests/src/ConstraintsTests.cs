
using System.Text;
using ChipmunkBinding;
using NUnit.Framework;

using cpConstraint = System.IntPtr;


namespace ChipmunkBindingTest.Tests
{
    [TestFixture]
    public class ConstraintsTests
    {
        Space space;
        Body bodyA;
        Shape shapeA;

        Body bodyB;
        Shape shape2;

        [SetUp]
        public void SetUp()
        {
            space = new Space();

            bodyA = new Body();
            shapeA = new Box(bodyA, 2, 2, 0);

            bodyB = new Body();
            shape2 = new Box(bodyB, 3, 3, 0);

            bodyA.Position = new Vect(2, 1);
            bodyB.Position = new Vect(3, 2);

            space.AddBody(bodyA);
            space.AddShape(shapeA);

            space.AddBody(bodyB);
            space.AddShape(shape2);
        }

        [TearDown]
        public void TearDown()
        {
            space.Dispose();
        }

        [Test]
        public void ManagedToNativeRoundtripAndObjectData()
        {
            var constraint = new PinJoint(bodyA, bodyB, Vect.Zero, Vect.Zero);

            var objData = new object();

            constraint.Data = objData;

            cpConstraint handle = constraint.Handle;

            var constraintFromHandle = Constraint.FromHandle(handle);

            Assert.AreSame(constraint, constraintFromHandle, "#1");
            Assert.AreSame(objData, constraintFromHandle.Data, "#2");

            constraint.Dispose();
        }

        [Test]
        public void ConstraintProperties()
        {
            var anchorA = new Vect(1, 1);
            var anchorB = new Vect(0.5, 0.5);

            var constraint = new PinJoint(bodyA, bodyB, anchorA, anchorB);

            Assert.IsNull(constraint.Space, "#1");

            space.AddConstraint(constraint);

            Assert.AreSame(space, constraint.Space, "#1.2");

            Assert.AreSame(bodyA, constraint.BodyA, "#2");
            Assert.AreSame(bodyB, constraint.BodyB, "#3");

            Assert.AreEqual(anchorA, constraint.AnchorA, "#2.1");
            Assert.AreEqual(anchorB, constraint.AnchorB, "#3.1");

            constraint.MaxForce = 1.2;

            Assert.AreEqual(1.2, constraint.MaxForce, "#4");

            constraint.ErrorBias = 10.0;

            Assert.AreEqual(10.0, constraint.ErrorBias, "#5");

            constraint.CollideBodies = false;
            Assert.IsFalse(constraint.CollideBodies, "#6");

            constraint.CollideBodies = true;
            Assert.IsTrue(constraint.CollideBodies, "#7");

            var sb = new StringBuilder();

            constraint.PreSolve = (c, s) => sb.Append("PreSolve");
            constraint.PostSolve = (c, s) => sb.Append("PostSolve");

            space.Step(0.2);

            constraint.PreSolve = null;
            constraint.PostSolve = null;

            space.Step(0.2);


            string solve = sb.ToString();

            Assert.AreEqual("PreSolvePostSolve", solve, "#8");
        }
    }
}
