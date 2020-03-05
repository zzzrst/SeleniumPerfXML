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
using System.Reflection;

namespace SeleniumPerfXMLNUnitTest
{
    public class TestTestStep
    {
        private string saveFileLocation;
        private string readFileLocation;
        private string webSiteLocation;
        private string logName;
        private string reportName;
        private TestSetBuilder builder;

        [SetUp]
        public void SetUp()
        {
            string executingLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            saveFileLocation = $"{executingLocation}/Testing/Files";
            readFileLocation = $"{executingLocation}/Testing/TestTestStep";
            webSiteLocation = $"{executingLocation}/Testing";
            logName = $"{executingLocation}/SeleniumPerfXML.log";
            reportName = "/Report.txt";
            // Removes all previous ran test results
            // If directory does not exist, don't even try   
            if (Directory.Exists(saveFileLocation))
            {
                if (File.Exists(logName))
                {
                    bool notDeleted = true;
                    do
                    {
                        try
                        {
                            File.Delete(logName);
                            notDeleted = false;
                        }
                        catch (IOException)
                        {
                        }
                    } while (notDeleted);
                }
                if (File.Exists(saveFileLocation + reportName))
                    File.Delete(saveFileLocation + reportName);
                Directory.Delete(saveFileLocation,true);
            }
        }

        [Test]
        public void TestFailTestStep()
        {
            TestSetXml testSet;

            testSet = buildTestSet("/TestFailTestStep.xml");
            AutomationTestSetDriver.RunTestSet(testSet);
            testSet.Reporter.Report();

            Reporter reporter = (Reporter)testSet.Reporter;

            Assert.IsFalse(reporter.TestSetStatuses[0].RunSuccessful);
            Assert.IsFalse(reporter.TestCaseStatuses[0].RunSuccessful);
            Assert.IsFalse(reporter.TestCaseToTestSteps[reporter.TestCaseStatuses[0]][0].RunSuccessful);
        }

        ///These don't work on the work flow for some reason...
        //[Test]
        //public void TestLog()
        //{
        //    TestSetXml testSet;

        //    testSet = buildTestSet("/TestLog.xml");
        //    AutomationTestSetDriver.RunTestSet(testSet);
        //    testSet.Reporter.Report();

        //    Reporter reporter = (Reporter)testSet.Reporter;

        //    string tempLogName = $"{this.logName}.tmp";

        //    File.Copy(this.logName, tempLogName);

        //    string logFile;
        //    using (StreamReader reader = new StreamReader(tempLogName))
        //    {
        //        logFile = reader.ReadToEnd();
        //    }

        //    File.Delete(tempLogName);
            
        //    Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful);
        //    Assert.IsTrue(reporter.TestCaseStatuses[0].RunSuccessful);
        //    Assert.IsTrue(reporter.TestCaseToTestSteps[reporter.TestCaseStatuses[0]][0].RunSuccessful);
        //    Assert.IsTrue(logFile.Contains("Name:Logging"), "Log file should have teststep in it");
        //}

        //[Test]
        //public void TestNoLog()
        //{
        //    TestSetXml testSet;

        //    testSet = buildTestSet("/TestNoLog.xml");
        //    AutomationTestSetDriver.RunTestSet(testSet);
        //    testSet.Reporter.Report();

        //    Reporter reporter = (Reporter)testSet.Reporter;

        //    string tempLogName = $"{this.logName}.tmp";

        //    File.Copy(this.logName, tempLogName);

        //    string logFile;
        //    using (StreamReader reader = new StreamReader(tempLogName))
        //    {
        //        logFile = reader.ReadToEnd();
        //    }

        //    File.Delete(tempLogName);

        //    Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful);
        //    Assert.IsTrue(reporter.TestCaseStatuses[0].RunSuccessful);
        //    Assert.IsTrue(reporter.TestCaseToTestSteps[reporter.TestCaseStatuses[0]][0].RunSuccessful);
        //    Assert.IsFalse(logFile.Contains("Name:No logging"), "Log file should not have teststep in it");
        //}

        /// <summary>
        /// Test To see if AODA Works
        /// Not ran automaticaly since it requires a web browser
        /// </summary>
        //[Test]
        //public void TestAODA()
        //{
        //    TestSetXml testSet;
        //    Reporter reporter;

        //    testSet = buildTestSet("/TestOpenClose.xml", $"{webSiteLocation}/Google.html");
        //    AutomationTestSetDriver.RunTestSet(testSet);
        //    testSet.Reporter.Report();
        //    builder.RunAODA();

        //    reporter = (Reporter)testSet.Reporter;

        //    Assert.IsTrue(Directory.Exists(saveFileLocation));
        //    Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful);
        //}

        /// <summary>
        /// Tests all concrete test steps except:
        /// Sign in: it is a combination of click element and populate element
        /// Not ran automaticaly since it requires a web browser
        /// </summary>
        //[Test]
        //public void TestAllConcreteTestSteps()
        //{
        //    TestSetXml testSet;

        //    testSet = buildTestSet("/TestAllConcreteSteps.xml");
        //    AutomationTestSetDriver.RunTestSet(testSet);
        //    testSet.Reporter.Report();

        //    Reporter reporter = (Reporter)testSet.Reporter;

        //    Assert.IsTrue(reporter.TestSetStatuses[0].RunSuccessful);
        //    Assert.IsTrue(reporter.TestCaseStatuses[0].RunSuccessful);
        //}

        private TestSetXml buildTestSet(string testFileName, string url = "testUrl")
        {
            builder = new TestSetBuilder($"{readFileLocation}{testFileName}")
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
