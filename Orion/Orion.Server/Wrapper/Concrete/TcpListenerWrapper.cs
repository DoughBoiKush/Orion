namespace Orion.Server.Wrapper.Concrete
{
    using System.Net;
    using System.Net.Sockets;

    using Orion.Server.Wrapper.Abstract;

    public class TcpListenerWrapper : ITcpListenerWrapper
    {
        private readonly IPAddress ipAddress;

        private readonly int port;

        private readonly TcpListener tcpListener;

        public TcpListenerWrapper(int port, IPAddress ipAddress)
        {
            this.port = port;
            this.ipAddress = ipAddress;

            tcpListener = CreateTcpListener();
        }

        public Socket AcceptSocket()
        {
            return tcpListener.AcceptSocket();
        }

        public void Start()
        {
            tcpListener.Start();
        }

        private TcpListener CreateTcpListener()
        {
            return new TcpListener(ipAddress, port);
        }
    }
}