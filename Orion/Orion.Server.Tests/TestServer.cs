namespace Orion.Server.Tests
{
    using System.Net.Sockets;

    using NSubstitute;

    using NUnit.Framework;

    using Orion.Logger.Abstract;
    using Orion.Network.Abstract;
    using Orion.Server.Concrete;
    using Orion.Server.Wrapper.Abstract;

    [TestFixture]
    public class TestServer
    {
        private const string ReadyForConnectionsMessage = "Ready for connections...";

        private const string StartUpMessage = "Starting Server...";

        private int loopCount;

        private IOrionLogger orionLoggerMock;

        private Server systemUnderTest;

        private TcpClient tcpClient;

        private IConnectionProcessor tcpClientProcessorMock;

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
        public void RunServer_WhenServerIsRunning_EachAcceptedClientRequestInTheLoopIsProcessed()
        {
            InvokeRunServer();

            tcpClientProcessorMock.Received(loopCount).ProcessConnection(tcpClient);
        }

        [Test]
        public void RunServer_WhenServerIsRunning_ReadyForConnectionsMessageIsLogged()
        {
            InvokeRunServer();

            orionLoggerMock.Received(1).LogMessage(ReadyForConnectionsMessage);
        }

        [Test]
        public void RunServer_WhenServerIsRunning_TcpListenerAcceptsSocketOnEachLoop()
        {
            InvokeRunServer();

            tcpListenerWrapperMock.Received(loopCount).AcceptTcpClient();
        }

        [SetUp]
        public void Setup()
        {
            ZeroTheLoopCount();
            tcpClient = CreateTcpClient();
            tcpClientProcessorMock = CreateTcpClientProcessorMock();
            tcpListenerWrapperMock = CreateTcpListenerWrapperMock();
            orionLoggerMock = CreateOrionLoggerMock();
            systemUnderTest = CreateSystemUnderTest();
        }

        private IOrionLogger CreateOrionLoggerMock()
        {
            return Substitute.For<IOrionLogger>();
        }

        private Server CreateSystemUnderTest()
        {
            return new Server(orionLoggerMock, tcpListenerWrapperMock, tcpClientProcessorMock);
        }

        private TcpClient CreateTcpClient()
        {
            return new TcpClient();
        }

        private IConnectionProcessor CreateTcpClientProcessorMock()
        {
            return Substitute.For<IConnectionProcessor>();
        }

        private ITcpListenerWrapper CreateTcpListenerWrapperMock()
        {
            var listenerWrapperMock = Substitute.For<ITcpListenerWrapper>();

            listenerWrapperMock.AcceptTcpClient().Returns(tcpClient);
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
            systemUnderTest.Run();
        }

        private void ZeroTheLoopCount()
        {
            loopCount = 0;
        }
    }
}