// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2023 Codefoco LTDA - The above copyright notice and this permission notice shall be
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
using System.Text;

using ChipmunkBinding;

using NUnit.Framework;

namespace ChipmunkBindingTest.Tests
{
    [TestFixture]
#pragma warning disable CA1001 // Types that own disposable fields should be disposable
    public class ConstraintsTests
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
    {
        private Space space;
        private Body bodyA;
        private Shape shapeA;
        private Body bodyB;
        private Shape shape2;

        [SetUp]
        public void SetUp()
        {
            space = new Space();

            bodyA = new Body(1, 1.666);
            shapeA = new Box(bodyA, 2, 2, 0);

            bodyB = new Body(2, 3.222);
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
            space?.Dispose();
        }

        [Test]
        public void ManagedToNativeRoundtripAndObjectData()
        {
            var constraint = new PinJoint(bodyA, bodyB, Vect.Zero, Vect.Zero);

            object objData = new object();

            constraint.Data = objData;

            IntPtr handle = constraint.Handle;

            Constraint constraintFromHandle = Constraint.FromHandle(handle);

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

        [Test]
        public void PinJointProperties()
        {
            var anchorA = new Vect(1, 1);
            var anchorB = new Vect(0.5, 0.5);

            var constraint = new PinJoint(bodyA, bodyB, anchorA, anchorB)
            {
                Distance = 13
            };

            Assert.IsTrue(PinJoint.IsPinJoint(constraint), "#0");
            Assert.AreEqual(13, constraint.Distance, "#1");
        }

        [Test]
        public void SlideJointProperties()
        {
            var anchorA = new Vect(1, 1);
            var anchorB = new Vect(0.5, 0.5);

            var constraint = new SlideJoint(bodyA,
                                            bodyB,
                                            anchorA,
                                            anchorB, 0.13, 0.80);

            Assert.AreEqual(anchorA, constraint.AnchorA, "#1");
            Assert.AreEqual(anchorB, constraint.AnchorB, "#2");

            constraint.AnchorA = anchorB;
            constraint.AnchorB = anchorA;

            Assert.AreEqual(anchorB, constraint.AnchorA, "#3");
            Assert.AreEqual(anchorA, constraint.AnchorB, "#4");

            Assert.IsTrue(SlideJoint.IsSlideJoint(constraint), "#5");

            Assert.AreEqual(0.13, constraint.Minimum, "#1");
            Assert.AreEqual(0.80, constraint.Maximum, "#2");

            constraint.Minimum = 0.14;
            constraint.Maximum = 0.90;

            Assert.AreEqual(0.14, constraint.Minimum, "#1");
            Assert.AreEqual(0.90, constraint.Maximum, "#2");
        }

        [Test]
        public void PivotJointProperties()
        {
            var anchorA = new Vect(1, 1);
            var anchorB = new Vect(0.5, 0.5);

            var constraint = new PivotJoint(bodyA,
                                            bodyB,
                                            anchorA,
                                            anchorB);

            var constraint2 = new PivotJoint(bodyA,
                                bodyB,
                                Vect.Zero
                                );

            Assert.AreEqual(anchorA, constraint.AnchorA, "#1");
            Assert.AreEqual(anchorB, constraint.AnchorB, "#2");

            constraint.AnchorA = anchorB;
            constraint.AnchorB = anchorA;

            Assert.AreEqual(anchorB, constraint.AnchorA, "#3");
            Assert.AreEqual(anchorA, constraint.AnchorB, "#4");

            Assert.IsTrue(PivotJoint.IsPivotJoint(constraint), "#5");

            Assert.AreEqual(new Vect(-3, -2), constraint2.AnchorB, "#6");
            Assert.AreEqual(new Vect(-2, -1), constraint2.AnchorA, "#7");
        }

        [Test]
        public void GrooveJointProperties()
        {
            var grooveA = new Vect(1, 1);
            var grooveB = new Vect(0.5, 0.5);
            var anchorB = new Vect(1.5, 1.5);

            var constraint = new GrooveJoint(bodyA,
                                            bodyB,
                                            grooveA,
                                            grooveB, anchorB);


            Assert.AreEqual(anchorB, constraint.AnchorB, "#1");
            Assert.AreEqual(grooveA, constraint.GrooveA, "#2");
            Assert.AreEqual(grooveB, constraint.GrooveB, "#3");

            constraint.GrooveA = grooveB;
            constraint.GrooveB = grooveA;

            Assert.AreEqual(grooveB, constraint.GrooveA, "#4");
            Assert.AreEqual(grooveA, constraint.GrooveB, "#5");

            Assert.IsTrue(GrooveJoint.IsGrooveJoint(constraint), "#6");

            constraint.AnchorB = new Vect(0.75, 1);


            Assert.AreEqual(new Vect(0.75, 1), constraint.AnchorB, "#7");
        }

        [Test]
        public void DampedSpringProperties()
        {
            var anchorA = new Vect(1, 1);
            var anchorB = new Vect(0.5, 0.5);

            double restLength = 1.2222;
            double stiffness = 0.888;
            double damping = 0.9090;

            space.Gravity = new Vect(0, -10);

            var spring = new DampedSpring(bodyA,
                                            bodyB,
                                            anchorA,
                                            anchorB, restLength, stiffness, damping);
            var sb = new StringBuilder();
            spring.Data = sb;

            spring.ForceFunction = ForceCallback;

            space.AddConstraint(spring);

            space.Step(1.0);

            Assert.AreEqual("ForceCallback", sb.ToString(), "#0");

            spring.ForceFunction = null;

            space.Step(1.0);

            Assert.AreEqual("ForceCallback", sb.ToString(), "#0.1");

            spring.ForceFunction = ForceCallback;
            space.Step(1.0);

            Assert.AreEqual("ForceCallbackForceCallback", sb.ToString(), "#0.1");

            Assert.AreEqual(anchorA, spring.AnchorA, "#1");
            Assert.AreEqual(anchorB, spring.AnchorB, "#2");

            Assert.AreEqual(restLength, spring.RestLength, "#3");
            Assert.AreEqual(stiffness, spring.Stiffness, "#4");
            Assert.AreEqual(damping, spring.Damping, "#5");

            spring.AnchorB = anchorA;
            spring.AnchorA = anchorB;

            Assert.AreEqual(anchorB, spring.AnchorA, "#6");
            Assert.AreEqual(anchorA, spring.AnchorB, "#7");

            spring.RestLength = 2.0;
            spring.Stiffness = 1.0;
            spring.Damping = 1.1;

            Assert.AreEqual(2.0, spring.RestLength, "#8");
            Assert.AreEqual(1.0, spring.Stiffness, "#9");
            Assert.AreEqual(1.1, spring.Damping, "#10");

            Assert.IsTrue(DampedSpring.IsDampedSpring(spring), "#11");
        }


        private static double ForceCallback(DampedSpring spring, double force)
        {
            var sb = (StringBuilder)spring.Data;
            _ = sb.Append("ForceCallback");
            return force;
        }

        [Test]
        public void DampedRotarySpringProperties()
        {
            double restAngle = 1.2222;
            double stiffness = 0.888;
            double damping = 0.9090;

            space.Gravity = new Vect(0, -10);

            var spring = new DampedRotarySpring(bodyA,
                                            bodyB,
                                            restAngle, stiffness, damping);
            var sb = new StringBuilder();
            spring.Data = sb;

            spring.TorqueFunction = TorqueCallback;

            space.AddConstraint(spring);

            space.Step(1.0);

            Assert.AreEqual("TorqueCallback", sb.ToString(), "#0");

            spring.TorqueFunction = null;

            space.Step(1.0);

            Assert.AreEqual("TorqueCallback", sb.ToString(), "#0.1");

            spring.TorqueFunction = TorqueCallback;

            space.Step(1.0);

            Assert.AreEqual("TorqueCallbackTorqueCallback", sb.ToString(), "#0.2");

            Assert.AreEqual(restAngle, spring.RestAngle, "#1");
            Assert.AreEqual(stiffness, spring.Stiffness, "#2");
            Assert.AreEqual(damping, spring.Damping, "#3");

            spring.RestAngle = 2.0;
            spring.Stiffness = 1.0;
            spring.Damping = 1.1;

            Assert.AreEqual(2.0, spring.RestAngle, "#4");
            Assert.AreEqual(1.0, spring.Stiffness, "#5");
            Assert.AreEqual(1.1, spring.Damping, "#6");

            Assert.IsTrue(DampedRotarySpring.IsDampedRotarySpring(spring), "#7");
        }

        private static double TorqueCallback(DampedRotarySpring spring, double force)
        {
            var sb = (StringBuilder)spring.Data;
            _ = sb.Append("TorqueCallback");
            return force;
        }

        [Test]
        public void RotaryLimitJointProperties()
        {
            double min = 0.111;
            double max = 0.999;

            var constraint = new RotaryLimitJoint(bodyA,
                                            bodyB,
                                            min,
                                            max);

            Assert.AreEqual(min, constraint.Minimum, "#1");
            Assert.AreEqual(max, constraint.Maximum, "#2");

            constraint.Minimum = 0.222;
            constraint.Maximum = 0.888;

            Assert.AreEqual(0.222, constraint.Minimum, "#4");
            Assert.AreEqual(0.888, constraint.Maximum, "#5");

            Assert.IsTrue(RotaryLimitJoint.IsRotaryLimitJoint(constraint), "#6");
        }

        [Test]
        public void RatchetJointProperties()
        {
            double phase = 0.111;
            double ratchet = 0.999;

            var constraint = new RatchetJoint(bodyA,
                                            bodyB,
                                            phase,
                                            ratchet);

            Assert.AreEqual(phase, constraint.Phase, "#1");
            Assert.AreEqual(ratchet, constraint.Rachet, "#2");

            constraint.Phase = 0.222;
            constraint.Rachet = 0.888;
            constraint.Angle = 0.5;

            Assert.AreEqual(0.222, constraint.Phase, "#3");
            Assert.AreEqual(0.888, constraint.Rachet, "#4");
            Assert.AreEqual(0.5, constraint.Angle, "#5");

            Assert.IsTrue(RatchetJoint.IsRatchetJoint(constraint), "#6");
        }

        [Test]
        public void SimpleMotorProperties()
        {
            double rate = 0.111;

            var constraint = new SimpleMotor(bodyA,
                                            bodyB,
                                            rate);

            Assert.AreEqual(rate, constraint.Rate, "#1");

            constraint.Rate = 0.222;

            Assert.AreEqual(0.222, constraint.Rate, "#2");

            Assert.IsTrue(SimpleMotor.IsSimpleMotor(constraint), "#3");
        }
    }
}