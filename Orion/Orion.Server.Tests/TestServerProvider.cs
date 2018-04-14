namespace Orion.Server.Tests
{
    using NSubstitute;

    using NUnit.Framework;

    using Orion.Logger.Abstract;
    using Orion.Server.Concrete;
    using Orion.Server.Wrapper.Abstract;

    [TestFixture]
    public class TestServerProvider
    {
        private const string ReadyForConnectionMessage = "Reading for connections...";

        private const string StartUpMessage = "Starting Server...";

        private IOrionLogger orionLoggerMock;

        private ServerProvider systemUnderTest;

        private ITcpListenerWrapper tcpListenerWrapperMock;

        [Test]
        public void RunServer_WhenInvoked_LoggerMockLogMessageIsCalledWithReadyForConnectionMessage()
        {
            InvokeRunServer();

            orionLoggerMock.Received(1).LogMessage(ReadyForConnectionMessage);
        }

        [Test]
        public void RunServer_WhenInvoked_LoggerMockLogMessageIsCalledWithStartUpMessage()
        {
            InvokeRunServer();

            orionLoggerMock.Received(1).LogMessage(StartUpMessage);
        }

        [Test]
        public void RunServer_WhenInvoked_TcpListenerWrapperMockStartIsCalled()
        {
            InvokeRunServer();

            tcpListenerWrapperMock.Received(1).Start();
        }

        [Test]
        public void RunServer_WhenInvoked_TcpListnerWrapperMockAcceptTcpClientIsCalled()
        {
            InvokeRunServer();

            tcpListenerWrapperMock.Received(1).AcceptTcpClient();
        }

        [SetUp]
        public void Setup()
        {
            tcpListenerWrapperMock = CreateTcpListenerWrapperMock();
            orionLoggerMock = CreateOrionLoggerMock();
            systemUnderTest = CreateSystemUnderTest();
        }

        private IOrionLogger CreateOrionLoggerMock()
        {
            return Substitute.For<IOrionLogger>();
        }

        private ServerProvider CreateSystemUnderTest()
        {
            return new ServerProvider(orionLoggerMock, tcpListenerWrapperMock);
        }

        private ITcpListenerWrapper CreateTcpListenerWrapperMock()
        {
            return Substitute.For<ITcpListenerWrapper>();
        }

        private void InvokeRunServer()
        {
            systemUnderTest.RunServer();
        }
    }
}