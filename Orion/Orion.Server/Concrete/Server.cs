namespace Orion.Server.Concrete
{
    using System.Net.Sockets;

    using Orion.Logger.Abstract;
    using Orion.Network.Abstract;
    using Orion.Server.Abstract;
    using Orion.Server.Wrapper.Abstract;

    public class Server : IServer
    {
        private const string ReadyForConnectionMessage = "Ready for connections...";

        private const string StartUpMessage = "Starting Server...";

        private readonly IOrionLogger orionLogger;

        private readonly IConnectionProcessor tcpClientProcessor;

        private readonly ITcpListenerWrapper tcpListenerWrapper;

        public Server(
            IOrionLogger orionLogger,
            ITcpListenerWrapper tcpListenerWrapper,
            IConnectionProcessor tcpClientProcessor)
        {
            this.orionLogger = orionLogger;
            this.tcpListenerWrapper = tcpListenerWrapper;
            this.tcpClientProcessor = tcpClientProcessor;
        }

        public bool ServerRunning { get; set; }

        public void Run()
        {
            LogMessage(StartUpMessage);
            StartServer();

            LogMessage(ReadyForConnectionMessage);
            while (ServerRunning)
            {
                TcpClient client = AcceptNextPendingConnectionRequest();
                ProcessConnectionRequest(client);
            }
        }

        private TcpClient AcceptNextPendingConnectionRequest()
        {
            return tcpListenerWrapper.AcceptTcpClient();
        }

        private void LogMessage(string message)
        {
            orionLogger.LogMessage(message);
        }

        private void ProcessConnectionRequest(TcpClient client)
        {
            tcpClientProcessor.ProcessConnection(client);
        }

        private void StartServer()
        {
            tcpListenerWrapper.Start();
            ServerRunning = true;
        }
    }
}