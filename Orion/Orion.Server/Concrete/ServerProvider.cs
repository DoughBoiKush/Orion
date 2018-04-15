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

        private readonly ITcpClientProcessor tcpClientProcessor;

        private readonly ITcpListenerWrapper tcpListenerWrapper;

        public ServerProvider(
            IOrionLogger orionLogger,
            ITcpListenerWrapper tcpListenerWrapper,
            ITcpClientProcessor tcpClientProcessor)
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
                var tcpClient = AcceptNextClientRequest();
                ProcessClientRequest(tcpClient);
            }
        }

        private TcpClient AcceptNextClientRequest()
        {
            return tcpListenerWrapper.AcceptTcpClient();
        }

        private void LogMessage(string message)
        {
            orionLogger.LogMessage(message);
        }

        private void ProcessClientRequest(TcpClient tcpClient)
        {
            ThreadPool.QueueUserWorkItem(ProcessTcpClient, tcpClient);
        }

        private void ProcessTcpClient(object client)
        {
            var tcpClient = (TcpClient)client;

            tcpClientProcessor.ProcessClient(tcpClient);
        }

        private void StartServer()
        {
            tcpListenerWrapper.Start();
            this.ServerRunning = true;
        }
    }
}