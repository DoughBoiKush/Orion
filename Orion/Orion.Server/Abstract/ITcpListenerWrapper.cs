namespace Orion.Server.Abstract
{
    using System.Net.Sockets;

    public interface ITcpListenerWrapper
    {
        TcpClient AcceptTcpClient();

        void Start();
    }
}