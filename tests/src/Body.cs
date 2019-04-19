
using ChipmunkBinding;
using NUnit.Framework;


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
        public void CreateBody ()
        {
            var body = new Body();

            Assert.IsNotNull(body.Handle, "#1");
        }
    }
}
