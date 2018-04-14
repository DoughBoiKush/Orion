namespace Orion.Server
{
    using System.Net;

    using Orion.Logger.Abstract;
    using Orion.Logger.Concrete;
    using Orion.Server.Abstract;
    using Orion.Server.Concrete;
    using Orion.Server.Wrapper.Abstract;
    using Orion.Server.Wrapper.Concrete;

    public class ServerRunner
    {
        private const string IpAddressString = "127.0.0.1";

        private const int Port = 43594;

        private static IPAddress ipAddress;

        private static IOrionLogger orionLogger;

        private static IServerProvider serverProvider;

        private static ITcpListenerWrapper tcpListenerWrapper;

        public static void Main()
        {
            ipAddress = CreateIpAddress();
            tcpListenerWrapper = CreateTcpListenerWrapper();
            orionLogger = CreateOrionLogger();

            serverProvider = CreateServerProvider();

            serverProvider.RunServer();
        }

        private static IPAddress CreateIpAddress()
        {
            return IPAddress.Parse(IpAddressString);
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
            return new TcpListenerWrapper(Port, ipAddress);
        }
    }
}