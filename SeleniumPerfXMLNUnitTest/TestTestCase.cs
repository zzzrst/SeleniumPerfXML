using System;
using AutomationTestSetFramework;
using CommandLine;
using NUnit.Framework;
using SeleniumPerfXML;
using SeleniumPerfXML.Implementations;
using System.Configuration;
using System.IO;
using SeleniumPerfXML.Implementations.Loggers_and_Reporters;

namespace SeleniumPerfXMLNUnitTest
{
    public class TestTestCase
    {
        private string saveFileLocation;
        private string readFileLocation;
        private string logName;
        private string reportName; 

        [SetUp]
        public void SetUp()
        {
            saveFileLocation = "C:\\SeleniumPerfXML\\Testing\\Files";
            readFileLocation = "C:\\SeleniumPerfXML\\Testing\\TestTestCase";
            logName = "\\Log.txt";
            reportName = "\\Report.txt";
            // Removes all previous ran test results
            // If directory does not exist, don't even try   
            if (Directory.Exists(saveFileLocation))
            {
                if (File.Exists(saveFileLocation + logName))
                    File.Delete(saveFileLocation + logName);
                if (File.Exists(saveFileLocation + reportName))
                    File.Delete(saveFileLocation + reportName);
                Directory.Delete(saveFileLocation);
            }
        }

        [Test]
        public void TestDuplicateIDs() { }

        [Test]
        public void TestSimpleIf() { }

        [Test]
        public void TestNestedIf()
        {

        }
        [Test]
        public void TestElseIf() { }

        [Test]
        public void TestElse() { }

        [Test]
        public void TestCannotFindTestStep() { }

        [Test]
        public void TestRepeatForMultiple() { }

        private TestSetXml buildTestSet(string testFileName, string url = "testUrl")
        {
            TestSetBuilder builder = new TestSetBuilder($"{readFileLocation}{testFileName}")
            {
                URL = url,
                CsvSaveFileLocation = saveFileLocation,
                LogSaveFileLocation = saveFileLocation,
                ScreenshotSaveLocation = saveFileLocation,
                ReportSaveFileLocation = saveFileLocation,
                XMLFile = $"{readFileLocation}{testFileName}",
            };

            return builder.BuildTestSet();
        }
    }
}
