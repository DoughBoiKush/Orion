namespace Orion.Server
{
    using Orion.Logger.Abstract;
    using Orion.Logger.Concrete;
    using Orion.Server.Abstract;
    using Orion.Server.Concrete;
    using Orion.Server.Wrapper.Abstract;
    using Orion.Server.Wrapper.Concrete;

    public class ServerRunner
    {
        private static IOrionLogger orionLogger;

        private static IServerProvider serverProvider;

        private static ITcpListenerWrapper tcpListenerWrapper;

        public static void Main()
        {
            tcpListenerWrapper = CreateTcpListenerWrapper();
            orionLogger = CreateOrionLogger();
            serverProvider = CreateServerProvider();

            serverProvider.RunServer();
        }

        private static IOrionLogger CreateOrionLogger()
        {
            return new OrionLogger();
        }

        private static IServerProvider CreateServerProvider()
        {
            return new ServerProvider(orionLogger, tcpListenerWrapper);
        }

        private static ITcpListenerWrapper CreateTcpListenerWrapper()
        {
            return new TcpListenerWrapper();
        }
    }
}