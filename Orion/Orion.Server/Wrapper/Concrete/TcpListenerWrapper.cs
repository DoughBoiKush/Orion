﻿namespace Orion.Server.Wrapper.Concrete
{
    using System.Net.Sockets;

    using Orion.Server.Wrapper.Abstract;

    public class TcpListenerWrapper : ITcpListenerWrapper
    {
        private readonly TcpListener tcpListener;

        public TcpListenerWrapper(TcpListener tcpListener)
        {
            this.tcpListener = tcpListener;
        }

        public TcpClient AcceptTcpClient()
        {
            return tcpListener.AcceptTcpClient();
        }

        public void Start()
        {
            tcpListener.Start();
        }
    }
}