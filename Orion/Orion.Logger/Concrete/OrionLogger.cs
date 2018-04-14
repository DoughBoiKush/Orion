namespace Orion.Logger.Concrete
{
    using System;

    using Orion.Logger.Abstract;

    public class OrionLogger : IOrionLogger
    {
        public void LogMessage(string message)
        {
            //TODO: Currently, this only writes to the console. We will implement file I/O output as well.

            Console.Write(message);
        }
    }
}