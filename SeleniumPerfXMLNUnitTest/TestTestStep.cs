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
        public string saveFileLocation;
        public string readFileLocation;
        public string logName;
        public string reportName; 

        [SetUp]
        public void SetUp()
        {
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
            TestSetXml testStep;

            TestSetBuilder builder = new TestSetBuilder($"{readFileLocation}\\TestFailTestStep.xml")
            {
                URL = "Test",
                CsvSaveFileLocation = saveFileLocation,
                LogSaveFileLocation = saveFileLocation,
                ScreenshotSaveLocation = saveFileLocation,
                ReportSaveFileLocation = saveFileLocation,
                XMLFile = $"{readFileLocation}\\TestFailTestStep.xml",
            };

            testStep = builder.BuildTestSet();
            AutomationTestSetDriver.RunTestSet(testStep);
            testStep.Reporter.Report();

            Reporter reporter = (Reporter)testStep.Reporter;

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
            TestSetXml testStep;

            TestSetBuilder builder = new TestSetBuilder($"{readFileLocation}\\TestOpenClose.xml")
            {
                URL = "https://www.google.ca/",
                Browser = "chrome",
                Environment = "Hello World!",
                CsvSaveFileLocation = saveFileLocation,
                LogSaveFileLocation = saveFileLocation,
                ScreenshotSaveLocation = saveFileLocation,
                ReportSaveFileLocation = saveFileLocation,
                XMLFile = $"{readFileLocation}\\TestOpenClose.xml",
            };

            testStep = builder.BuildTestSet();
            AutomationTestSetDriver.RunTestSet(testStep);
            testStep.Reporter.Report();

            Reporter reporter = (Reporter)testStep.Reporter;

            Assert.Pass();
            Assert.IsTrue(Directory.Exists(saveFileLocation));
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful);
        }
    }
}
