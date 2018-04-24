namespace Orion.Network.Abstract
{
    using System.Net.Sockets;

    public interface IConnectionProcessor
    {
        void ProcessConnection(TcpClient client);
    }
}