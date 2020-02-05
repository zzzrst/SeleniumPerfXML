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
        private string saveFileLocation;
        private string readFileLocation;
        private string logName;
        private string reportName; 

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
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestBareMinimum.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;

            Assert.Pass();
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful);
        }

        [Test]
        public void TestDuplicateIDs() {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestSetDuplicateId.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;

            Assert.Equals(1,reporter.TestSetStatuses.Count);
            Assert.Equals(1, reporter.TestCaseStatuses.Count);
            Assert.Equals(1, reporter.TestCaseToTestSteps.Count);
        }
        
        [Test]
        public void TestSimpleIf() {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestSetDuplicateId.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;
            Assert.IsFalse(reporter.TestSetStatuses[0].RunSuccessful);
            Assert.IsFalse(reporter.TestCaseStatuses[0].RunSuccessful);
            Assert.IsFalse(reporter.TestCaseToTestSteps[reporter.TestCaseStatuses[0]][0].RunSuccessful);
        }

        [Test]
        public void TestIfChain() { }

        [Test]
        public void TestElseIf() { }

        [Test]
        public void TestElse() { }

        [Test]
        public void TestCannotFindTestCase() { }

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
