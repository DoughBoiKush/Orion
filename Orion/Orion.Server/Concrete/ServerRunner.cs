namespace Orion.Server.Concrete
{
    using System;

    using Orion.Server.Abstract;

    public class ServerRunner : IServerRunner
    {
        private static ServerRunner serverRunner;

        public static void Main()
        {
            serverRunner = CreateServerRunner();

            serverRunner.RunServer();
        }

        public void RunServer()
        {
            throw new NotImplementedException();
        }

        private static ServerRunner CreateServerRunner()
        {
            return new ServerRunner();
        }
    }
}