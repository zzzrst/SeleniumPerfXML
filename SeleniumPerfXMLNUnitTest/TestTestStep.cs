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
    public class TestTestStep
    {
        private string saveFileLocation;
        private string readFileLocation;
        private string webSiteLocation;
        private string logName;
        private string reportName; 

        [SetUp]
        public void SetUp()
        {
            webSiteLocation = "C:\\SeleniumPerfXML\\Testing";
            saveFileLocation = "C:\\SeleniumPerfXML\\Testing\\Files";
            readFileLocation = "C:\\SeleniumPerfXML\\Testing\\TestTestStep";
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
        public void TestFailTestStep()
        {
            TestSetXml testSet;

            testSet = buildTestSet("\\TestFailTestStep.xml");
            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            Reporter reporter = (Reporter)testSet.Reporter;

            Assert.IsFalse(reporter.TestSetStatuses[0].RunSuccessful);
            Assert.IsFalse(reporter.TestCaseStatuses[0].RunSuccessful);
            Assert.IsFalse(reporter.TestCaseToTestSteps[reporter.TestCaseStatuses[0]][0].RunSuccessful);
        }

        [Test]
        public void TestNoLog()
        {

        }

        [Test]
        public void TestAODA()
        {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestOpenClose.xml", $"{webSiteLocation}\\Google.html");
            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;

            Assert.Pass();
            Assert.IsTrue(Directory.Exists(saveFileLocation));
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful);
        }

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
