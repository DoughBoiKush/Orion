namespace Orion.Server.Concrete
{
    using System.Net.Sockets;
    using System.Threading;

    using Orion.Logger.Abstract;
    using Orion.Network.Abstract;
    using Orion.Server.Abstract;
    using Orion.Server.Wrapper.Abstract;

    public class ServerProvider : IServerProvider
    {
        private const string ReadyForConnectionMessage = "Ready for connections...";

        private const string StartUpMessage = "Starting Server...";

        private readonly IOrionLogger orionLogger;

        private readonly IConnectionProcessor tcpClientProcessor;

        private readonly ITcpListenerWrapper tcpListenerWrapper;

        public ServerProvider(
            IOrionLogger orionLogger,
            ITcpListenerWrapper tcpListenerWrapper,
            IConnectionProcessor tcpClientProcessor)
        {
            this.orionLogger = orionLogger;
            this.tcpListenerWrapper = tcpListenerWrapper;
            this.tcpClientProcessor = tcpClientProcessor;
        }

        public bool ServerRunning { get; set; }

        public void RunServer()
        {
            LogMessage(StartUpMessage);
            StartServer();

            while (ServerRunning)
            {
                LogMessage(ReadyForConnectionMessage);
                Socket socket = AcceptNextPendingConnectionRequest();
                ProcessConnectionRequest(socket);
            }
        }

        private Socket AcceptNextPendingConnectionRequest()
        {
            return tcpListenerWrapper.AcceptSocket();
        }

        private void LogMessage(string message)
        {
            orionLogger.LogMessage(message);
        }

        private void ProcessConnection(object obj)
        {
            var socket = (Socket)obj;

            tcpClientProcessor.ProcessConnection(socket);
        }

        private void ProcessConnectionRequest(Socket socket)
        {
            ThreadPool.QueueUserWorkItem(ProcessConnection, socket);
        }

        private void StartServer()
        {
            tcpListenerWrapper.Start();
            ServerRunning = true;
        }
    }
}