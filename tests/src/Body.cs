
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

            body = new Body(10.0, 0.0, BodyType.Dinamic);

            Assert.AreEqual(10.0, body.Mass, "#2");
            Assert.AreEqual(0.0, body.Moment, "#3");
            Assert.AreEqual(BodyType.Dinamic, body.Type, "#4");
        }

        [Test]
        public void TypeProperty()
        {
            var body = new Body(BodyType.Kinematic);
            

            Assert.AreEqual(BodyType.Kinematic, body.Type, "#1");

            body.Type = BodyType.Static;

            Assert.AreEqual(BodyType.Static, body.Type, "#2");
        }


    }
}
