
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



    }
}
