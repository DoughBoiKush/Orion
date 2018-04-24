namespace Orion.Server.Wrapper.Concrete
{
    using System;
    using System.Net.Sockets;

    using Orion.Server.Wrapper.Abstract;

    public class TcpListenerWrapper : ITcpListenerWrapper
    {
        public TcpClient AcceptTcpClient()
        {
            return new TcpClient();
        }

        public void Start()
        {
            //TODO: Implement
        }
    }
}