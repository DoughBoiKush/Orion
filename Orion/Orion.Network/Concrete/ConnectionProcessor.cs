namespace Orion.Network.Concrete
{
    using System;
    using System.Net.Sockets;

    using Orion.Network.Abstract;

    public class ConnectionProcessor : IConnectionProcessor
    {
        public void ProcessConnection(TcpClient socket)
        {
            throw new NotImplementedException();
        }
    }
}