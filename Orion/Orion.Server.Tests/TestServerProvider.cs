namespace Orion.Server.Tests
{
    using NUnit.Framework;

    using Orion.Server.Concrete;

    [TestFixture]
    public class TestServerProvider
    {
        private ServerProvider systemUnderTest;

        [SetUp]
        public void Setup()
        {
            systemUnderTest = CreateSystemUnderTest();
        }

        private ServerProvider CreateSystemUnderTest()
        {
            return new ServerProvider();
        }
    }
}