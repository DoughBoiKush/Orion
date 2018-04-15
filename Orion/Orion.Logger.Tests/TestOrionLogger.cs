namespace Orion.Logger.Tests
{
    using System;
    using System.IO;
    using System.Reflection;

    using NUnit.Framework;

    using Orion.Logger.Concrete;

    [TestFixture]
    public class TestOrionLogger
    {
        private const string OrionLogRelativeDirectory = @"..\..\..\..\Logs\OrionLog.txt";

        private OrionLogger systemUnderTest;

        [Test]
        public void LogMessage_WhenInvoked_LogsFormattedCallingProjectsNameInLogMessage()
        {
            systemUnderTest.LogMessage("some other string");

            var formattedAssemblyName = GetFormattedAssemblyName();

            string[] actualLogContents = GetLogFileContents();

            Assert.That(actualLogContents[0].Contains($"[{formattedAssemblyName}]"));
        }

        [Test]
        public void LogMessage_WhenInvoked_LogsFormattedTimeInLog()
        {
            DateTime currentDateTime = DateTime.Now;

            systemUnderTest.LogMessage("some string");

            string[] actualLogContents = GetLogFileContents();

            Assert.That(actualLogContents[0].Contains($"[{currentDateTime:hh:mm:ss}]"));
        }

        [TestCase("message")]
        [TestCase("potato")]
        [TestCase("rekted")]
        public void LogMessage_WhenInvoked_LogsMessageToLogsDirectory(string message)
        {
            systemUnderTest.LogMessage(message);

            string[] actualLogContents = GetLogFileContents();

            Assert.That(actualLogContents[0].Contains(message));
        }

        [Test]
        public void LogMessage_WhenInvoked_MessageIsInProperFormat()
        {
            DateTime currentDateTime = DateTime.Now;
            systemUnderTest.LogMessage("RuneScape sucks");

            string[] actualLogContents = GetLogFileContents();

            Assert.That(actualLogContents[0], Is.EqualTo($"[{currentDateTime:hh:mm:ss}][Logger]\t:: RuneScape sucks"));
        }

        [Test]
        public void LogMessage_WhenInvoked_MessageIsSpacedCorrectly()
        {
            systemUnderTest.LogMessage("something");

            string[] actualLogContents = GetLogFileContents();

            Assert.That(actualLogContents[0].Contains("\t:: something"));
        }

        [SetUp]
        public void SetUp()
        {
            systemUnderTest = CreateSystemUnderTest();
        }

        [TearDown]
        public void TearDown()
        {
            RemoveOrionLogFile();
        }

        private static string GetOrionLogPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string library = Uri.UnescapeDataString(uri.Path);
            string path = Path.GetDirectoryName(library);
            return Path.GetFullPath(Path.Combine(path, OrionLogRelativeDirectory));
        }

        private OrionLogger CreateSystemUnderTest()
        {
            return new OrionLogger();
        }

        private string GetFormattedAssemblyName()
        {
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            return assemblyName.Split('.')[1];
        }

        private string[] GetLogFileContents()
        {
            var orionLogPath = GetOrionLogPath();
            return File.ReadAllLines(orionLogPath);
        }

        private void RemoveOrionLogFile()
        {
            string path = GetOrionLogPath();
            File.Delete(path);
        }
    }
}