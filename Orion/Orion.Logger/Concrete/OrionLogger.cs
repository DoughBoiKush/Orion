namespace Orion.Logger.Concrete
{
    using System;
    using System.IO;
    using System.Reflection;

    using Orion.Logger.Abstract;

    public class OrionLogger : IOrionLogger
    {
        private const string OrionLogRelativeDirectory = @"..\..\..\..\Logs\OrionLog.txt";

        public void LogMessage(string message)
        {
            var relativeFilePath = GetRelativeFilePath();
            WriteMessage(message, relativeFilePath);
        }

        private string FormatMessage(string message)
        {
            string formattedDateTime = GetFormattedTime();
            string callingProject = GetFormattedProjectName();
            return $"{formattedDateTime}{callingProject}\t:: {message}";
        }

        private string GetCallingProjectName()
        {
            return Assembly.GetCallingAssembly().GetName().Name;
        }

        private string GetFormattedProjectName()
        {
            string projectName = GetCallingProjectName();
            string formattedProjectName = projectName.Split('.')[1];
            return $"[{formattedProjectName}]";
        }

        private string GetFormattedTime()
        {
            return $"[{DateTime.Now:hh:mm:ss}]";
        }

        private string GetRelativeFilePath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string library = Uri.UnescapeDataString(uri.Path);
            string directoryName = Path.GetDirectoryName(library);
            return Path.Combine(directoryName, OrionLogRelativeDirectory);
        }

        private void WriteMessage(string message, string relativeFilePath)
        {
            string formattedMessage = FormatMessage(message);
            File.AppendAllLines(relativeFilePath, new[] { formattedMessage });
            Console.Write(message);
        }
    }
}