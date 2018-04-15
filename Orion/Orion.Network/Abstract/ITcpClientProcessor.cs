namespace Orion.Network.Abstract
{
    using System.Net.Sockets;

    public interface ITcpClientProcessor
    {
        void ProcessClient(TcpClient tcpClient);
    }
}