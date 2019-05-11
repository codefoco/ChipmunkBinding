
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
            var space = new Space();

            space.Iterations = 10;

            Assert.AreEqual(10, space.Iterations, "#1");

            space.Iterations = 15;

            Assert.AreEqual(15, space.Iterations, "#2");

            space.Dispose();
        }

        [Test]
        public void Gravity()
        {
            var space = new Space();
            var g = new cpVect() { x = 0, y = -10 };
            space.Gravity = g;

            Assert.AreEqual(g, space.Gravity, "#1");
        }

        [Test]
        public void Damping()
        {
            var space = new Space();
            space.Damping = 0.10;

            Assert.AreEqual(0.10, space.Damping, "#1");
        }

        [Test]
        public void IdleSpeedThreshold()
        {
            var space = new Space();
            space.IdleSpeedThreshold = 0.10;

            Assert.AreEqual(0.10, space.IdleSpeedThreshold, "#1");
        }

        [Test]
        public void StaticBody()
        {
            var space = new Space();
            Body body = space.StaticBody;
            Body body2 = space.StaticBody;

            Assert.AreSame(body, body2, "#1");
        }

        [Test]
        public void ContainsRemove()
        {
            var space = new Space();
            var body = new Body();


            Assert.IsFalse(space.Contains(body), "#1");

            space.AddBody(body);

            Assert.IsTrue(space.Contains(body), "#2");

            space.Remove(body);

            Assert.IsFalse(space.Contains(body), "#3");
        }

        [Test]
        public void AddPostStepCallback()
        {
            var space = new Space();
            string foo = string.Empty;
            
            space.AddPostStepCallback((s, k, d) => foo = k + " " + d, "key", "data");

            space.Step(0.1);

            Assert.AreEqual("key data", foo, "#1");
        }


    }
}
