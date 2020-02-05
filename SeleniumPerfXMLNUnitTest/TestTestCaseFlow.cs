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
    public class TestTestCaseFlow
    {
        public string saveFileLocation;
        public string readFileLocation;
        public string logName;
        public string reportName; 

        [SetUp]
        public void SetUp()
        {
            saveFileLocation = "C:\\SeleniumPerfXML\\Testing\\Files";
            readFileLocation = "C:\\SeleniumPerfXML\\Testing\\TestTestCaseFlow";
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
        public void TestBareMinimum()
        {
            TestSetXml testStep;

            TestSetBuilder builder = new TestSetBuilder($"{readFileLocation}\\TestBareMinimum.xml")
            {
                URL = "testurl",
                CsvSaveFileLocation = saveFileLocation,
                LogSaveFileLocation = saveFileLocation,
                ScreenshotSaveLocation = saveFileLocation,
                ReportSaveFileLocation = saveFileLocation,
                XMLFile = $"{readFileLocation}\\TestBareMinimum.xml",
            };

            testStep = builder.BuildTestSet();
            AutomationTestSetDriver.RunTestSet(testStep);
            testStep.Reporter.Report();

            Reporter reporter = (Reporter)testStep.Reporter;

            Assert.Pass();
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful);
        }

        [Test]
        public void TestDuplicateIDs() { }
        
        [Test]
        public void TestSimpleIf() { }

        [Test]
        public void TestIfChain() { }

        [Test]
        public void TestElseIf() { }

        [Test]
        public void TestElse() { }
    }
}
