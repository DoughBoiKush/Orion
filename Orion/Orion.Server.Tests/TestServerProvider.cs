namespace Orion.Server.Tests
{
    using NSubstitute;

    using NUnit.Framework;

    using Orion.Logger.Abstract;
    using Orion.Network.Abstract;
    using Orion.Server.Concrete;
    using Orion.Server.Wrapper.Abstract;

    [TestFixture]
    public class TestServerProvider
    {
        private const string ReadyForConnectionsMessage = "Ready for connections...";

        private const string StartUpMessage = "Starting Server...";

        private int loopCount;

        private IOrionLogger orionLoggerMock;

        private ServerProvider systemUnderTest;

        private ITcpClientProcessor tcpClientProcessorMock;

        private ITcpListenerWrapper tcpListenerWrapperMock;

        [Test]
        public void RunServer_WhenInvoked_StartUpMessageIsLogged()
        {
            InvokeRunServer();

            orionLoggerMock.Received(1).LogMessage(StartUpMessage);
        }

        [Test]
        public void RunServer_WhenInvoked_TcpListenerStarsListeningForTcpConnections()
        {
            InvokeRunServer();

            tcpListenerWrapperMock.Received(1).Start();
        }

        [Test]
        public void RunServer_WhenServerIsRunning_ReadyForConnectionsMessageIsLoggedForEachLoop()
        {
            InvokeRunServer();

            orionLoggerMock.Received(loopCount).LogMessage(ReadyForConnectionsMessage);
        }

        [Test]
        public void RunServer_WhenServerIsRunning_TcpListenerAcceptsTcpClientOnEachLoop()
        {
            InvokeRunServer();

            tcpListenerWrapperMock.Received(loopCount).AcceptTcpClient();
        }

        [SetUp]
        public void Setup()
        {
            ZeroTheLoopCount();
            tcpClientProcessorMock = CreateTcpClientProcessorMock();
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
            return new ServerProvider(orionLoggerMock, tcpListenerWrapperMock, tcpClientProcessorMock);
        }

        private ITcpClientProcessor CreateTcpClientProcessorMock()
        {
            return Substitute.For<ITcpClientProcessor>();
        }

        private ITcpListenerWrapper CreateTcpListenerWrapperMock()
        {
            var listenerWrapperMock = Substitute.For<ITcpListenerWrapper>();

            listenerWrapperMock.When(wrapper => wrapper.AcceptTcpClient()).Do(info => EnsureLoopCount());

            return listenerWrapperMock;
        }

        private void EnsureLoopCount()
        {
            if (loopCount > 1)
            {
                systemUnderTest.ServerRunning = false;
            }

            loopCount++;
        }

        private void InvokeRunServer()
        {
            systemUnderTest.RunServer();
        }

        private void ZeroTheLoopCount()
        {
            loopCount = 0;
        }
    }
}