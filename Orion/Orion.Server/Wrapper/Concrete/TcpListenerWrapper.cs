namespace Orion.Server.Wrapper.Concrete
{
    using System;
    using System.Net.Sockets;

    using Orion.Server.Wrapper.Abstract;

    public class TcpListenerWrapper : ITcpListenerWrapper
    {
        public TcpClient AcceptTcpClient()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}