namespace Orion.Server
{
    using System.Net;
    using System.Net.Sockets;

    using Orion.Logger.Abstract;
    using Orion.Logger.Concrete;
    using Orion.Network.Abstract;
    using Orion.Network.Concrete;
    using Orion.Server.Abstract;
    using Orion.Server.Concrete;

    public class ServerRunner
    {
        private const string IpAddressString = "127.0.0.1";

        private const int Port = 43594;

        private static IOrionLogger orionLogger;

        private static IConnectionProcessor tcpClientProcessor;

        private static ITcpListenerWrapper tcpListenerWrapper;

        public static void Main()
        {
<<<<<<< HEAD
=======
            tcpClientProcessor = CreateTcpClientProcessor();
>>>>>>> dev
            tcpListenerWrapper = CreateTcpListenerWrapper();
            orionLogger = CreateOrionLogger();
            serverProvider = CreateServerProvider();

            IServer server = CreateServer();
            Run(server);
        }

        private static IOrionLogger CreateOrionLogger()
        {
            return new OrionLogger();
        }

        private static IServer CreateServer()
        {
            return new Server(orionLogger, tcpListenerWrapper, tcpClientProcessor);
        }

        private static IConnectionProcessor CreateTcpClientProcessor()
        {
            return new ConnectionProcessor();
        }

        private static TcpListener CreateTcpListener(IPAddress ipAddress)
        {
            return new TcpListener(ipAddress, Port);
        }

        private static ITcpListenerWrapper CreateTcpListenerWrapper()
        {
            IPAddress ipAddress = ParseIpAddress(IpAddressString);
            TcpListener tcpListener = CreateTcpListener(ipAddress);
            return new TcpListenerWrapper(tcpListener);
        }

        private static IPAddress ParseIpAddress(string address)
        {
            return IPAddress.Parse(address);
        }

        private static void Run(IServer server)
        {
            server.Run();
        }
    }
}