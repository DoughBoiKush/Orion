namespace Orion.Server.Concrete
{
    using Orion.Logger.Abstract;
    using Orion.Server.Abstract;
    using Orion.Server.Wrapper.Abstract;

    public class ServerProvider : IServerProvider
    {
        private const string ReadyForConnectionMessage = "Reading for connections...";

        private const string StartUpMessage = "Starting Server...";

        private readonly IOrionLogger orionLogger;

        private readonly ITcpListenerWrapper tcpListenerWrapper;

        public ServerProvider(IOrionLogger orionLogger, ITcpListenerWrapper tcpListenerWrapper)
        {
            this.orionLogger = orionLogger;
            this.tcpListenerWrapper = tcpListenerWrapper;
        }

        public bool ServerRunning { get; set; } = true; //TODO: Setup an actual controlled flag for this.

        public void RunServer()
        {
            orionLogger.LogMessage(StartUpMessage);
            tcpListenerWrapper.Start();

            while (ServerRunning)
            {
                orionLogger.LogMessage(ReadyForConnectionMessage);
                tcpListenerWrapper.AcceptTcpClient();

                ServerRunning = false; //Only to run test once. (For now until implementation is complete)
            }
        }
    }
}