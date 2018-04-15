namespace Orion.Server.Wrapper.Abstract
{
    using System.Net.Sockets;

    public interface ITcpListenerWrapper
    {
        Socket AcceptSocket();

        void Start();
    }
}