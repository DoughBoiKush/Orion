namespace Orion.Server
{
    using Orion.Server.Abstract;
    using Orion.Server.Concrete;

    public class ServerRunner
    {
        private static IServerProvider serverProvider;

        public static void Main()
        {
            serverProvider = CreateServerProvider();

            serverProvider.RunServer();
        }

        private static IServerProvider CreateServerProvider()
        {
            return new ServerProvider();
        }
    }
}