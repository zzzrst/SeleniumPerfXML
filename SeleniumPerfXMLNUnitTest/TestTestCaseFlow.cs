using System;
using AutomationTestSetFramework;
using CommandLine;
using NUnit.Framework;
using SeleniumPerfXML;
using SeleniumPerfXML.Implementations;
using System.Configuration;
using System.IO;
using SeleniumPerfXML.Implementations.Loggers_and_Reporters;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            string executingLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            saveFileLocation = $"{executingLocation}\\Testing\\Files";
            readFileLocation = $"{executingLocation}\\Testing\\TestTestCaseFlow";
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
                Directory.Delete(saveFileLocation, true);
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
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful,"Expeted to pass");
        }

        [Test]
        public void TestDuplicateIDs() {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestSetDuplicateId.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;

            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful, "Expeted to pass");
            Assert.AreEqual(1, reporter.TestCaseStatuses.Count, "Expeted to have 1 test case");
            Assert.AreEqual(1, reporter.TestCaseToTestSteps.Sum(x => x.Value.Count), "Expected to have 1 test set");
        }

        [Test]
        public void TestSameCaseRanTwice()
        {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestSameCaseRanTwice.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful,"Expected to pass");
            Assert.AreEqual(2, reporter.TestCaseStatuses.Count, "Expected to have 2 test case");
            Assert.AreEqual(2, reporter.TestCaseToTestSteps.Sum(x => x.Value.Count), "Expected to have 2 test steps");
        }

        [Test]
        public void TestSimpleIf() {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestSetSimpleIf.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful, "Expected to pass");
            Assert.AreEqual(1, reporter.TestCaseStatuses.Count, "Expected to have 1 test case");
            Assert.AreEqual(1, reporter.TestCaseToTestSteps.Sum(x => x.Value.Count), "Expected to have 1 test steps");
        }

        [Test]
        public void TestNestedIf()
        {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestSetNestedIf.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful, "Expected to pass");
            Assert.AreEqual(2, reporter.TestCaseStatuses.Count, "Expected to have 2 test case");
            Assert.AreEqual(2, reporter.TestCaseToTestSteps.Sum(x => x.Value.Count), "Expected to have 2 test steps");
        }

        [Test]
        public void TestElseIf() {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestSetElseIf.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful, "Expected to pass");
            Assert.AreEqual(2, reporter.TestCaseStatuses.Count, "Expected to have 1 test case");
            Assert.AreEqual(1, reporter.TestCaseToTestSteps.Sum(x => x.Value.Count), "Expected to have 1 test steps");
        }

        [Test]
        public void TestElse() {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestSetElse.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful, "Expected to pass");
            Assert.AreEqual(4, reporter.TestCaseStatuses.Count, "Expected to have 4 test case");
            Assert.AreEqual(3, reporter.TestCaseToTestSteps.Sum(x => x.Value.Count), "Expected to have 3 test steps");
        }

        [Test]
        public void TestCannotFindTestCase()
        {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestSetMissingTestCase.xml");

            try
            {
                AutomationTestSetDriver.RunTestSet(testSet);
            }
            catch (Exception)
            {
            }

            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;
            Assert.IsFalse(reporter.TestSetStatuses[0].RunSuccessful, "Expected to fail");
            Assert.AreEqual(0, reporter.TestCaseStatuses.Count, "Expected to have 0 test case");
            Assert.AreEqual(0, reporter.TestCaseToTestSteps.Sum(x => x.Value.Count), "Expected to have 0 test steps");
        }

        [Test]
        public void UnknownNodeName()
        {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestSetUnknownNodeName.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful, "Expected to pass");
            Assert.AreEqual(2, reporter.TestCaseStatuses.Count, "Expected to have 2 test case");
            Assert.AreEqual(2, reporter.TestCaseToTestSteps.Sum(x => x.Value.Count), "Expected to have 2 test steps");
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
