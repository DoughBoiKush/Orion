namespace Orion.Server.Wrapper.Abstract
{
    using System.Net.Sockets;

    public interface ITcpListenerWrapper
    {
        TcpClient AcceptTcpClient();

        void Start();
    }
}