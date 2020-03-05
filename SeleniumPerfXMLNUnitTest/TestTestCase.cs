using System;
using AutomationTestSetFramework;
using CommandLine;
using NUnit.Framework;
using SeleniumPerfXML;
using SeleniumPerfXML.Implementations;
using System.Configuration;
using System.IO;
using SeleniumPerfXML.Implementations.Loggers_and_Reporters;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

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
            string executingLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            saveFileLocation = $"{executingLocation}\\Testing\\Files";
            readFileLocation = $"{executingLocation}\\Testing\\TestTestCase";
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
        public void TestDuplicateIDs()
        {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestCaseDuplicateId.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;

            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful, "Expeted to pass");
            Assert.AreEqual(1, reporter.TestCaseStatuses.Count, "Expeted to have 1 test case");
            Assert.AreEqual(1, reporter.TestCaseToTestSteps.Sum(x => x.Value.Count), "Expected to have 1 test set");
        }

        [Test]
        public void TestSimpleIf()
        {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestCaseSimpleIf.xml");

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

            testSet = buildTestSet("\\TestCaseNestedIf.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful, "Expected to pass");
            Assert.AreEqual(1, reporter.TestCaseStatuses.Count, "Expected to have 1 test case");
            Assert.AreEqual(2, reporter.TestCaseToTestSteps.Sum(x => x.Value.Count), "Expected to have 2 test steps");
        }

        [Test]
        public void TestElseIf()
        {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestCaseElseIf.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful, "Expected to pass");
            Assert.AreEqual(1, reporter.TestCaseStatuses.Count, "Expected to have 1 test case");
            Assert.AreEqual(2, reporter.TestCaseToTestSteps.Sum(x => x.Value.Count), "Expected to have 2 test steps");
            Assert.AreEqual(1, countNotRanTestSteps(reporter), "Expected to have 1 not ran test steps");
        }

        [Test]
        public void TestElse()
        {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestCaseElse.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful, "Expected to pass");
            Assert.AreEqual(1, reporter.TestCaseStatuses.Count, "Expected to have 1 test case");
            Assert.AreEqual(4, reporter.TestCaseToTestSteps.Sum(x => x.Value.Count), "Expected to have 4 test steps");
            Assert.AreEqual(1, countNotRanTestSteps(reporter), "Expected to have 1 not ran test steps");
        }

        [Test]
        public void TestCannotFindTestStep()
        {
            TestSetXml testSet;
            Reporter reporter;

            try
            {
                testSet = buildTestSet("\\TestCaseMissingTestStep.xml");
                AutomationTestSetDriver.RunTestSet(testSet);
                testSet.Reporter.Report();
            }
            catch (Exception)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void TestRepeatForMultiple()
        {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestCaseRepeatMultiple.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful, "Expected to pass");
            Assert.AreEqual(1, reporter.TestCaseStatuses.Count, "Expected to have 1 test case");
            Assert.AreEqual(2, reporter.TestCaseToTestSteps.Sum(x => x.Value.Count), "Expected to have 2 test steps");
        }

        [Test]
        public void TestSameTestStep()
        {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestCaseSameTestStep.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;

            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful, "Expected to pass");
            Assert.AreEqual(1, reporter.TestCaseStatuses.Count, "Expected to have 1 test case");
            Assert.AreEqual(2, reporter.TestCaseToTestSteps.Sum(x => x.Value.Count), "Expected to have 2 test steps");
        }

        [Test]
        public void TestUnknownNodeName()
        {
            TestSetXml testSet;
            Reporter reporter;

            testSet = buildTestSet("\\TestCaseUnknownNodeName.xml");

            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            reporter = (Reporter)testSet.Reporter;
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful, "Expected to pass");
            Assert.AreEqual(1, reporter.TestCaseStatuses.Count, "Expected to have 1 test case");
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
                PassedInRespectRepeatFor = "true",
                XMLFile = $"{readFileLocation}{testFileName}",
            };

            return builder.BuildTestSet();
        }

        private int countNotRanTestSteps(Reporter reporter)
        {
            int count = 0;
            foreach (List<ITestStepStatus> list in reporter.TestCaseToTestSteps.Values)
            {
                foreach (ITestStepStatus status in list)
                {
                    if (status.Actual.Equals("N/A"))
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
