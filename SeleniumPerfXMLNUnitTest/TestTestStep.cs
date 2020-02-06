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
                {
                    bool notDeleted = true;
                    do
                    {
                        try
                        {
                            File.Delete(saveFileLocation + logName);
                            notDeleted = false;
                        }
                        catch (IOException)
                        {
                        }
                    } while (notDeleted);
                }
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
        public void TestLog()
        {
            TestSetXml testSet;

            testSet = buildTestSet("\\TestLog.xml");
            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            Reporter reporter = (Reporter)testSet.Reporter;
            string firstLine;
            using (StreamReader reader = new StreamReader(this.saveFileLocation + logName))
            {
                firstLine = reader.ReadLine();
                reader.ReadToEnd();
                reader.Close();
            }
            
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful);
            Assert.IsTrue(reporter.TestCaseStatuses[0].RunSuccessful);
            Assert.IsTrue(reporter.TestCaseToTestSteps[reporter.TestCaseStatuses[0]][0].RunSuccessful);
            Assert.AreEqual("Name:Logging", firstLine.Trim(), "Log file should have teststep in it");
        }

        [Test]
        public void TestNoLog()
        {
            TestSetXml testSet;

            testSet = buildTestSet("\\TestNoLog.xml");
            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            Reporter reporter = (Reporter)testSet.Reporter;
            string firstLine;
            using (StreamReader reader = new StreamReader(this.saveFileLocation + logName))
            {
                firstLine = reader.ReadLine();
                reader.ReadToEnd();
                reader.Close();
            }

            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful);
            Assert.IsTrue(reporter.TestCaseStatuses[0].RunSuccessful);
            Assert.IsTrue(reporter.TestCaseToTestSteps[reporter.TestCaseStatuses[0]][0].RunSuccessful);
            Assert.AreNotEqual("Name:No logging", firstLine.Trim(), "Log file should not have teststep in it");
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

            Assert.IsTrue(Directory.Exists(saveFileLocation));
            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful);
        }

        /// <summary>
        /// Tests all concrete test steps except:
        /// Sign in: it is a combination of click element and populate element
        /// 
        /// </summary>
        [Test]
        public void TestAllConcreteTestSteps()
        {
            TestSetXml testSet;

            testSet = buildTestSet("\\TestOpenClose.xml");
            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            Reporter reporter = (Reporter)testSet.Reporter;

            Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful);
            Assert.IsTrue(reporter.TestCaseStatuses[0].RunSuccessful);
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
                PassedInRespectRunAODAFlag = "true",
                XMLFile = $"{readFileLocation}{testFileName}",
            };

            return builder.BuildTestSet();
        }
    }
}
